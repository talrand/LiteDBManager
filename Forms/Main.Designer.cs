
namespace LiteDBManager
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuOpenDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQueries = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseCurrentQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAllButCurrentQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAllQueries = new System.Windows.Forms.ToolStripMenuItem();
            this.treeTables = new System.Windows.Forms.TreeView();
            this.imgDatabaseExplorer = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpDatabaseExplorer = new System.Windows.Forms.GroupBox();
            this.tabQueries = new System.Windows.Forms.TabControl();
            this.mnuDatabaseTables = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewTableSchema = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpDatabaseExplorer.SuspendLayout();
            this.mnuDatabaseTables.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenDatabase,
            this.mnuDisconnect,
            this.mnuNewQuery,
            this.mnuQueries});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(990, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuOpenDatabase
            // 
            this.mnuOpenDatabase.Name = "mnuOpenDatabase";
            this.mnuOpenDatabase.Size = new System.Drawing.Size(64, 20);
            this.mnuOpenDatabase.Text = "Connect";
            this.mnuOpenDatabase.Click += new System.EventHandler(this.mnuOpenDatabase_Click);
            // 
            // mnuDisconnect
            // 
            this.mnuDisconnect.Enabled = false;
            this.mnuDisconnect.Name = "mnuDisconnect";
            this.mnuDisconnect.Size = new System.Drawing.Size(78, 20);
            this.mnuDisconnect.Text = "Disconnect";
            this.mnuDisconnect.Click += new System.EventHandler(this.mnuDisconnect_Click);
            // 
            // mnuNewQuery
            // 
            this.mnuNewQuery.Enabled = false;
            this.mnuNewQuery.Name = "mnuNewQuery";
            this.mnuNewQuery.Size = new System.Drawing.Size(78, 20);
            this.mnuNewQuery.Text = "New Query";
            this.mnuNewQuery.Click += new System.EventHandler(this.mnuNewQuery_Click);
            // 
            // mnuQueries
            // 
            this.mnuQueries.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCloseCurrentQuery,
            this.mnuCloseAllButCurrentQuery,
            this.mnuCloseAllQueries});
            this.mnuQueries.Enabled = false;
            this.mnuQueries.Name = "mnuQueries";
            this.mnuQueries.Size = new System.Drawing.Size(85, 20);
            this.mnuQueries.Text = "Query Panes";
            this.mnuQueries.DropDownOpening += new System.EventHandler(this.mnuQueries_DropDownOpening);
            // 
            // mnuCloseCurrentQuery
            // 
            this.mnuCloseCurrentQuery.Name = "mnuCloseCurrentQuery";
            this.mnuCloseCurrentQuery.Size = new System.Drawing.Size(184, 22);
            this.mnuCloseCurrentQuery.Text = "Close Current";
            this.mnuCloseCurrentQuery.Click += new System.EventHandler(this.mnuCloseCurrentQuery_Click);
            // 
            // mnuCloseAllButCurrentQuery
            // 
            this.mnuCloseAllButCurrentQuery.Name = "mnuCloseAllButCurrentQuery";
            this.mnuCloseAllButCurrentQuery.Size = new System.Drawing.Size(184, 22);
            this.mnuCloseAllButCurrentQuery.Text = "Close All But Current";
            this.mnuCloseAllButCurrentQuery.Click += new System.EventHandler(this.mnuCloseAllButCurrentQuery_Click);
            // 
            // mnuCloseAllQueries
            // 
            this.mnuCloseAllQueries.Name = "mnuCloseAllQueries";
            this.mnuCloseAllQueries.Size = new System.Drawing.Size(184, 22);
            this.mnuCloseAllQueries.Text = "Close All";
            this.mnuCloseAllQueries.Click += new System.EventHandler(this.mnuCloseAllQueries_Click);
            // 
            // treeTables
            // 
            this.treeTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeTables.ImageIndex = 2;
            this.treeTables.ImageList = this.imgDatabaseExplorer;
            this.treeTables.Location = new System.Drawing.Point(4, 19);
            this.treeTables.Name = "treeTables";
            this.treeTables.SelectedImageIndex = 0;
            this.treeTables.Size = new System.Drawing.Size(207, 607);
            this.treeTables.TabIndex = 0;
            this.treeTables.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeTables_NodeMouseClick);
            this.treeTables.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeTables_NodeMouseDoubleClick);
            // 
            // imgDatabaseExplorer
            // 
            this.imgDatabaseExplorer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgDatabaseExplorer.ImageStream")));
            this.imgDatabaseExplorer.TransparentColor = System.Drawing.Color.Transparent;
            this.imgDatabaseExplorer.Images.SetKeyName(0, "database.png");
            this.imgDatabaseExplorer.Images.SetKeyName(1, "folder.png");
            this.imgDatabaseExplorer.Images.SetKeyName(2, "table.png");
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpDatabaseExplorer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabQueries);
            this.splitContainer.Size = new System.Drawing.Size(990, 632);
            this.splitContainer.SplitterDistance = 221;
            this.splitContainer.TabIndex = 3;
            // 
            // grpDatabaseExplorer
            // 
            this.grpDatabaseExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatabaseExplorer.Controls.Add(this.treeTables);
            this.grpDatabaseExplorer.Location = new System.Drawing.Point(3, 0);
            this.grpDatabaseExplorer.Name = "grpDatabaseExplorer";
            this.grpDatabaseExplorer.Size = new System.Drawing.Size(218, 632);
            this.grpDatabaseExplorer.TabIndex = 0;
            this.grpDatabaseExplorer.TabStop = false;
            this.grpDatabaseExplorer.Text = "Database Explorer";
            // 
            // tabQueries
            // 
            this.tabQueries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabQueries.Location = new System.Drawing.Point(0, 0);
            this.tabQueries.Name = "tabQueries";
            this.tabQueries.SelectedIndex = 0;
            this.tabQueries.Size = new System.Drawing.Size(765, 632);
            this.tabQueries.TabIndex = 0;
            this.tabQueries.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabQueries_MouseClick);
            // 
            // mnuDatabaseTables
            // 
            this.mnuDatabaseTables.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewTableSchema,
            this.mnuDeleteTable});
            this.mnuDatabaseTables.Name = "mnuDatabaseTables";
            this.mnuDatabaseTables.Size = new System.Drawing.Size(145, 48);
            // 
            // mnuViewTableSchema
            // 
            this.mnuViewTableSchema.Name = "mnuViewTableSchema";
            this.mnuViewTableSchema.Size = new System.Drawing.Size(144, 22);
            this.mnuViewTableSchema.Text = "View Schema";
            this.mnuViewTableSchema.Click += new System.EventHandler(this.mnuViewTableSchema_Click);
            // 
            // mnuDeleteTable
            // 
            this.mnuDeleteTable.Name = "mnuDeleteTable";
            this.mnuDeleteTable.Size = new System.Drawing.Size(144, 22);
            this.mnuDeleteTable.Text = "Delete Table";
            this.mnuDeleteTable.Click += new System.EventHandler(this.mnuDeleteTable_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 656);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiteDB Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpDatabaseExplorer.ResumeLayout(false);
            this.mnuDatabaseTables.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenDatabase;
        private System.Windows.Forms.TreeView treeTables;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpDatabaseExplorer;
        private System.Windows.Forms.ToolStripMenuItem mnuDisconnect;
        private System.Windows.Forms.ToolStripMenuItem mnuNewQuery;
        private System.Windows.Forms.TabControl tabQueries;
        private System.Windows.Forms.ToolStripMenuItem mnuQueries;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseCurrentQuery;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseAllButCurrentQuery;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseAllQueries;
        private System.Windows.Forms.ImageList imgDatabaseExplorer;
        private System.Windows.Forms.ContextMenuStrip mnuDatabaseTables;
        private System.Windows.Forms.ToolStripMenuItem mnuViewTableSchema;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteTable;
    }
}

