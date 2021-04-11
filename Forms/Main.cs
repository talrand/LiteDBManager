﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiteDBManager.Classes;
using static LiteDBManager.Classes.DatabaseEngine;

namespace LiteDBManager
{
    public partial class frmMain : Form
    {
        private const string DatabaseFilter = "Database files|*.db";
        private const string DatabaseTreeNodeTag = "DB";
        private const string TableTreeNodeTag = "Table";

        private string _currentTable = "";

        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuNewDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = DatabaseFilter;
                    saveFileDialog.ShowDialog();

                    if (saveFileDialog.FileName != "")
                    {
                        OpenDatabase(saveFileDialog.FileName);
                        PopulateTableNames();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuOpenDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = DatabaseFilter;
                    openFileDialog.ShowDialog();

                    if (openFileDialog.FileName != "")
                    {
                        OpenDatabase(openFileDialog.FileName);
                        PopulateTableNames();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateTableNames()
        {
            List<string> tableNames = null;
            
            try
            {
                // Remove previous tables
                treeTables.Nodes.Clear();

                // Add database node
                treeTables.Nodes.Add(new TreeNode() { Text = DatabaseName, Tag = DatabaseTreeNodeTag });

                // Get names of all user defined tables
                tableNames = GetNonSystemTableNames();

                // Populate treeview
                foreach (string tableName in tableNames)
                {
                    treeTables.Nodes[0].Nodes.Add(new TreeNode() { Text = tableName, Tag = TableTreeNodeTag });
                }

                treeTables.ExpandAll();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeTables_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Tag.ToString() == DatabaseTreeNodeTag)
                {
                    return;
                }

                // Default query text
                txtQuery.Text = "SELECT $ FROM " + e.Node.Text;
                _currentTable = e.Node.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuExecuteQuery_Click(object sender, EventArgs e)
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
                        PopulateGridFromSelectQuery();
                        return;
                    }
                }

                // Execute non-query command
                Database.Execute(txtQuery.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateGridFromSelectQuery()
        {
            try
            {
                dgvResults.DataSource = ExecuteQuery(txtQuery.Text);

                // Stop users editing internal _id column
                if (dgvResults.Columns.Contains("_id"))
                {
                    dgvResults.Columns["_id"].ReadOnly = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Dispose of LiteDatabase object
                CloseDatabase();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvResults_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            var insertCommand = new InsertCommandBuilder();

            try
            {
                // Ignore existing rows
                if (dgvResults.Rows[e.RowIndex].Cells["_id"].Value.ToString() != "")
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
                Database.Execute(insertCommand.ToString());

                // Re-run query command - needs to use BeginInvoke call to avoid reentrant errors
                BeginInvoke(new MethodInvoker(PopulateGridFromSelectQuery));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            catch(Exception ex)
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

                Database.Execute(updateCommand.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}