using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using HBD.Framework.Data.SqlClient;
using HBD.Framework.Extensions;
using HBD.Framework.Data;
using System.Threading.Tasks;

namespace HBD.SSDT.Extensions.Helpers
{
    public class MergeScriptGeneration : SqlClientHelper
    {
        #region consts
        private const string DefaultOutputFolder = "Output";
        private const string DefaultOutputFileName = "Merge_Data_{0}_Table.sql";
        private const string DefaultOutputExecuteFileName = "Merges_Data.sql";
        #endregion

        public string OutputFolder { get; set; }

        #region Contructors
        public MergeScriptGeneration(string nameOrConnectionString)
            : this(nameOrConnectionString, null)
        { }

        public MergeScriptGeneration(string nameOrConnectionString, string outputFolder) : base(nameOrConnectionString)
        {
            this.OutputFolder = outputFolder ?? DefaultOutputFolder;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Generate Static Data Migration Script for all Tables in DB.
        /// </summary>
        /// <param name="option"></param>
        public virtual void GenerateAll(MergeScriptOption option = MergeScriptOption.Default)
        {
            var tables = SchemaInfo.Tables.Select(t => t.Name.FullName).ToArray();
            Generate(option, tables);
        }

        public virtual void Generate(MergeScriptOption option = MergeScriptOption.Default, params string[] tables)
            => Generate(option, null, tables);

        /// <summary>
        /// Generate Static Data Migration Script for the list of Tables.
        /// </summary>
        /// <param name="option"></param>
        /// <param name="tables">The list of Table Name</param>
        public virtual void Generate(MergeScriptOption option = MergeScriptOption.Default, Action<string> updateStatus = null, params string[] tables)
        {
            if (tables.Length == 0) throw new ArgumentNullException(nameof(tables));

            if (!Directory.Exists(this.OutputFolder))
                Directory.CreateDirectory(this.OutputFolder);

            var builder = new StringBuilder();
            AddHeader(builder);

            foreach (var tb in SortTablesByDependence(tables))
            {
                if (updateStatus != null) updateStatus(tb);

                var script = this.Generate(tb, option);
                if (string.IsNullOrEmpty(script)) continue;
                try
                {
                    var fileName = string.Format(DefaultOutputFileName, tb.Replace("[", string.Empty).Replace("]", string.Empty).Replace(" ", string.Empty));
                    builder.AppendFormat(":r .\\{0}", fileName).Append(Environment.NewLine);
                    File.WriteAllText(Path.Combine(this.OutputFolder, fileName), script);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            //Write Execute Script.
            File.WriteAllText(Path.Combine(this.OutputFolder, DefaultOutputExecuteFileName), builder.ToString());
        }

        public virtual Task GenerateAsync(MergeScriptOption option = MergeScriptOption.Default, params string[] tables)
            => Task.Run(() => Generate(option, tables));

        public virtual Task GenerateAsync(MergeScriptOption option = MergeScriptOption.Default, Action<string> updateStatus = null, params string[] tables)
           => Task.Run(() => Generate(option, updateStatus, tables));

        /// <summary>
        /// Generate Static Data Migration Script From DataTable
        /// </summary>
        /// <param name="table"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual string Generate(DataTable table, MergeScriptOption option = MergeScriptOption.Default)
        {
            if (table == null || table.Rows.Count == 0) return null;
            var tableName = Common.GetSqlName(table.TableName);
            var isIdentity = IsTableIdentity(tableName);

            var builder = new StringBuilder();
            AddHeader(builder);

            builder.AppendFormat("PRINT N'Merging static data to {0}';", tableName).Append(Environment.NewLine)
                .Append("GO").Append(Environment.NewLine).Append(Environment.NewLine);

            if (isIdentity)
            {
                builder.Append("BEGIN TRY").Append(Environment.NewLine)
                    .Append($"SET IDENTITY_INSERT {tableName} ON").Append(Environment.NewLine)
                    .Append("END TRY").Append(Environment.NewLine)
                    .Append("BEGIN CATCH").Append(Environment.NewLine)
                    .Append("END CATCH").Append(Environment.NewLine)
                    .Append("GO").Append(Environment.NewLine).Append(Environment.NewLine);
            }

            builder.Append($"MERGE INTO {tableName} AS Target")
                .Append(Environment.NewLine).Append("USING( VALUES").Append(Environment.NewLine);

            for (var i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var strValues = string.Join(",", BuildSqlValueList(row.ItemArray));
                builder.Append("(").Append(strValues).Append(")");
                if (i < table.Rows.Count - 1) builder.Append(",");
                builder.Append(Environment.NewLine);
            }

            var columns = BuildSqlFieldList(table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray());
            builder.AppendFormat(")AS Source({0})", string.Join(",", columns))
                .Append(Environment.NewLine)
                .Append("ON ");

            var primaryKeys = string.Join(" AND ",
                table.PrimaryKey.Select(p => $"Target.[{p.ColumnName}] = Source.[{p.ColumnName}]"));
            builder.Append(primaryKeys).Append(Environment.NewLine);

            var setColumns = string.Join("," + Environment.NewLine,
                table.Columns.Cast<DataColumn>().Where(c => !table.PrimaryKey.Contains(c)).Select(c => $"[{c.ColumnName}] = Source.[{c.ColumnName}]"));

            if (option.HasFlag(MergeScriptOption.Update)
                && !string.IsNullOrWhiteSpace(setColumns))
            {
                builder.Append("WHEN MATCHED THEN").Append(Environment.NewLine)
                    .Append("UPDATE SET ")
                    .Append(setColumns).Append(Environment.NewLine);
            }

            if (option.HasFlag(MergeScriptOption.Insert))
            {
                builder.Append("WHEN NOT MATCHED BY TARGET THEN").Append(Environment.NewLine)
                    .AppendFormat("INSERT({0})", columns).Append(Environment.NewLine)
                    .AppendFormat("VALUES({0})", columns).Append(Environment.NewLine);
            }

            if (option.HasFlag(MergeScriptOption.Delete))
            {
                builder.Append("WHEN NOT MATCHED BY SOURCE THEN").Append(Environment.NewLine)
                    .Append("DELETE");
            }

            builder.Append(";").Append(Environment.NewLine)
                .Append("GO").Append(Environment.NewLine).Append(Environment.NewLine);

            if (isIdentity)
            {
                builder.Append("BEGIN TRY").Append(Environment.NewLine)
                    .Append($"SET IDENTITY_INSERT {tableName} OFF").Append(Environment.NewLine)
                    .Append("END TRY").Append(Environment.NewLine)
                    .Append("BEGIN CATCH").Append(Environment.NewLine)
                    .Append("END CATCH").Append(Environment.NewLine)
                    .Append("GO").Append(Environment.NewLine).Append(Environment.NewLine);
            }

            builder.AppendFormat("PRINT N'Completed merge static data to {0}';", table).Append(Environment.NewLine).Append(Environment.NewLine);
            return builder.ToString();
        }
        #endregion

        private string[] SortTablesByDependence(string[] tables)
        {
            if (tables == null) return tables;
            if (tables.Length <= 1) return tables;

            return SchemaInfo.Tables.SortByDependences(tables);
        }

        #region Protected Methods
        /// <summary>
        /// Generate the Static Data Migration Script for particular table.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        protected virtual string Generate(string table, MergeScriptOption option = MergeScriptOption.Default)
        {
            using (var data = this.GetDataTable(table))
                return this.Generate(data, option);
        }

        protected virtual void AddHeader(StringBuilder builder)
        {
            builder.Append("/*").Append(Environment.NewLine)
                .AppendFormat("Generated Date: {0:dd-MMM-yyyy hh:mm:ss}", DateTime.Now).Append(Environment.NewLine)
                .AppendFormat("Generated User: {0}", HBD.Framework.Core.IdentityHelper.UserName).Append(Environment.NewLine)
                .Append("*/").Append(Environment.NewLine).Append(Environment.NewLine);
        }

        protected virtual bool IsTableIdentity(string tableName)
        {
            var isIdentity = this.SchemaInfo.Tables[tableName]?.Columns.Any(c => c.IsPrimaryKey && c.IsIdentity);
            return isIdentity ?? false;
        }

        protected virtual DataTable GetDataTable(string table)
            => this.ExecuteTable($"SELECT * FROM {Common.GetSqlName(table)}");
        #endregion

        #region Static Methods
        //public static string GetSqlName(string name)
        //{
        //    if (string.IsNullOrWhiteSpace(name)) return name;

        //    if (name.Contains("."))
        //    {
        //        var split = name.Split('.');
        //        return string.Join(".", split.Select(GetSqlName));
        //    }
        //    else if (name.StartsWith("[") && name.EndsWith("]")) return name;
        //    return $"[{name}]";
        //}

        private static string BuildSqlFieldList(string[] fields)
        {
            var builder = new StringBuilder();
            if (fields.Length == 0) return builder.ToString();

            foreach (var f in fields)
            {
                if (builder.Length > 0) builder.Append(",");
                builder.Append(Common.GetSqlName(f));
            }

            return builder.ToString();
        }

        public static byte[] GetBytes(string value)
        {
            var shb = SoapHexBinary.Parse(value);
            return shb.Value;
        }

        public static string GetString(byte[] value)
        {
            var shb = new SoapHexBinary(value);
            return "0x" + shb;
        }

        private static string BuildSqlValueList(object[] items)
        {
            var builder = new StringBuilder();
            if (items.Length == 0) return builder.ToString();

            foreach (var obj in items)
            {
                if (builder.Length > 0) builder.Append(",");

                if (obj == null || obj == DBNull.Value)
                {
                    builder.Append("NULL");
                }
                else if (obj is byte[])
                {
                    var strByte = GetString((byte[])obj);
                    builder.Append(strByte);
                }
                else if (obj is bool)
                {
                    builder.Append(obj.Equals(true) ? 1 : 0);
                }
                else if (obj is DateTime)
                {
                    builder.AppendFormat("'{0}'", ((DateTime)obj).ToString("yyyy-MM-dd hh:mm:ss"));
                }
                else
                {
                    var strValue = obj.ToString().Replace("'", "''");
                    builder.AppendFormat(obj is string ? "N'{0}'" : "{0}", strValue);
                }
            }

            return builder.ToString();
        }
        #endregion
    }

    [Flags]
    public enum MergeScriptOption
    {
        Default = Insert | Update,
        All = Insert | Update | Delete,
        Insert = 2,
        Update = 4,
        Delete = 8
    }
}
