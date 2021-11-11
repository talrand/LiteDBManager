using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LiteDBManager.Classes.Database;

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
    }
}