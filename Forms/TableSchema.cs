using System;
using System.Windows.Forms;
using LiteDBManager.Classes.Database;

namespace LiteDBManager.Forms
{
    public partial class frmTableSchema : Form
    {
        private string _tableName = "";

        public string TableName { set { _tableName = value; } }

        public frmTableSchema()
        {
            InitializeComponent();
        }

        private void frmTableSchema_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += $" - {_tableName}";
            }
            catch (Exception ex)
            {
                new frmSystemError {Exception = ex}.ShowDialog();
            }
        }

        private void frmTableSchema_Shown(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();

                // Only show add button if database isn't in read only mode
                butAddField.Visible = !LiteDBWrapper.DatabaseReadOnly;
            }
            catch (Exception ex)
            {
                new frmSystemError { Exception = ex }.ShowDialog();
            }
        }

        private void PopulateGrid()
        {
            var tableReader = new TableReader();
            dgvSchema.DataSource = tableReader.ReadSchema(_tableName);
        }

        private void butAddField_Click(object sender, EventArgs e)
        {
            var newTableField = new frmNewTableField();

            try
            {
                newTableField.TableName = _tableName;
                newTableField.ShowDialog();

                if (newTableField.Saved)
                {
                    PopulateGrid();
                }
            }
            catch (Exception ex)
            {
                new frmSystemError { Exception = ex }.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                new frmSystemError { Exception = ex }.ShowDialog();
            }
        }
    }
}