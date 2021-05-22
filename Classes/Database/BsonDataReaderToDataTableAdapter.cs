using System;
using System.Data;
using LiteDB;
using System.Collections.Generic;

namespace LiteDBManager.Classes.Database
{
    public class BsonDataReaderToDataTableAdapter
    {
        private DataTable _dataTable;
        public DataTable Convert(IBsonDataReader reader)
        {
            DataRow dataRow = null;
            bool isArrayField = false;

            _dataTable = new DataTable();

            // Populate datatable
            while (reader.Read())
            {
                if (_dataTable.Columns.Count == 0)
                {
                    // Add columns to table
                    foreach (var keyValuePair in (BsonDocument)reader.Current)
                    {
                        if (keyValuePair.Value.Type == BsonType.Array)
                        {
                            _dataTable.Columns.Add(new DataColumn(keyValuePair.Key, typeof(String)));
                        }
                        else
                        {
                            _dataTable.Columns.Add(new DataColumn(keyValuePair.Key, keyValuePair.Value.Type.ToSystemType()));
                        }
                    }
                }

                dataRow = _dataTable.NewRow();

                // Populate new row with data
                foreach (var keyValuePair in (BsonDocument)reader.Current)
                {
                    if (keyValuePair.Value.Type == BsonType.Array)
                    {
                        ProcessArrayField(keyValuePair.Key, keyValuePair.Value);
                        isArrayField = true;
                    }
                    else
                    {
                        dataRow[keyValuePair.Key] = keyValuePair.Value.RawValue;
                    }
                }

                // Add row to table
                if (isArrayField == false)
                {
                    _dataTable.Rows.Add(dataRow);
                    _dataTable.AcceptChanges();
                }
            }

            return _dataTable;
        }

        private void ProcessArrayField(string key, BsonValue bsonValue)
        {
            DataRow dataRow = null;

            foreach (var value in bsonValue.AsArray)
            {
                dataRow = _dataTable.NewRow();
                dataRow[key] = value.RawValue;
                _dataTable.Rows.Add(dataRow);
                _dataTable.AcceptChanges();
            }
        }

    }
}
