using System;
using System.Windows.Forms;
using Talrand.Core;

namespace LiteDBManager.Classes.Exporters
{
    public class XmlDataExporter : IDataExporter
    {
        string _fileName = "";
        DataGridView _dataGridView = null;

        public string FileName { set { _fileName = value; } }
        public DataGridView DataGridView { set { _dataGridView = value; } }

        public void Run()
        {
            Type fieldType = null;
            string columnName = "";

            using (var xmlWriter = new XmlWriter())
            {
                xmlWriter.WriteStartElement("data");

                // Write each row
                foreach (DataGridViewRow row in _dataGridView.Rows)
                {
                    // Don't output new row
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    xmlWriter.WriteStartElement("item");

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        columnName = _dataGridView.Columns[cell.ColumnIndex].Name;

                        if (cell.Value == null)
                        {
                            xmlWriter.WriteStringElement(columnName, "");
                        }
                        else
                        {
                            fieldType = cell.Value.GetType();

                            // Write Json element based on value type
                            if (fieldType.Equals(typeof(string)) || fieldType.Equals(typeof(DateTime)))
                            {
                                xmlWriter.WriteStringElement(columnName, cell.Value.ToString());
                            }

                            if (fieldType.Equals(typeof(bool)))
                            {
                                xmlWriter.WriteBooleanElement(columnName, (bool)cell.Value);
                            }

                            if (fieldType.Equals(typeof(int)) || fieldType.Equals(typeof(byte)))
                            {
                                xmlWriter.WriteNumberElement(columnName, (int)cell.Value);
                            }

                            if (fieldType.Equals(typeof(decimal)) || fieldType.Equals(typeof(double)))
                            {
                                xmlWriter.WriteNumberElement(columnName, (decimal)cell.Value);
                            }
                        }
                    }
                    xmlWriter.WriteEndElement(); // item
                }
                xmlWriter.WriteEndElement(); // data

                // Finally write xml to file
                xmlWriter.WriteToFile(_fileName);
            }
        }
    }
}