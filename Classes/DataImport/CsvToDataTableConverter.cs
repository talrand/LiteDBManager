using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace LiteDBManager.Classes.DataImport
{
    public class CsvToDataTableConverter
    {
        private bool _firstRowContainsHeaders = false;
        private DataTable _dataTable;

        public bool FirstRowContainsHeaders { set => _firstRowContainsHeaders = value; }

        public DataTable Convert(string fileName)
        {
            string[] lines;

            // Ensure passed file exists
            if (String.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));

            if (File.Exists(fileName) == false) throw new ArgumentException($"{nameof(fileName)} does not exist");

            _dataTable = new DataTable();

            // Read file for data
            lines = File.ReadAllLines(fileName);

            for (int i = 0; i < lines.Length; i++)
            {
                List<string> fields = GetFieldsFromLine(lines[i]);

                // Add columns when on the first row
                if (i == 0)
                {
                    if (_firstRowContainsHeaders)
                    {
                        AddColumnsToDataTableFromFieldNames(fields);
                        continue;
                    }
                    else
                    {
                        AddNumericColumnsToDataTable(fields.Count);
                    }
                }

                // Create a row from fields and add to table
                AddRowToDataTable(fields);
            }

            return _dataTable;
        }

        private List<string> GetFieldsFromLine(string line)
        {
            List<string> fields = new List<string>();
            char[] chars = line.ToCharArray();
            string field = "";
            bool quoteCharFound = false;

            for (int i = 0; i < chars.Length; i++)
            {
                switch (chars[i])
                {
                    case '"':
                        quoteCharFound = !quoteCharFound;
                        break;
                    case ',':
                        if (quoteCharFound == false)
                        {
                            // End of field
                            fields.Add(field);

                            // Reset field variable for next field
                            field = String.Empty;
                        }
                        else
                        {
                            // Inside quotes, therefore treat char as part of field
                            field += chars[i].ToString();
                        }
                        break;

                    default:
                        field += chars[i].ToString();
                        break;
                }
            }

            // Add last field to list
            fields.Add(field);

            return fields;
        }

        private void AddColumnsToDataTableFromFieldNames(List<string> fields)
        {
            foreach (string field in fields)
            {
                _dataTable.Columns.Add(SetColumnNameFromFieldName(field));
            }
        }

        private string SetColumnNameFromFieldName(string fieldName)
        {
            int count = 0;

            /* Csv files may contain fields of the same name
            * create a unique column name by appending a number */
            while (_dataTable.Columns.Contains(fieldName))
            {
                count++;
                fieldName += count;
            }

            return fieldName;
        }

        private void AddNumericColumnsToDataTable(int columnCount)
        {
            // File doesn't contain column headers, therefore generate unique column names
            for (int i = 1; i <= columnCount; i++)
            {
                _dataTable.Columns.Add($"field{i}");
            }
        }

        private void AddRowToDataTable(List<string> fields)
        {
            DataRow dataRow = _dataTable.NewRow();

            // Move data to row
            for (int i = 0; i < fields.Count; i++)
            {
                dataRow[i] = fields[i].Trim();  
            }

            // Add row to table
            _dataTable.Rows.Add(dataRow);
        }
    }
}