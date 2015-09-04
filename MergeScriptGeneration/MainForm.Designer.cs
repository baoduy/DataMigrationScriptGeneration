namespace DataMigrationScriptGenerationApplication
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Tables", System.Windows.Forms.HorizontalAlignment.Left);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btn_About = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Generate = new System.Windows.Forms.ToolStripButton();
            this.toolTipConnectionBuilderControl1 = new HBD.WinForms.UserControls.ToolTipConnectionBuilderControl();
            this.gr_TablesOption = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ch_Delete = new System.Windows.Forms.CheckBox();
            this.ch_Update = new System.Windows.Forms.CheckBox();
            this.ch_Insert = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ss_Title = new System.Windows.Forms.ToolStripStatusLabel();
            this.ss_Text = new System.Windows.Forms.ToolStripStatusLabel();
            this.gr_ScriptOption = new System.Windows.Forms.GroupBox();
            this.directoryBrowser1 = new HBD.WinForms.UserControls.DirectoryBrowser();
            this.toolStrip.SuspendLayout();
            this.gr_TablesOption.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gr_ScriptOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_About,
            this.toolStripSeparator1,
            this.btn_Generate,
            this.toolTipConnectionBuilderControl1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(601, 32);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // btn_About
            // 
            this.btn_About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_About.Image = ((System.Drawing.Image)(resources.GetObject("btn_About.Image")));
            this.btn_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_About.Name = "btn_About";
            this.btn_About.Size = new System.Drawing.Size(23, 29);
            this.btn_About.Text = "About";
            this.btn_About.Click += new System.EventHandler(this.btn_About_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // btn_Generate
            // 
            this.btn_Generate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btn_Generate.Enabled = false;
            this.btn_Generate.Image = ((System.Drawing.Image)(resources.GetObject("btn_Generate.Image")));
            this.btn_Generate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(74, 29);
            this.btn_Generate.Text = "Generate";
            this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // toolTipConnectionBuilderControl1
            // 
            this.toolTipConnectionBuilderControl1.CreateNewText = "...";
            this.toolTipConnectionBuilderControl1.Name = "toolTipConnectionBuilderControl1";
            this.toolTipConnectionBuilderControl1.Size = new System.Drawing.Size(453, 29);
            this.toolTipConnectionBuilderControl1.Text = "toolTipConnectionBuilderControl1";
            this.toolTipConnectionBuilderControl1.Title = "Connection";
            this.toolTipConnectionBuilderControl1.UpdateText = "...";
            this.toolTipConnectionBuilderControl1.Change += new System.EventHandler(this.queryBuilderControl1_Change);
            // 
            // gr_TablesOption
            // 
            this.gr_TablesOption.Controls.Add(this.listView1);
            this.gr_TablesOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gr_TablesOption.Location = new System.Drawing.Point(0, 115);
            this.gr_TablesOption.Name = "gr_TablesOption";
            this.gr_TablesOption.Size = new System.Drawing.Size(601, 424);
            this.gr_TablesOption.TabIndex = 2;
            this.gr_TablesOption.TabStop = false;
            this.gr_TablesOption.Text = "Tables";
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Tables";
            listViewGroup1.Name = "listViewGroupTable";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listView1.Location = new System.Drawing.Point(3, 16);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(595, 405);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table16.png");
            this.imageList1.Images.SetKeyName(1, "bullet-green.png");
            // 
            // ch_Delete
            // 
            this.ch_Delete.AutoSize = true;
            this.ch_Delete.Location = new System.Drawing.Point(191, 24);
            this.ch_Delete.Name = "ch_Delete";
            this.ch_Delete.Size = new System.Drawing.Size(57, 17);
            this.ch_Delete.TabIndex = 2;
            this.ch_Delete.Text = "Delete";
            this.toolTip1.SetToolTip(this.ch_Delete, "Generate Delete statement in Migration Script");
            this.ch_Delete.UseVisualStyleBackColor = true;
            // 
            // ch_Update
            // 
            this.ch_Update.AutoSize = true;
            this.ch_Update.Checked = true;
            this.ch_Update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_Update.Location = new System.Drawing.Point(105, 24);
            this.ch_Update.Name = "ch_Update";
            this.ch_Update.Size = new System.Drawing.Size(61, 17);
            this.ch_Update.TabIndex = 1;
            this.ch_Update.Text = "Update";
            this.toolTip1.SetToolTip(this.ch_Update, "Generate Update statement in Migration Script");
            this.ch_Update.UseVisualStyleBackColor = true;
            // 
            // ch_Insert
            // 
            this.ch_Insert.AutoSize = true;
            this.ch_Insert.Checked = true;
            this.ch_Insert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_Insert.Location = new System.Drawing.Point(19, 24);
            this.ch_Insert.Name = "ch_Insert";
            this.ch_Insert.Size = new System.Drawing.Size(52, 17);
            this.ch_Insert.TabIndex = 0;
            this.ch_Insert.Text = "Insert";
            this.toolTip1.SetToolTip(this.ch_Insert, "Generate Insert statement in Migration Script");
            this.ch_Insert.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss_Title,
            this.ss_Text});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(601, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ss_Title
            // 
            this.ss_Title.Name = "ss_Title";
            this.ss_Title.Size = new System.Drawing.Size(42, 17);
            this.ss_Title.Text = "Status:";
            // 
            // ss_Text
            // 
            this.ss_Text.Name = "ss_Text";
            this.ss_Text.Size = new System.Drawing.Size(39, 17);
            this.ss_Text.Text = "Ready";
            // 
            // gr_ScriptOption
            // 
            this.gr_ScriptOption.Controls.Add(this.directoryBrowser1);
            this.gr_ScriptOption.Controls.Add(this.ch_Delete);
            this.gr_ScriptOption.Controls.Add(this.ch_Update);
            this.gr_ScriptOption.Controls.Add(this.ch_Insert);
            this.gr_ScriptOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.gr_ScriptOption.Location = new System.Drawing.Point(0, 32);
            this.gr_ScriptOption.Name = "gr_ScriptOption";
            this.gr_ScriptOption.Size = new System.Drawing.Size(601, 83);
            this.gr_ScriptOption.TabIndex = 4;
            this.gr_ScriptOption.TabStop = false;
            this.gr_ScriptOption.Text = "Migration Script Option";
            // 
            // directoryBrowser1
            // 
            this.directoryBrowser1.BrowseText = "...";
            this.directoryBrowser1.Location = new System.Drawing.Point(12, 47);
            this.directoryBrowser1.Name = "directoryBrowser1";
            this.directoryBrowser1.Size = new System.Drawing.Size(437, 31);
            this.directoryBrowser1.TabIndex = 3;
            this.directoryBrowser1.Title = "Output Folder";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 561);
            this.Controls.Add(this.gr_TablesOption);
            this.Controls.Add(this.gr_ScriptOption);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "MainForm";
            this.Text = "Static Data Migration Script Generation";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.gr_TablesOption.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gr_ScriptOption.ResumeLayout(false);
            this.gr_ScriptOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.GroupBox gr_TablesOption;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton btn_Generate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton btn_About;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox gr_ScriptOption;
        private HBD.WinForms.UserControls.DirectoryBrowser directoryBrowser1;
        private System.Windows.Forms.CheckBox ch_Delete;
        private System.Windows.Forms.CheckBox ch_Update;
        private System.Windows.Forms.CheckBox ch_Insert;
        private System.Windows.Forms.ListView listView1;
        private HBD.WinForms.UserControls.ToolTipConnectionBuilderControl toolTipConnectionBuilderControl1;
        private System.Windows.Forms.ToolStripStatusLabel ss_Title;
        private System.Windows.Forms.ToolStripStatusLabel ss_Text;
    }
}

