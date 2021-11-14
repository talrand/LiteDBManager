using System;
using System.Data;
using System.Windows.Forms;
using LiteDBManager.Classes.DataImport;
using static LiteDBManager.Classes.Globals;
using System.IO;

namespace LiteDBManager.Forms
{
    public partial class frmImportData : Form
    {
        private readonly Importer _importer = new Importer();
        private string _tableName = "";
        private bool _imported = false;
        private bool _autoLoadDataFromClipboard = false;

        public string TableName { set => _tableName = value; }

        public bool AutoLoadDataFromClipboard { set => _autoLoadDataFromClipboard = value; }

        public bool Imported { get => _imported; }

        public frmImportData()
        {
            InitializeComponent();
        }

        private void frmImportData_Load(object sender, EventArgs e)
        {
            DataTable dataTable;

            try
            {
                Text += $" into {_tableName} table";

                dataTable = _importer.CreateImportDataTable(_tableName);

                // Insert data already on clipboard
                if (_autoLoadDataFromClipboard)
                {
                    try
                    {
                        InsertRowsFromClipboard(dataTable);
                        return;
                    }
                    catch (InvalidDataException invalidDataEx)
                    {
                        MessageBox.Show(invalidDataEx.Message); ;
                    }
                }

                /* No data passed. Initialise grid with schema for passed table
                * This will allow users to manually input data if they wish */
                dgvImport.DataSource = dataTable;
                SetIdColumnAsReadOnly();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                InsertRowsFromClipboard();
            }
            catch (InvalidDataException invalidDataEx)
            {
                MessageBox.Show(invalidDataEx.Message);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void dgvImport_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // Shift + Insert or CTRL + V is pressed, insert rows from clipboard
                if (IsPasteKeys(e))
                {
                    InsertRowsFromClipboard();
                }
            }
            catch (InvalidDataException invalidDataEx)
            {
                MessageBox.Show(invalidDataEx.Message);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void InsertRowsFromClipboard()
        {
            InsertRowsFromClipboard((DataTable)dgvImport.DataSource);
        }

        private void InsertRowsFromClipboard(DataTable dataTable)
        {
            // Reset the grid's datasource, otherwise blank rows are added to the grid
            dgvImport.DataSource = null;
            dgvImport.DataSource = _importer.ReadDataFromClipboard(dataTable);
            SetIdColumnAsReadOnly();
        }

        private void SetIdColumnAsReadOnly()
        {
            if (dgvImport.Columns.Contains("_id"))
            {
                dgvImport.Columns["_id"].ReadOnly = true;
            }
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            try
            {
                // Import data in grid to passed table. This may error if the data isn't in the correct format
                _importer.ImportData(_tableName, (DataTable)dgvImport.DataSource);
                _imported = true;

                MessageBox.Show("Operation Complete");
                Close();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }
    }
}