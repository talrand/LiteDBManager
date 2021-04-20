using System;
using System.Windows.Forms;
using Talrand.Core;

namespace LiteDBManager.Classes
{
    public static class DataGridViewExtensions
    {
        public static object DefaultValue(this DataGridViewCell cell)
        {
            Type columnValueType = cell.DataGridView.Columns[cell.ColumnIndex].ValueType;

            // Default numeric columns to 0
            if (columnValueType.Equals(typeof(int)) || columnValueType.Equals(typeof(byte)) || columnValueType.Equals(typeof(decimal)) || columnValueType.Equals(typeof(double)))
            {
                return 0;
            }

            // Default boolean columns to false
            if (columnValueType.Equals(typeof(bool)))
            {
                return false;
            }

            return "";
        }

        public static void ToCSV(this DataGridView dataGridView, string fileName)
        {
            CSVWriter csvWriter = new CSVWriter();

            csvWriter.FileName = fileName;

            // Write headings to csv file
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                csvWriter.AddValue(column.Name);
            }

            csvWriter.WriteLine();

            // Write rows to csv file
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null)
                    {
                        csvWriter.AddValue("");
                    }
                    else
                    {
                        csvWriter.AddValue(cell.Value.ToString());
                    }
                }

                csvWriter.WriteLine();
            }
        }

        public static void ToJson(this DataGridView dataGridView, string fileName)
        {
            Type fieldType = null;
            string columnName = "";

            using (var jsonWriter = new JsonWriter())
            {
                jsonWriter.WriteArrayStartElement("root");

                // Write each row
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    jsonWriter.WriteArrayItemStart();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        columnName = dataGridView.Columns[cell.ColumnIndex].Name;

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
                jsonWriter.WriteToFile(fileName);
            }
        }
    }
}