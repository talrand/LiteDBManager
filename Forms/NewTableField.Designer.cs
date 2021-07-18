
namespace LiteDBManager.Forms
{
    partial class frmNewTableField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewTableField));
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFieldType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDefaultValue = new System.Windows.Forms.TextBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            this.nudDefaultValue = new System.Windows.Forms.NumericUpDown();
            this.cboDefaultValue = new System.Windows.Forms.ComboBox();
            this.dtpDefaultValue = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultValue)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(93, 12);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(156, 20);
            this.txtFieldName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // cboFieldType
            // 
            this.cboFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFieldType.FormattingEnabled = true;
            this.cboFieldType.Location = new System.Drawing.Point(93, 38);
            this.cboFieldType.Name = "cboFieldType";
            this.cboFieldType.Size = new System.Drawing.Size(156, 21);
            this.cboFieldType.TabIndex = 3;
            this.cboFieldType.SelectedIndexChanged += new System.EventHandler(this.cboFieldType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Default Value";
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.Location = new System.Drawing.Point(93, 65);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(75, 20);
            this.txtDefaultValue.TabIndex = 5;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(174, 92);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 7;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(93, 92);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 23);
            this.butSave.TabIndex = 6;
            this.butSave.Text = "Save";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // nudDefaultValue
            // 
            this.nudDefaultValue.Location = new System.Drawing.Point(93, 65);
            this.nudDefaultValue.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudDefaultValue.Name = "nudDefaultValue";
            this.nudDefaultValue.Size = new System.Drawing.Size(75, 20);
            this.nudDefaultValue.TabIndex = 8;
            this.nudDefaultValue.Visible = false;
            // 
            // cboDefaultValue
            // 
            this.cboDefaultValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDefaultValue.FormattingEnabled = true;
            this.cboDefaultValue.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cboDefaultValue.Location = new System.Drawing.Point(93, 64);
            this.cboDefaultValue.Name = "cboDefaultValue";
            this.cboDefaultValue.Size = new System.Drawing.Size(75, 21);
            this.cboDefaultValue.TabIndex = 9;
            this.cboDefaultValue.Visible = false;
            // 
            // dtpDefaultValue
            // 
            this.dtpDefaultValue.CustomFormat = "";
            this.dtpDefaultValue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDefaultValue.Location = new System.Drawing.Point(93, 64);
            this.dtpDefaultValue.Name = "dtpDefaultValue";
            this.dtpDefaultValue.Size = new System.Drawing.Size(156, 20);
            this.dtpDefaultValue.TabIndex = 10;
            this.dtpDefaultValue.Visible = false;
            // 
            // frmNewTableField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 124);
            this.Controls.Add(this.dtpDefaultValue);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDefaultValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboFieldType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.cboDefaultValue);
            this.Controls.Add(this.nudDefaultValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewTableField";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Field";
            this.Load += new System.EventHandler(this.frmNewTableField_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDefaultValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboFieldType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDefaultValue;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.NumericUpDown nudDefaultValue;
        private System.Windows.Forms.ComboBox cboDefaultValue;
        private System.Windows.Forms.DateTimePicker dtpDefaultValue;
    }
}