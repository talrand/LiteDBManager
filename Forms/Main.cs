using System;
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
        private struct DatabaseExplorerNodeTags
        {
            public const string Database = "DB";
            public const string System = "System";
            public const string UserTable = "UserTable";
            public const string SystemTable = "SystemTable";
        }

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
                        PopulateDatabaseExplorer();

                        // Enable menu items
                        mnuDisconnect.Enabled = true;
                        mnuNewQuery.Enabled = true;
                        mnuQueries.Enabled = true;
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

        private void PopulateDatabaseExplorer()
        {
            try
            {
                // Remove previous tables
                treeTables.Nodes.Clear();

                // Add database node
                treeTables.Nodes.Add(new TreeNode() { Text = DatabaseName, Tag = DatabaseExplorerNodeTags.Database });

                if (IsDatabaseReadOnly)
                {
                    treeTables.Nodes[0].Text += " [Read Only]";
                }

                // Add tables
                AddSystemTablesToDatabaseExplorer();
                AddUserTablesToDatabaseExplorer();

                treeTables.Nodes[0].Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddSystemTablesToDatabaseExplorer()
        {
            List<string> tableNames = null;
            TreeNode systemTreeNode = null;

            try
            {
                systemTreeNode = new TreeNode() { Text = "System", Tag = DatabaseExplorerNodeTags.System };           

                // Get all system tables and add them to system node
                tableNames = GetTableNames(TableType.System);

                foreach (string tableName in tableNames)
                {
                    systemTreeNode.Nodes.Add(new TreeNode() { Text = tableName, Tag = DatabaseExplorerNodeTags.SystemTable });
                }

                // Add system node to database node
                treeTables.Nodes[0].Nodes.Add(systemTreeNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddUserTablesToDatabaseExplorer()
        {
            List<string> tableNames = null;

            try
            {
                // Get names of all user defined tables
                tableNames = GetTableNames(TableType.User);

                // Populate treeview
                foreach (string tableName in tableNames)
                {
                    treeTables.Nodes[0].Nodes.Add(new TreeNode() { Text = tableName, Tag = DatabaseExplorerNodeTags.UserTable });
                }
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
                mnuQueries.Enabled = false;
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
                if (e.Node.Tag.ToString() == DatabaseExplorerNodeTags.Database)
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

        private void mnuCloseCurrentQuery_Click(object sender, EventArgs e)
        {
            try
            {
                tabQueries.TabPages.Remove(tabQueries.SelectedTab);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuCloseAllButCurrentQuery_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(TabPage tabPage in tabQueries.TabPages)
                {
                    if(tabPage != tabQueries.SelectedTab)
                    {
                        tabQueries.TabPages.Remove(tabPage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuCloseAllQueries_Click(object sender, EventArgs e)
        {
            try
            {
                tabQueries.TabPages.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnuQueries_DropDownOpening(object sender, EventArgs e)
        {
            bool enabled = false;

            try
            {
                // Only enable query pane menu items if there are any open tabs 
                if(tabQueries.TabPages.Count > 0)
                {
                    enabled = true;
                }

                mnuCloseCurrentQuery.Enabled = enabled;
                mnuCloseAllButCurrentQuery.Enabled = enabled;
                mnuCloseAllQueries.Enabled = enabled;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}