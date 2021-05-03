using System;
using System.Data;
using LiteDB;

namespace LiteDBManager.Classes
{
    public class BsonDataReaderToDataTableAdapter
    {
        public DataTable Convert(IBsonDataReader reader)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = null;

            // Populate datatable
            while (reader.Read())
            {
                if (dataTable.Columns.Count == 0)
                {
                    // Add columns to table
                    foreach (var keyValuePair in (BsonDocument)reader.Current)
                    {
                        dataTable.Columns.Add(new DataColumn(keyValuePair.Key, keyValuePair.Value.Type.ToSystemType()));
                    }
                }

                dataRow = dataTable.NewRow();

                // Populate new row with data
                foreach (var keyValuePair in (BsonDocument)reader.Current)
                {

                    dataRow[keyValuePair.Key] = keyValuePair.Value.RawValue;
                }

                // Add row to table
                dataTable.Rows.Add(dataRow);
                dataTable.AcceptChanges();
            }

            return dataTable;
        }
    }
}
