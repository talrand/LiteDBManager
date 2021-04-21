using System;
using System.Windows.Forms;
using static LiteDBManager.Classes.LiteDBWrapper;

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
                dgvSchema.DataSource = GetTableSchema(_tableName);
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