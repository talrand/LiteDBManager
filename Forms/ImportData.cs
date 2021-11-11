using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiteDBManager.Classes.DataImport;

namespace LiteDBManager.Forms
{
    public partial class frmImportData : Form
    {
        private string _tableName = "";
        private readonly Importer _importer = new Importer();

        public string TableName { set => _tableName = value; }

        public frmImportData()
        {
            InitializeComponent();
        }

        private void frmImportData_Load(object sender, EventArgs e)
        {
            try
            {
                dgvImport.DataSource = _importer.CreateImportDataTable(_tableName);
            }
            catch (Exception ex)
            {
                new frmSystemError { Exception = ex };
            }
        }
    }
}
