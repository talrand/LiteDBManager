using System;
using System.Windows.Forms;
using System.IO;
using static LiteDBManager.Classes.Database.LiteDBWrapper;
using LiteDBManager.Classes.Database;

namespace LiteDBManager.Forms
{
    public partial class frmDatabaseConnection : Form
    {
        private bool _connected = false;
        public bool Connected { get { return _connected; } }

        public frmDatabaseConnection()
        {
            InitializeComponent();
        }

        private void frmDatabaseConnection_Load(object sender, EventArgs e)
        {
            try
            {
                // Populate drop downs
                PopulateConnectionMethods();
                PopulateRecentFileNames();
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.ShowDialog();
            }
        }

        private void PopulateConnectionMethods()
        {
            cboMethod.Items.Add(ConnectionMethod.Shared);
            cboMethod.Items.Add(ConnectionMethod.Direct);
            cboMethod.SelectedIndex = 0;
        }

        private void PopulateRecentFileNames()
        {
            foreach (string file in RecentFiles.Files)
            {
                cboFileName.Items.Add(file);
            }

            // Select first item
            if (cboFileName.Items.Count > 0)
            {
                cboFileName.SelectedIndex = 0;
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Prompt user to select a .db file
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = DatabaseFilter;
                    openFileDialog.ShowDialog();
                    cboFileName.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.ShowDialog();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            DatabaseConnector databaseConnector = new DatabaseConnector();
            DatabaseInfo databaseInfo = null;

            try
            {
                databaseInfo = databaseConnector.Connect(cboFileName.Text, txtPassword.Text, cboMethod.SelectedItem.ToString());
                SetDatabaseInfo(databaseInfo);
                _connected = true;
                this.Close();
            }
            catch (FileLoadException fileEx)
            {
                MessageBox.Show(fileEx.Message, "File in use", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.ShowDialog();
            }
        }
    }
}