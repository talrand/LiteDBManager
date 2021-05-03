using System;
using System.Windows.Forms;
using Talrand.Core;

namespace LiteDBManager.Classes.Exporters
{
    public class JsonDataExporter : IDataExporter
    {
        string _fileName = "";
        DataGridView _dataGridView = null;

        public string FileName { set { _fileName = value; } }
        public DataGridView DataGridView { set { _dataGridView = value; } }

        public void Run()
        {
            Type fieldType = null;
            string columnName = "";

            using (var jsonWriter = new JsonWriter())
            {
                jsonWriter.WriteArrayStartElement("root");

                // Write each row
                foreach (DataGridViewRow row in _dataGridView.Rows)
                {
                    // Don't output new row
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    jsonWriter.WriteArrayItemStart();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        columnName = _dataGridView.Columns[cell.ColumnIndex].Name;

                        if (cell.Value == null)
                        {
                            jsonWriter.WriteStringElement(columnName, "");
                        }
                        else
                        {
                            fieldType = cell.Value.GetType();

                            // Write Json element based on value type
                            if (fieldType.Equals(typeof(string)) || fieldType.Equals(typeof(DateTime)))
                            {
                                jsonWriter.WriteStringElement(columnName, cell.Value.ToString());
                            }

                            if (fieldType.Equals(typeof(bool)))
                            {
                                jsonWriter.WriteBooleanElement(columnName, (bool)cell.Value);
                            }

                            if (fieldType.Equals(typeof(int)) || fieldType.Equals(typeof(byte)))
                            {
                                jsonWriter.WriteNumberElement(columnName, (int)cell.Value);
                            }

                            if (fieldType.Equals(typeof(decimal)) || fieldType.Equals(typeof(double)))
                            {
                                jsonWriter.WriteNumberElement(columnName, (decimal)cell.Value);
                            }
                        }
                    }

                    jsonWriter.WriteEndElement(); // item
                }

                jsonWriter.WriteEndElement(); // root

                // Finally write json to file
                jsonWriter.WriteToFile(_fileName);
            }
        }
    }
}