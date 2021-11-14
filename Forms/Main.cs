using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiteDBManager.Classes.Database;
using LiteDBManager.Forms;
using LiteDBManager.Controls;
using static LiteDBManager.Classes.Database.LiteDBWrapper;
using static LiteDBManager.Classes.Globals;

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
                DisplayError(ex);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Dispose of LiteDatabase object
                Disconnect();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
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
                DisplayError(ex);
            }
        }

        private void ClearControls()
        {
            treeTables.Nodes.Clear();
            tabQueries.TabPages.Clear();
        }

        #region PopulatingDatabaseExplorer
        private void PopulateDatabaseExplorer()
        {
            // Remove previous tables
            treeTables.Nodes.Clear();

            // Add database node
            treeTables.Nodes.Add(new TreeNode() { Text = DatabaseName, Tag = DatabaseExplorerNodeTags.Database, ImageIndex = 0, ToolTipText = DatabaseFileName, ContextMenuStrip = mnuDatabase });

            if (DatabaseReadOnly)
            {
                treeTables.Nodes[0].Text += " [Read Only]";
                mnuDeleteTable.Enabled = false;
                mnuDatabaseRebuild.Enabled = false;
            }

            // Add tables
            AddSystemTablesToDatabaseExplorer();
            AddUserTablesToDatabaseExplorer();

            treeTables.Nodes[0].Expand();
        }

        private void AddSystemTablesToDatabaseExplorer()
        {
            TableReader tableReader = new TableReader();
            List<string> tableNames = null;
            TreeNode systemTreeNode = null;

            systemTreeNode = new TreeNode() { Text = "System", Tag = DatabaseExplorerNodeTags.System, ImageIndex = 1, SelectedImageIndex = 1 };

            // Get all system tables and add them to system node
            tableNames = tableReader.ReadNames(TableType.System);

            foreach (string tableName in tableNames)
            {
                systemTreeNode.Nodes.Add(new TreeNode() { Text = tableName, Tag = DatabaseExplorerNodeTags.SystemTable, ImageIndex = 2, SelectedImageIndex = 2 });
            }

            // Add system node to database node
            treeTables.Nodes[0].Nodes.Add(systemTreeNode);
        }

        private void AddUserTablesToDatabaseExplorer()
        {
            TableReader tableReader = new TableReader();
            List<string> tableNames = null;

            // Get names of all user defined tables
            tableNames = tableReader.ReadNames(TableType.User);

            // Populate treeview
            foreach (string tableName in tableNames)
            {
                treeTables.Nodes[0].Nodes.Add(new TreeNode() { Text = tableName, Tag = DatabaseExplorerNodeTags.UserTable, ImageIndex = 2, SelectedImageIndex = 2 });
            }
        }
        #endregion

        private void mnuDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Disconnect();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }


        private void Disconnect()
        {
            CloseDatabase();
            ClearControls();
            mnuDisconnect.Enabled = false;
            mnuNewQuery.Enabled = false;
            mnuQueries.Enabled = false;
        }

        private void treeTables_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Tag.ToString() == DatabaseExplorerNodeTags.Database || e.Node.Tag.ToString() == DatabaseExplorerNodeTags.System)
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
                DisplayError(ex);
            }
        }

        private void CreateNewQueryPane(string queryText = "")
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

        #region QueriesContextMenu
        private void mnuQueries_DropDownOpening(object sender, EventArgs e)
        {
            bool enabled = false;

            try
            {
                // Only enable query pane menu items if there are any open tabs 
                if (tabQueries.TabPages.Count > 0)
                {
                    enabled = true;
                }

                mnuCloseCurrentQuery.Enabled = enabled;
                mnuCloseAllButCurrentQuery.Enabled = enabled;
                mnuCloseAllQueries.Enabled = enabled;

            }
            catch (Exception ex)
            {
                DisplayError(ex);
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
                DisplayError(ex);
            }
        }

        private void mnuCloseAllButCurrentQuery_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage tabPage in tabQueries.TabPages)
                {
                    if (tabPage != tabQueries.SelectedTab)
                    {
                        tabQueries.TabPages.Remove(tabPage);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
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
                DisplayError(ex);
            }
        }
        #endregion


        private void tabQueries_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    // Loop through tabs and find tab that was clicked
                    for (int i = 0; i < tabQueries.TabPages.Count; i++)
                    {
                        if (tabQueries.GetTabRect(i).Contains(e.Location))
                        {
                            // Remove tab
                            tabQueries.TabPages.Remove(tabQueries.TabPages[i]);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        #region DatabaseExplorerContextMenu
        private void treeTables_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    return;
                }

                if (e.Node.Tag.ToString() == DatabaseExplorerNodeTags.UserTable)
                {
                    // Need to select node clicked on, otherwise previously selected node is still active
                    treeTables.SelectedNode = e.Node;
                    mnuDatabaseTables.Show(Cursor.Position);
                }

            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuTableNewQuery_Click(object sender, EventArgs e)
        {
            try
            {
                CreateNewQueryPane($"SELECT $ FROM { treeTables.SelectedNode.Text }");
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuViewTableSchema_Click(object sender, EventArgs e)
        {
            try
            {
                frmTableSchema tableSchema = new frmTableSchema();
                tableSchema.TableName = treeTables.SelectedNode.Text;
                tableSchema.ShowDialog();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuDeleteTable_Click(object sender, EventArgs e)
        {
            CommandExecutor commandExecutor = new CommandExecutor();

            try
            {
                if (MessageBox.Show("Are you sure you want to delete this table?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Delete table from database
                    commandExecutor.DeleteTable(treeTables.SelectedNode.Text);

                    // Remove tree node
                    treeTables.Nodes.Remove(treeTables.SelectedNode);
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuImportData_Click(object sender, EventArgs e)
        {
            frmImportData importData = new frmImportData();

            try
            {
                // Show import data form
                importData.TableName = treeTables.SelectedNode.Text;
                importData.ShowDialog();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }
        #endregion

        #region DatabaseContextMenu
        private void mnuDatabaseDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Disconnect();
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void mnuDatabaseRebuild_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to rebuild the database?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RebuildDatabase();
                    PopulateDatabaseExplorer();
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }
        #endregion
    }
}