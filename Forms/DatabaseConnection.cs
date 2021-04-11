using System;
using System.Windows.Forms;
using static LiteDBManager.Classes.DatabaseWrapper;
using LiteDBManager.Classes;

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
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateConnectionMethods()
        {
            try
            {
                cboMethod.Items.Add(ConnectionMethod.Shared);
                cboMethod.Items.Add(ConnectionMethod.Exclusive);
                cboMethod.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateRecentFileNames()
        {
            try
            {
                foreach(string file in RecentFiles.Files)
                {
                    cboFileName.Items.Add(file);
                }

                // Select first item
                if(cboFileName.Items.Count > 0)
                {
                    cboFileName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                OpenDatabase(cboFileName.Text, txtPassword.Text, cboMethod.SelectedItem.ToString());
                _connected = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }
    }
}