
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
            this.treeTables = new System.Windows.Forms.TreeView();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpDatabaseExplorer = new System.Windows.Forms.GroupBox();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this.mnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpDatabaseExplorer.SuspendLayout();
            this.grpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.grpQuery.SuspendLayout();
            this.mnuGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenDatabase,
            this.mnuDisconnect});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(1320, 28);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuOpenDatabase
            // 
            this.mnuOpenDatabase.Name = "mnuOpenDatabase";
            this.mnuOpenDatabase.Size = new System.Drawing.Size(77, 24);
            this.mnuOpenDatabase.Text = "Connect";
            this.mnuOpenDatabase.Click += new System.EventHandler(this.mnuOpenDatabase_Click);
            // 
            // treeTables
            // 
            this.treeTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeTables.Location = new System.Drawing.Point(5, 23);
            this.treeTables.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeTables.Name = "treeTables";
            this.treeTables.Size = new System.Drawing.Size(203, 639);
            this.treeTables.TabIndex = 0;
            this.treeTables.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeTables_NodeMouseDoubleClick);
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(8, 23);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(1066, 107);
            this.txtQuery.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 28);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpDatabaseExplorer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpResults);
            this.splitContainer.Panel2.Controls.Add(this.grpQuery);
            this.splitContainer.Size = new System.Drawing.Size(1320, 670);
            this.splitContainer.SplitterDistance = 221;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 3;
            // 
            // grpDatabaseExplorer
            // 
            this.grpDatabaseExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatabaseExplorer.Controls.Add(this.treeTables);
            this.grpDatabaseExplorer.Location = new System.Drawing.Point(4, 0);
            this.grpDatabaseExplorer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDatabaseExplorer.Name = "grpDatabaseExplorer";
            this.grpDatabaseExplorer.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDatabaseExplorer.Size = new System.Drawing.Size(217, 670);
            this.grpDatabaseExplorer.TabIndex = 0;
            this.grpDatabaseExplorer.TabStop = false;
            this.grpDatabaseExplorer.Text = "Database Explorer";
            // 
            // grpResults
            // 
            this.grpResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResults.Controls.Add(this.dgvResults);
            this.grpResults.Location = new System.Drawing.Point(3, 183);
            this.grpResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpResults.Name = "grpResults";
            this.grpResults.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpResults.Size = new System.Drawing.Size(1087, 487);
            this.grpResults.TabIndex = 1;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // dgvResults
            // 
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(8, 23);
            this.dgvResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.Size = new System.Drawing.Size(1071, 456);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellLeave);
            this.dgvResults.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellValueChanged);
            this.dgvResults.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_RowLeave);
            this.dgvResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvResults_MouseClick);
            // 
            // grpQuery
            // 
            this.grpQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpQuery.Controls.Add(this.btnExecuteQuery);
            this.grpQuery.Controls.Add(this.txtQuery);
            this.grpQuery.Location = new System.Drawing.Point(3, 0);
            this.grpQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpQuery.Size = new System.Drawing.Size(1083, 176);
            this.grpQuery.TabIndex = 0;
            this.grpQuery.TabStop = false;
            this.grpQuery.Text = "Query";
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteQuery.Location = new System.Drawing.Point(975, 139);
            this.btnExecuteQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(100, 28);
            this.btnExecuteQuery.TabIndex = 1;
            this.btnExecuteQuery.Text = "Run";
            this.btnExecuteQuery.UseVisualStyleBackColor = true;
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            // 
            // mnuGrid
            // 
            this.mnuGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeleteRow});
            this.mnuGrid.Name = "mnuGrid";
            this.mnuGrid.Size = new System.Drawing.Size(156, 28);
            this.mnuGrid.Opening += new System.ComponentModel.CancelEventHandler(this.mnuGrid_Opening);
            // 
            // mnuDeleteRow
            // 
            this.mnuDeleteRow.Name = "mnuDeleteRow";
            this.mnuDeleteRow.Size = new System.Drawing.Size(155, 24);
            this.mnuDeleteRow.Text = "Delete Row";
            this.mnuDeleteRow.Click += new System.EventHandler(this.mnuDeleteRow_Click);
            // 
            // mnuDisconnect
            // 
            this.mnuDisconnect.Enabled = false;
            this.mnuDisconnect.Name = "mnuDisconnect";
            this.mnuDisconnect.Size = new System.Drawing.Size(96, 24);
            this.mnuDisconnect.Text = "Disconnect";
            this.mnuDisconnect.Click += new System.EventHandler(this.mnuDisconnect_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 698);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.grpResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.grpQuery.ResumeLayout(false);
            this.grpQuery.PerformLayout();
            this.mnuGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenDatabase;
        private System.Windows.Forms.TreeView treeTables;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.GroupBox grpDatabaseExplorer;
        private System.Windows.Forms.ContextMenuStrip mnuGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteRow;
        private System.Windows.Forms.Button btnExecuteQuery;
        private System.Windows.Forms.ToolStripMenuItem mnuDisconnect;
    }
}

