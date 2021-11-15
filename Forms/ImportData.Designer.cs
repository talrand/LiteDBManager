
namespace LiteDBManager.Forms
{
    partial class frmImportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportData));
            this.dgvImport = new System.Windows.Forms.DataGridView();
            this.butCancel = new System.Windows.Forms.Button();
            this.butImport = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.importFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvImport
            // 
            this.dgvImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImport.Location = new System.Drawing.Point(16, 38);
            this.dgvImport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvImport.Name = "dgvImport";
            this.dgvImport.RowHeadersWidth = 51;
            this.dgvImport.Size = new System.Drawing.Size(1035, 469);
            this.dgvImport.TabIndex = 0;
            this.dgvImport.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvImport_KeyUp);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(951, 516);
            this.butCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(100, 28);
            this.butCancel.TabIndex = 2;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butImport
            // 
            this.butImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butImport.Location = new System.Drawing.Point(843, 516);
            this.butImport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butImport.Name = "butImport";
            this.butImport.Size = new System.Drawing.Size(100, 28);
            this.butImport.TabIndex = 1;
            this.butImport.Text = "Import";
            this.butImport.UseVisualStyleBackColor = true;
            this.butImport.Click += new System.EventHandler(this.butImport_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // importFromToolStripMenuItem
            // 
            this.importFromToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClipboard,
            this.mnuFile});
            this.importFromToolStripMenuItem.Name = "importFromToolStripMenuItem";
            this.importFromToolStripMenuItem.Size = new System.Drawing.Size(115, 24);
            this.importFromToolStripMenuItem.Text = "Import From...";
            // 
            // mnuClipboard
            // 
            this.mnuClipboard.Name = "mnuClipboard";
            this.mnuClipboard.Size = new System.Drawing.Size(224, 26);
            this.mnuClipboard.Text = "Clipboard";
            this.mnuClipboard.Click += new System.EventHandler(this.mnuClipboard_Click);
            // 
            // mnuFile
            // 
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(224, 26);
            this.mnuFile.Text = "File";
            this.mnuFile.Click += new System.EventHandler(this.mnuFile_Click);
            // 
            // frmImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.butImport);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.dgvImport);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmImportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Data";
            this.Load += new System.EventHandler(this.frmImportData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvImport;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butImport;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem importFromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuClipboard;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
    }
}