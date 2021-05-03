using System;
using System.Windows.Forms;
using Talrand.Core;

namespace LiteDBManager.Classes.Exporters
{
    public class CsvDataExporter : IDataExporter
    {
        string _fileName = "";
        DataGridView _dataGridView = null;

        public string FileName { set { _fileName = value; } }
        public DataGridView DataGridView { set { _dataGridView = value; } }

        public void Run()
        {
            CSVWriter csvWriter = new CSVWriter();

            csvWriter.FileName = _fileName;

            // Write headings to csv file
            foreach (DataGridViewColumn column in _dataGridView.Columns)
            {
                csvWriter.AddValue(column.Name);
            }

            csvWriter.WriteLine();

            // Write rows to csv file
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                // Don't output new row
                if (row.IsNewRow)
                {
                    continue;
                }

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
    }
}