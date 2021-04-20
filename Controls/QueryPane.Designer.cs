
namespace LiteDBManager.Controls
{
    partial class QueryPane
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.panQueryResults = new System.Windows.Forms.Panel();
            this.lblExecuteResults = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.txtNonQueryResult = new System.Windows.Forms.TextBox();
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.mnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportResults = new System.Windows.Forms.ToolStripMenuItem();
            this.grpResults.SuspendLayout();
            this.panQueryResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.grpQuery.SuspendLayout();
            this.mnuGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResults
            // 
            this.grpResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResults.Controls.Add(this.panQueryResults);
            this.grpResults.Controls.Add(this.txtNonQueryResult);
            this.grpResults.Location = new System.Drawing.Point(3, 152);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(725, 287);
            this.grpResults.TabIndex = 3;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // panQueryResults
            // 
            this.panQueryResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panQueryResults.Controls.Add(this.lblExecuteResults);
            this.panQueryResults.Controls.Add(this.dgvResults);
            this.panQueryResults.Location = new System.Drawing.Point(6, 19);
            this.panQueryResults.Name = "panQueryResults";
            this.panQueryResults.Size = new System.Drawing.Size(712, 262);
            this.panQueryResults.TabIndex = 3;
            // 
            // lblExecuteResults
            // 
            this.lblExecuteResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExecuteResults.Location = new System.Drawing.Point(0, 243);
            this.lblExecuteResults.Name = "lblExecuteResults";
            this.lblExecuteResults.Size = new System.Drawing.Size(709, 18);
            this.lblExecuteResults.TabIndex = 1;
            this.lblExecuteResults.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvResults
            // 
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.Size = new System.Drawing.Size(709, 240);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellEndEdit);
            this.dgvResults.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellLeave);
            this.dgvResults.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellValueChanged);
            this.dgvResults.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvResults_DataError);
            this.dgvResults.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvResults_DefaultValuesNeeded);
            this.dgvResults.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_RowLeave);
            this.dgvResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvResults_MouseClick);
            // 
            // txtNonQueryResult
            // 
            this.txtNonQueryResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNonQueryResult.Location = new System.Drawing.Point(6, 19);
            this.txtNonQueryResult.Multiline = true;
            this.txtNonQueryResult.Name = "txtNonQueryResult";
            this.txtNonQueryResult.ReadOnly = true;
            this.txtNonQueryResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNonQueryResult.Size = new System.Drawing.Size(713, 262);
            this.txtNonQueryResult.TabIndex = 2;
            this.txtNonQueryResult.Visible = false;
            // 
            // grpQuery
            // 
            this.grpQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpQuery.Controls.Add(this.btnExecuteQuery);
            this.grpQuery.Controls.Add(this.txtQuery);
            this.grpQuery.Location = new System.Drawing.Point(3, 3);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(725, 143);
            this.grpQuery.TabIndex = 2;
            this.grpQuery.TabStop = false;
            this.grpQuery.Text = "Query";
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteQuery.Location = new System.Drawing.Point(643, 113);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(75, 23);
            this.btnExecuteQuery.TabIndex = 1;
            this.btnExecuteQuery.Text = "Run";
            this.btnExecuteQuery.UseVisualStyleBackColor = true;
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(6, 19);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(713, 88);
            this.txtQuery.TabIndex = 0;
            // 
            // mnuGrid
            // 
            this.mnuGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeleteRow,
            this.mnuExportResults});
            this.mnuGrid.Name = "mnuGrid";
            this.mnuGrid.Size = new System.Drawing.Size(149, 48);
            this.mnuGrid.Opening += new System.ComponentModel.CancelEventHandler(this.mnuGrid_Opening);
            // 
            // mnuDeleteRow
            // 
            this.mnuDeleteRow.Name = "mnuDeleteRow";
            this.mnuDeleteRow.Size = new System.Drawing.Size(148, 22);
            this.mnuDeleteRow.Text = "Delete Row";
            this.mnuDeleteRow.Click += new System.EventHandler(this.mnuDeleteRow_Click);
            // 
            // mnuExportResults
            // 
            this.mnuExportResults.Name = "mnuExportResults";
            this.mnuExportResults.Size = new System.Drawing.Size(148, 22);
            this.mnuExportResults.Text = "Export Results";
            this.mnuExportResults.Click += new System.EventHandler(this.mnuExportResults_Click);
            // 
            // QueryPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.grpQuery);
            this.Name = "QueryPane";
            this.Size = new System.Drawing.Size(731, 442);
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            this.panQueryResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.grpQuery.ResumeLayout(false);
            this.grpQuery.PerformLayout();
            this.mnuGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.Button btnExecuteQuery;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.ContextMenuStrip mnuGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteRow;
        private System.Windows.Forms.TextBox txtNonQueryResult;
        private System.Windows.Forms.Panel panQueryResults;
        private System.Windows.Forms.Label lblExecuteResults;
        private System.Windows.Forms.ToolStripMenuItem mnuExportResults;
    }
}
