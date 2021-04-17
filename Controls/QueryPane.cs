﻿using System;
using System.Reflection;
using System.Windows.Forms;
using LiteDBManager.Classes;
using static LiteDBManager.Classes.DatabaseWrapper;
using System.Drawing;

namespace LiteDBManager.Controls
{
    public partial class QueryPane : UserControl
    {
        private string _currentTable = "";

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
                MessageBox.Show(ex.Message);
            }
        }

        public void SetQueryText(string query)
        {
            try
            {
                txtQuery.Text = query;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            try
            {
                txtQuery.Text = "";
                dgvResults.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQuery.Text.Length == 0)
                {
                    return;
                }

                // Populate datagrid from select query
                if (txtQuery.Text.Length >= 6)
                {
                    if (txtQuery.Text.Substring(0, 6).Equals("SELECT", StringComparison.InvariantCultureIgnoreCase))
                    {
                        GetCurrentTableNameFromSelectQuery();
                        PopulateGridFromSelectQuery();

                        dgvResults.Visible = true;
                        txtNonQueryResult.Visible = false;

                        return;
                    }
                }

                // Run non-query command
                ExecuteNonQueryCommand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetCurrentTableNameFromSelectQuery()
        {
            string[] words = null;
            bool fromKeyWordFound = false;

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateGridFromSelectQuery()
        {
            try
            {
                dgvResults.DataSource = ExecuteQuery(txtQuery.Text);

                // Database in readonly mode
                if (IsDatabaseReadOnly)
                {
                    dgvResults.ReadOnly = true;
                    return;
                }

                // Stop users editing internal _id column
                if (dgvResults.Columns.Contains("_id"))
                {
                    dgvResults.Columns["_id"].ReadOnly = true;
                    dgvResults.ReadOnly = false;
                }
                else
                {
                    dgvResults.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteNonQueryCommand()
        {
            ExecuteResult executeResult = null;

            try
            {
                txtNonQueryResult.Visible = true;
                dgvResults.Visible = false;

                executeResult = ExecuteNonQuery(txtQuery.Text);

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
                if (dgvResults.ReadOnly)
                {
                    return;
                }

                // Ignore existing rows
                if (dgvResults.Rows[e.RowIndex].Cells["_id"].Value.ToString() != "")
                {
                    return;
                }

                // Don't insert blank rows
                if (IsGridRowBlank(dgvResults.Rows[e.RowIndex]) == true)
                {
                    return;
                }

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
                ExecuteNonQuery(insertCommand.ToString());

                // Re-run query command - needs to use BeginInvoke call to avoid reentrant errors
                BeginInvoke(new MethodInvoker(PopulateGridFromSelectQuery));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void dgvResults_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Finish editing cell before leaving.
                // This resolves issues with null values being inserted after editing a value in a new row and switching rows
                if (dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode == true)
                {
                    dgvResults.EndEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                // Get values from grid
                cell = dgvResults.Rows[e.RowIndex].Cells[e.ColumnIndex];
                columnName = dgvResults.Columns[e.ColumnIndex].Name;
                id = dgvResults.Rows[e.RowIndex].Cells["_id"].Value.ToString();

                // No id means we're adding a new row
                if (id == "")
                {
                    return;
                }

                // Perform update 
                updateCommand.SetTableName(_currentTable);
                updateCommand.AddField(columnName, cell.Value);
                updateCommand.SetWhereClause($"_id={FormatIdFieldForWhereClause(id)}");

                ExecuteNonQuery(updateCommand.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                    return;
                }

                mnuDeleteRow.Enabled = !dgvResults.ReadOnly;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuDeleteRow_Click(object sender, EventArgs e)
        {
            string id = "";

            try
            {
                if (dgvResults.CurrentRow == null)
                {
                    return;
                }

                id = dgvResults.CurrentRow.Cells["_id"].Value.ToString();

                // Don't attempt to delete 'New Row'
                if (id == "")
                {
                    return;
                }

                if (MessageBox.Show("Are you sure you want to delete this row?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Delete row and repopulate grid
                    ExecuteNonQuery($"DELETE {_currentTable} WHERE _id = {FormatIdFieldForWhereClause(id)}");
                    PopulateGridFromSelectQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}