using System;
using System.Collections.Generic;
using LiteDB;
using System.IO;
using System.Data;
using static LiteDBManager.Classes.BsonTypeMapper;

namespace LiteDBManager.Classes
{
    public static class DatabaseEngine
    {
        private static LiteDatabase _database = null;
        private static string _databaseName = "";

        public static LiteDatabase Database { get { return _database; } }
        public static string DatabaseName { get { return _databaseName; } }

        public static void OpenDatabase(string fileName)
        {
            _database = new LiteDatabase("Filename=" + fileName + ";connection=shared;");
            _databaseName = Path.GetFileName(fileName);
        }

        public static void CloseDatabase()
        {
            _database?.Dispose();
            _database = null;
        }

        public static List<string> GetNonSystemTableNames()
        {
            var tableNames = new List<string>();
            // Get user defined tables
            var reader = _database.Execute("SELECT name from $cols WHERE Type = 'user'");
            
            while(reader.Read())
            {
                var keyValuePairs = (Dictionary<string, BsonValue>)reader.Current.RawValue;
               
                // Add table name to collection
                foreach (var value in keyValuePairs)
                {
                    tableNames.Add(value.Value.RawValue.ToString());
                }
            }

            return tableNames;
        }

        public static DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = null;
            
            // Execute query
            var reader = _database.Execute(query);

            // Populate datatable
            while (reader.Read())
            {
                if (dataTable.Columns.Count == 0)
                {
                    // Add columns to table
                    foreach(var keyValuePair in (BsonDocument)reader.Current)
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