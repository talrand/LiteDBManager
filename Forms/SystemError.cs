using System;
using System.Text;
using System.Windows.Forms;

namespace LiteDBManager.Forms
{
    public partial class frmSystemError : Form
    {
        public Exception Exception { get; set; }

        public frmSystemError()
        {
            InitializeComponent();
        }

        private void frmSystemError_Load(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                stringBuilder.AppendLine(Exception.Message);
                stringBuilder.AppendLine(Exception.InnerException?.Message);
                stringBuilder.AppendLine(Exception.StackTrace);
                txtError.Text = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtError.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
