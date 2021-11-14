using System;
using System.Reflection;
using System.Windows.Forms;
using LiteDBManager.Classes.Database;
using System.Drawing;
using LiteDBManager.Forms;
using Talrand.Core;
using System.IO;
using static LiteDBManager.Classes.Database.LiteDBWrapper;
using static LiteDBManager.Classes.DataGridViewExtensions;
using LiteDBManager.Classes.Exporters;
using static LiteDBManager.Classes.Globals;

namespace LiteDBManager.Controls
{
    public partial class QueryPane : UserControl
    {
        private string _currentTable = "";
        private readonly CommandExecutor _commandExecutor = new CommandExecutor();
        private bool _repositionToEndOfGrid = false;

        public QueryPane()
        {
            InitializeComponent();

            try
            {
                // Set double buffered field to improve visual performance of grid
                typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dgvResults, new object[] { true });
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        public void SetQueryText(string query)
        {
            txtQuery.Text = query;
        }

        public void Clear()
        {
            txtQuery.Text = "";
            dgvResults.DataSource = null;
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQuery.Text.Length == 0)
                {
                    return;
                }

                if (IsSelectQuery())
                {
                    // Populate datagrid from select query
                    ExecuteSelectQuery();
                }
                else
                {
                    // Run non-query command
                    ExecuteNonQueryCommand();
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private bool IsSelectQuery()
        {
            if (txtQuery.Text.Length < 6) return false;

            return txtQuery.Text.Substring(0, 6).Equals("SELECT", StringComparison.OrdinalIgnoreCase);
        }

        private void ExecuteSelectQuery()
        {
            GetCurrentTableNameFromSelectQuery();
            PopulateGridFromSelectQuery();

            panQueryResults.Visible = true;
            txtNonQueryResult.Visible = false;
        }

        private void GetCurrentTableNameFromSelectQuery()
        {
            string[] words = null;
            bool fromKeyWordFound = false;

            words = txtQuery.Text.Split(new string[] { " " }, StringSplitOptions.None);

            foreach (string word in words)
            {
                // FROM keyword found - store current word as table name
                if (fromKeyWordFound == true)
                {
                    _currentTable = word.Trim();
                    break;
                }

                // If current word is the FROM keyword, next word is the table name
                if (word.Trim().Equals("FROM", StringComparison.InvariantCultureIgnoreCase))
                {
                    fromKeyWordFound = true;
                    continue;
                }
            }
        }

        private void PopulateGridFromSelectQuery()
        {
            ExecuteResult result = null;

            dgvResults.DataSource = null;
            result = _commandExecutor.ExecuteQuery(txtQuery.Text);

            // Update grid data
            dgvResults.DataSource = result.Data;

            // Display execution info
            lblExecuteResults.Text = $"{result.Count} rows | Elapsed time {result.ElapsedTime}";

            // Database in readonly mode
            if (DatabaseReadOnly)
            {
                dgvResults.ReadOnly = true;
                return;
            }

            // _id column not present - set grid to read only to prevent errors when updating cells / inserting new rows
            if (dgvResults.Columns.Contains("_id") == false)
            {
                dgvResults.ReadOnly = true;
                return;
            }

            // Stop users editing internal _id column
            dgvResults.Columns["_id"].ReadOnly = true;
            dgvResults.ReadOnly = false;

            if (_repositionToEndOfGrid)
            {
                SelectLastGridRow();
                _repositionToEndOfGrid = false;
            }
        }

        private void SelectLastGridRow()
        {
            int rowIndex = dgvResults.Rows.Count - 2;

            dgvResults.ClearSelection();
            dgvResults.CurrentCell = dgvResults.Rows[rowIndex].Cells[0];
            dgvResults.Rows[rowIndex].Selected = true;
        }

        private void ExecuteNonQueryCommand()
        {
            ExecuteResult executeResult = null;

            try
            {
                txtNonQueryResult.Visible = true;
                panQueryResults.Visible = false;

                executeResult = _commandExecutor.ExecuteNonQuery(txtQuery.Text);

                // Display results
                txtNonQueryResult.ForeColor = Color.Black;
                txtNonQueryResult.Text = $"Command completed successfully.{Environment.NewLine + Environment.NewLine}{executeResult.Count} row(s) affected in {executeResult.ElapsedTime}";

            }
            catch (Exception ex)
            {
                txtNonQueryResult.ForeColor = Color.Red;
                txtNonQueryResult.Text = ex.Message;
            }
        }

        private void dgvResults_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            var insertCommand = new InsertCommandBuilder();

            try
            {
                // Grid is readonly
                if (dgvResults.ReadOnly) return;

                if (!dgvResults.Columns.Contains("_id")) return;

                // Ignore new rows that are being cancelled
                if (dgvResults.Rows[e.RowIndex].Cells["_id"].Value == null) return;

                // Ignore existing rows
                if (dgvResults.Rows[e.RowIndex].Cells["_id"].Value.ToString() != "") return;

                // Don't insert blank rows
                if (IsGridRowBlank(dgvResults.Rows[e.RowIndex]) || HasGridRowOnlyDefaultValues(dgvResults.Rows[e.RowIndex])) return;

                // Exit if there's currently errors on the rows
                if (dgvResults.Rows[e.RowIndex].ErrorText != "") return;

                // Build insert command
                insertCommand.SetTableName(_currentTable);

                foreach (DataGridViewCell cell in dgvResults.Rows[e.RowIndex].Cells)
                {
                    // Ignore _id column as this will be automatically populated
                    if (dgvResults.Columns[cell.ColumnIndex].Name == "_id")
                    {
                        continue;
                    }

                    insertCommand.AddField(dgvResults.Columns[cell.ColumnIndex].Name, cell.Value);
                }

                // Run insert command
                _commandExecutor.ExecuteNonQuery(insertCommand.ToString());

                _repositionToEndOfGrid = true;

                // Re-run query command - needs to use BeginInvoke call to avoid reentrant errors
                BeginInvoke(new MethodInvoker(PopulateGridFromSelectQuery));
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private bool IsGridRowBlank(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                // As soon as a cell contains a value return false
                if (String.IsNullOrEmpty(cell.Value.ToString()) == false)
                {
                    return false;
                }
            }

            // All cells are blank
            return true;
        }

        private bool HasGridRowOnlyDefaultValues(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                // Ignore null values
                if (cell.Value.GetType().Equals(typeof(DBNull)))
                {
                    continue;
                }

                // As soon as a cell contains a non-default value return false
                if (cell.Value.Equals(cell.DefaultValue()) == false)
                {
                    return false;
                }
            }

            // All cells are default values
            return true;
        }

        private void dgvResults_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Finish editing cell before leaving.
                // This resolves issues with null values being inserted after editing a value in a new row and switching rows
                if (dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode == true)
                {
                    dgvResults.EndEdit(DataGridViewDataErrorContexts.Formatting);
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void dgvResults_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = null;
            string columnName = "";
            string id = null;
            var updateCommand = new UpdateCommandBuilder();

            try
            {
                if (dgvResults.ReadOnly) return;

                // Get values from grid
                cell = dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex];
                columnName = dgvResults.Columns[e.ColumnIndex].Name;
                id = dgvResults.Rows[e.RowIndex].Cells["_id"].Value?.ToString();

                // No id means we're adding a new row
                if (String.IsNullOrEmpty(id)) return;

                // Perform update 
                updateCommand.SetTableName(_currentTable);
                updateCommand.AddField(columnName, cell.Value);
                updateCommand.SetWhereClause($"_id={FormatIdFieldForWhereClause(id)}");

                _commandExecutor.ExecuteNonQuery(updateCommand.ToString());
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void dgvResults_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                foreach (DataGridViewCell cell in e.Row.Cells)
                {
                    // Ignore _id column
                    if (dgvResults.Columns[cell.ColumnIndex].Name == "_id")
                    {
                        continue;
                    }

                    cell.Value = cell.DefaultValue();
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void dgvResults_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                // Display error to user
                dgvResults.Rows[e.RowIndex].ErrorText = e.Exception.Message;
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void dgvResults_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Reset error text - just in case user presses ESC whilst editing a cell
                dgvResults.Rows[e.RowIndex].ErrorText = "";
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void dgvResults_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    mnuGrid.Show(Cursor.Position);
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuGrid_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // Disable menu items if no rows in grid
                if (dgvResults.Rows.Count == 0)
                {
                    mnuDeleteRow.Enabled = false;
                    mnuExportResults.Enabled = false;
                    return;
                }

                mnuDeleteRow.Enabled = !dgvResults.ReadOnly;
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuDeleteRow_Click(object sender, EventArgs e)
        {
            string id = "";

            try
            {
                if (dgvResults.CurrentRow == null) return;

                id = dgvResults.CurrentRow.Cells["_id"].Value.ToString();

                // Don't attempt to delete 'New Row'
                if (id == "") return;

                if (MessageBox.Show("Are you sure you want to delete this row?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Delete row and repopulate grid
                    _commandExecutor.ExecuteNonQuery($"DELETE {_currentTable} WHERE _id = {FormatIdFieldForWhereClause(id)}");
                    PopulateGridFromSelectQuery();
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuExportResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            IDataExporter dataExporter = null;

            try
            {
                // Ask user where they would like to save the file
                saveFileDialog.Filter = "CSV|*.csv|json|*.json|XML Document|*.xml";
                saveFileDialog.ShowDialog();

                if (String.IsNullOrEmpty(saveFileDialog.FileName)) return;

                // Delete existing file
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }

                dataExporter = DataExporterFactory.Create(saveFileDialog.FileName, dgvResults);
                dataExporter.Run();

                // Open when complete
                ProcessManager.OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void dgvResults_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // Don't import rows if user isn't running a SELECT query
                if (dgvResults.DataSource == null || IsSelectQuery() == false) return;

                if (IsPasteKeys(e))
                {
                    DisplayImportData(true);
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void DisplayImportData(bool loadDataFromClipboard = false)
        {
            // Show Import Data form
            frmImportData importData = new frmImportData();
            importData.TableName = _currentTable;
            importData.AutoLoadDataFromClipboard = loadDataFromClipboard;
            importData.ShowDialog();

            if (importData.Imported == false) return;

            // Refresh grid if data was imported
            if (IsSelectQuery())
            {
                ExecuteSelectQuery();
            }

        }

        private void dgvResults_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string id;

            try
            {
                // No need to update database if id not present in grid
                if (dgvResults.Columns.Contains("_id") == false) return;

                // Get id of current row
                id = e.Row.Cells["_id"].Value.ToString();

                if (String.IsNullOrEmpty(id)) return;

                // Ask user for confirmation
                if (MessageBox.Show("Are you sure you want to delete this row?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                // Delete row and repopulate grid
                _commandExecutor.ExecuteNonQuery($"DELETE {_currentTable} WHERE _id = {FormatIdFieldForWhereClause(id)}");
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }
    }
}