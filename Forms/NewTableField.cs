using System;
using System.Windows.Forms;
using LiteDBManager.Classes.Database;

namespace LiteDBManager.Forms
{
    public partial class frmNewTableField : Form
    {
        private string _tableName = "";
        private bool _saved = false;

        public string TableName { set => _tableName = value; }
        public bool Saved { get => _saved; }

        public frmNewTableField()
        {
            InitializeComponent();
        }

        private void frmNewTableField_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateFieldTypes();
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.ShowDialog();
            }
        }

        private void PopulateFieldTypes()
        {
            cboFieldType.Items.Add(TableFieldTypes.String.Name);
            cboFieldType.Items.Add(TableFieldTypes.Integer.Name);
            cboFieldType.Items.Add(TableFieldTypes.Decimal.Name);
            cboFieldType.Items.Add(TableFieldTypes.Boolean.Name);
            cboFieldType.Items.Add(TableFieldTypes.Date.Name);
            cboFieldType.SelectedIndex = 0;
        }

        private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // To save duplicating code, hide all default value fields first
                HideDefaultValueFields();

                switch(cboFieldType.SelectedItem.ToString())
                {
                    case TableFieldTypes.String.Name:
                        ShowDefaultStringValueField();
                        break;
                    case TableFieldTypes.Integer.Name:
                        ShowDefaultIntegerValueField();
                        break;
                    case TableFieldTypes.Decimal.Name:
                        ShowDefaultDecimalValueField();
                        break;
                    case TableFieldTypes.Boolean.Name:
                        ShowDefaultBooleanValueField();
                        break;
                    case TableFieldTypes.Date.Name:
                        ShowDefaultDateValueField();
                        break;
                }
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.Show();
            }
        }
        
        private void HideDefaultValueFields()
        {
            txtDefaultValue.Visible = false;
            nudDefaultValue.Visible = false;
            cboDefaultValue.Visible = false;
            dtpDefaultValue.Visible = false;
        }

        private void ShowDefaultStringValueField()
        {
            txtDefaultValue.Text = TableFieldTypes.String.DefaultValue;
            txtDefaultValue.Visible = true;
        }

        private void ShowDefaultIntegerValueField()
        {
            nudDefaultValue.Value = TableFieldTypes.Integer.DefaultValue;
            nudDefaultValue.DecimalPlaces = 0;
            nudDefaultValue.Visible = true;
        }

        private void ShowDefaultDecimalValueField()
        {
            nudDefaultValue.Value = TableFieldTypes.Decimal.DefaultValue;
            nudDefaultValue.DecimalPlaces = 4;
            nudDefaultValue.Visible = true;
        }

        private void ShowDefaultBooleanValueField()
        {
            cboDefaultValue.SelectedIndex = 0;
            cboDefaultValue.Visible = true;
        }

        private void ShowDefaultDateValueField()
        {
            dtpDefaultValue.Value = TableFieldTypes.Date.DefaultValue;
            dtpDefaultValue.Visible = true;
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            var updateCommandBuilder = new UpdateCommandBuilder();
            var commandExecutor = new CommandExecutor();

            try
            {
                // To add a new field to the database we need to perform an update statement
                updateCommandBuilder.SetTableName(_tableName);

                switch (cboFieldType.SelectedItem.ToString())
                {
                    case TableFieldTypes.String.Name:
                        updateCommandBuilder.AddField(txtFieldName.Text, txtDefaultValue.Text);
                        break;
                    case TableFieldTypes.Integer.Name:
                    case TableFieldTypes.Decimal.Name:
                        updateCommandBuilder.AddField(txtFieldName.Text, nudDefaultValue.Value);
                        break;
                    case TableFieldTypes.Boolean.Name:
                        updateCommandBuilder.AddField(txtFieldName.Text, Convert.ToBoolean(cboDefaultValue.SelectedItem));
                        break;
                    case TableFieldTypes.Date.Name:
                        updateCommandBuilder.AddField(txtFieldName.Text, dtpDefaultValue.Value);
                        break;
                }

                commandExecutor.ExecuteNonQuery(updateCommandBuilder.ToString());

               _saved = true;
                MessageBox.Show("Operation Complete");
                Close();
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.ShowDialog();
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                new frmSystemError() { Exception = ex }.ShowDialog();
            }
        }
    }
}
