﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiteDBManager.Classes;
using LiteDBManager.Forms;
using LiteDBManager.Controls;
using static LiteDBManager.Classes.DatabaseWrapper;

namespace LiteDBManager
{
    public partial class frmMain : Form
    {        
        private const string DatabaseTreeNodeTag = "DB";
        private const string TableTreeNodeTag = "Table";

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                // Get filenames of recently opened databases
                RecentFiles.Read();

                // Display version number in form title
                this.Text += $" v{Application.ProductVersion}";
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuOpenDatabase_Click(object sender, EventArgs e)
        {           
            try
            {
                using (var databaseConnection = new frmDatabaseConnection())
                {
                    databaseConnection.ShowDialog();

                    if (databaseConnection.Connected == true)
                    {
                        // Clear previous data
                        ClearControls();

                        // Populate table names
                        PopulateTableNames();

                        // Enable menu items
                        mnuDisconnect.Enabled = true;
                        mnuNewQuery.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearControls()
        {
            try
            {
                treeTables.Nodes.Clear();
                tabQueries.TabPages.Clear();
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

                if (IsDatabaseReadOnly)
                {
                    treeTables.Nodes[0].Text += " [Read Only]";
                }

                // Get names of all user defined tables
                tableNames = GetNonSystemTableNames();

                // Populate treeview
                foreach (string tableName in tableNames)
                {
                    treeTables.Nodes[0].Nodes.Add(new TreeNode() { Text = tableName, Tag = TableTreeNodeTag });
                }

                treeTables.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                CloseDatabase();
                ClearControls();
                mnuDisconnect.Enabled = false;
                mnuNewQuery.Enabled = false;
            }
            catch (Exception ex)
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

                CreateNewQueryPane($"SELECT $ FROM { e.Node.Text }");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuNewQuery_Click(object sender, EventArgs e)
        {
            try
            {
                CreateNewQueryPane();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateNewQueryPane(string queryText = "")
        {
            try
            {
                // Create new query tab
                TabPage queryTab = new TabPage() { Text = $"Query {tabQueries.TabPages.Count + 1}" };
                
                // Create new query pane
                QueryPane queryPane = new QueryPane() { Dock = DockStyle.Fill };
                queryPane.SetQueryText(queryText);
                queryTab.Controls.Add(queryPane);

                // Add to new tab
                tabQueries.Controls.Add(queryTab);

                // Select new tab
                tabQueries.SelectedTab = queryTab;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}