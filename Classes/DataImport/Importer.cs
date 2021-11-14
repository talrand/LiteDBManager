using System;
using System.Linq;
using System.Data;
using LiteDBManager.Classes.Database;
using System.IO;

namespace LiteDBManager.Classes.DataImport
{
    public class Importer
    {
        public DataTable CreateImportDataTable(string tableName)
        {
            if (String.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

            DataTable dataTable = new DataTable();
            TableReader tableReader = new TableReader();

            // Get schema of passed table
            DataTable tableSchema = tableReader.ReadSchema(tableName);

            // Add columns for each field in returned schema
            foreach (DataRow row in tableSchema.Rows)
            {
                dataTable.Columns.Add((string)row["Field"], Type.GetType((string)row["Type"]));
            }

            return dataTable;
        }

        public DataTable ReadDataFromClipboard(DataTable dataTable)
        {
            string textToImport = System.Windows.Forms.Clipboard.GetText();
            string[] rows;
            DataRow dataRow;

            // Ensure data has tabs, as this is what we'll use to separate data
            if (textToImport.Contains('\t') == false) throw new InvalidDataException("Copied data must contain tabs");

            rows = textToImport.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string row in rows)
            {
                // Remove leading tabs to prevent errors and then split to get individual fields
                string[] fields = row.TrimStart().Split('\t');

                // Sanity check the data before continuing
                if (fields.Length > dataTable.Columns.Count) throw new InvalidDataException("Copied data has more columns than current table");

                // Move data to row and add to table
                dataRow = dataTable.NewRow();

                for (int i = 0; i < fields.Length; i++)
                {
                    // Blank out LiteDB id field, as importing will generate new ids for the records
                    if (dataTable.Columns[i].ColumnName == "_id")
                    {
                        dataRow[i] = "";
                    }
                    else
                    {
                        dataRow[i] = fields[i];
                    }
                }

                dataTable.Rows.Add(dataRow);
                dataTable.AcceptChanges();
            }

            return dataTable;
        }

        public void ImportData(string tableName, DataTable dataTable)
        {
            // Don't continue if no data passed
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));

            CommandExecutor commandExecutor = new CommandExecutor();
            InsertCommandBuilder insertCommandBuilder;

            // To prevent attempting to import deleted rows, need to accept changes on the passed datatable first
            dataTable.AcceptChanges();

            // Run an INSERT command for each row
            foreach (DataRow row in dataTable.Rows)
            {
                insertCommandBuilder = new InsertCommandBuilder();
                insertCommandBuilder.SetTableName(tableName);

                // Construct INSERT command
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (dataTable.Columns[i].ColumnName == "_id") continue;

                    insertCommandBuilder.AddField(dataTable.Columns[i].ColumnName, row[i]);
                }

                // Insert into database
                commandExecutor.ExecuteNonQuery(insertCommandBuilder.ToString());
            }
        }
    }
}