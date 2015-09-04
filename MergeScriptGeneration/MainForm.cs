using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HBD.Framework.Extensions;
using HBD.Framework.Data.SqlClient;
using DataMigrationScriptGenerationApplication.Entities;
using HBD.WinForms.Base;
using HBD.WinForms.Extensions;
using HBD.SSDT.Extensions.Helpers;
using HBD.SSDT.Extensions;

namespace DataMigrationScriptGenerationApplication
{
    public partial class MainForm : FormBase
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public virtual string ConnectionString => this.toolTipConnectionBuilderControl1.ConnectionString;
        protected virtual SqlClientHelper CreateSqlClientHelper() => new SqlClientHelper(ConnectionString);
        private void queryBuilderControl1_Change(object sender, EventArgs e) => this.LoadListSchemaInfo();

        private void LoadListSchemaInfo()
        {
            this.listView1.Items.Clear();

            if (ConnectionString.IsNotNullOrEmpty())
            {
                using (var conn = CreateSqlClientHelper())
                {
                    var item = this.listView1.Items.Add("(All)", 1);
                    this.listView1.Groups[0].Items.Add(item);

                    foreach (var tb in conn.SchemaInfo.Tables.Where(t => t.TotalRecords > 0))
                    {
                        item = this.listView1.Items.Add(new TableInfoListViewItem(tb, 0));
                        this.listView1.Groups[0].Items.Add(item);
                    }
                }
            }

            this.btn_Generate.Enabled = this.listView1.Items.Count > 0;
        }

        private void EnabledControls(bool enabled)
        {
            this.toolTipConnectionBuilderControl1.Enabled = enabled;
            this.gr_ScriptOption.Enabled = enabled;
            this.gr_TablesOption.Enabled = enabled;
            this.btn_Generate.Enabled = enabled;
        }

        public override bool ValidateData()
        {
            this.ClearError();

            if (!this.ch_Insert.Checked && !this.ch_Update.Checked && !this.ch_Delete.Checked)
            {
                this.SetError(this.ch_Delete, "Please specify the Migration Script Options.");
                return false;
            }

            if (!this.ValidateRequiredControls(this.directoryBrowser1)) return false;

            if (this.listView1.CheckedItems.Count == 0)
            {
                this.ShowErrorMessage("Please specify the tables.");
                return false;
            }

            return true;
        }

        private void SetStatus(string title, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action<string, string>)this.SetStatus, title, text);
                return;
            }

            this.ss_Title.Text = title;
            this.ss_Text.Text = text;
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.listView1.ItemChecked -= listView1_ItemChecked;

            //The first Items Checked
            if (e.Item.Name.IsNullOrEmpty())
            {
                foreach (var item in this.listView1.Items.Cast<ListViewItem>().Where(item => item != e.Item))
                    item.Checked = e.Item.Checked;
            }
            else
            {
                if (!e.Item.Checked)
                    this.listView1.Items[0].Checked = false;
            }

            this.listView1.ItemChecked += listView1_ItemChecked;
            this.SetStatus("Selected Tables:", (this.listView1.CheckedItems.Count - (this.listView1.Items[0].Checked ? 1 : 0)).ToString());
        }

        private async void btn_Generate_Click(object sender, EventArgs e)
        {
            if (!this.ValidateData()) return;
            this.EnabledControls(false);

            var tables = this.listView1.CheckedItems.Cast<ListViewItem>().Where(i => i.Name.IsNotNullOrEmpty()).Select(i => i.Name).ToArray();
            var output = this.directoryBrowser1.SelectedPath;
            var connectionString = this.ConnectionString;
            var option = MergeScriptOption.Insert;
            if (this.ch_Insert.Checked) option = MergeScriptOption.Insert;
            if (this.ch_Update.Checked) option |= MergeScriptOption.Update;
            if (this.ch_Delete.Checked) option |= MergeScriptOption.Delete;

            if (System.IO.Directory.GetFiles(output, "*.sql").Length > 0
                && this.ShowConfirmationMessage("The output folder is not empty. Do you want to overwrite the *.sql file?") != DialogResult.Yes)
            {
                this.EnabledControls(true);
                return;
            }

            //Delete All Files in output folder.
            HBD.Framework.IO.Directory.DeleteFiles(output, "*.sql");

            using (var g = new MergeScriptGeneration(connectionString, output))
                await g.GenerateAsync(option, (status) => this.SetStatus("Generating table", status), tables);

            this.ShowInfoMessage($"Migration script had been generated for {tables.Length} tables.{Environment.NewLine}Saved location {output}");
            this.EnabledControls(true);
        }

        private void btn_About_Click(object sender, EventArgs e)
        {
            using (var about = new AboutBox())
                about.ShowDialog(this);
        }
    }
}
