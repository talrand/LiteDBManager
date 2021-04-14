using System;
using System.Collections.Generic;
using LiteDB;
using System.IO;
using System.Data;
using static LiteDBManager.Classes.BsonTypeMapper;
using System.Text;

namespace LiteDBManager.Classes
{
    public static class DatabaseWrapper
    {
        public struct ConnectionMethod
        {
            public const string Shared = "shared";
            public const string Exclusive = "exclusive";
        }

        public const string DatabaseFilter = "Database files|*.db";

        private static LiteDatabase _database = null;
        private static string _databaseName = "";
        private static bool _isDatabaseReadOnly = false;

        public static LiteDatabase Database { get { return _database; } }
        public static string DatabaseName { get { return _databaseName; } }
        public static bool IsDatabaseReadOnly { get { return _isDatabaseReadOnly; } }

        public static void OpenDatabase(string fileName, string password, string connectionMethod)
        {
            // Connect to database - this will create it if it doesn't exist
            _database = new LiteDatabase(BuildConnectionString(fileName, password, connectionMethod));
            
            // Store database name for display in database explorer
            _databaseName = Path.GetFileName(fileName);

            // Store database file in recent files log
            RecentFiles.Write(fileName);
        }

        private static string BuildConnectionString(string fileName, string password, string connectionMethod)
        {
            var stringBuilder = new StringBuilder();
            FileInfo fileInfo = null;

            stringBuilder.Append($"filename={fileName};");
            stringBuilder.Append($"connection={connectionMethod};");

            if (password != "")
            {
                stringBuilder.Append($"password={password};");
            }

            // Get database file info
            fileInfo = new FileInfo(fileName);
            _isDatabaseReadOnly = fileInfo.IsReadOnly;

            if(_isDatabaseReadOnly)
            {
                stringBuilder.Append("readonly=true;");
            }

            return stringBuilder.ToString();
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

        public static string FormatFieldValue(object value)
        {
            Type valueType = value.GetType();

            // For strings and dates wrap value in quotes
            if (valueType.Equals(typeof(string)) || valueType.Equals(typeof(DateTime)))
            {
                return $"'{value}'";
            }

            return value.ToString();
        }

        public static string FormatIdFieldForWhereClause(string id)
        {
            return $"{{\"$oid\": \"{id}\"}}";
        }
    }
}