using System;
using System.Windows.Forms;
using static LiteDBManager.Classes.DatabaseWrapper;

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
                // Populate connection methods
                cboMethod.Items.Add(ConnectionMethod.Shared);
                cboMethod.Items.Add(ConnectionMethod.Exclusive);
                cboMethod.SelectedIndex = 0;
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
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = DatabaseFilter;
                    openFileDialog.ShowDialog();
                    txtFileName.Text = openFileDialog.FileName;
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
                OpenDatabase(txtFileName.Text, txtPassword.Text, cboMethod.SelectedItem.ToString());
                _connected = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
