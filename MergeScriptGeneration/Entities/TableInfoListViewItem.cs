using HBD.Framework.Data.Base;
using HBD.Framework.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMigrationScriptGenerationApplication.Entities
{
    public class TableInfoListViewItem : ListViewItem
    {
        public TableInfo Table { get; private set; }

        public TableInfoListViewItem(TableInfo table, int imageIndex) : this(table) { this.ImageIndex = imageIndex; }
        public TableInfoListViewItem(TableInfo table, string imageKey) : this(table) { this.ImageKey = imageKey; }
        public TableInfoListViewItem(TableInfo table)
        {
            this.Table = table;
            if (table == null) return;

            this.Name = table.Name;
            this.Text = $"{table.Name} ({table.TotalRecords})";
        }
    }
}
