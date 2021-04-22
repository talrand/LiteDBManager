using System;
using System.Collections.Generic;
using LiteDB;
using System.IO;
using System.Data;
using System.Text;
using System.Diagnostics;
using static LiteDBManager.Classes.BsonTypeMapper;
using static Talrand.Core.ProcessManager;
using static Talrand.Core.Extensions;
using System.Linq;

namespace LiteDBManager.Classes
{
    public static class LiteDBWrapper
    {
        public struct ConnectionMethod
        {
            public const string Shared = "shared";
            public const string Direct = "direct";
        }

        public struct TableType
        {
            public const string System = "system";
            public const string User = "user";
        }

        public const string DatabaseFilter = "Database files|*.db";

        private static LiteDatabase _database = null;
        private static string _databaseName = "";
        private static bool _databaseReadOnly = false;

        public static string DatabaseName { get { return _databaseName; } }
        public static bool DatabaseReadOnly { get { return _databaseReadOnly; } }

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

            stringBuilder.Append($"filename={fileName};");            

            if (password != "")
            {
                stringBuilder.Append($"password={password};");
            }

            if(IsDatabaseReadOnly(fileName) || IsFileLocked(fileName))
            {
                _databaseReadOnly = true;

                // Read only & locked databases can only be opened in shared mode
                stringBuilder.Append($"connection={ConnectionMethod.Shared};readonly=true;");
            }
            else
            {
                _databaseReadOnly = false;

                stringBuilder.Append($"connection={connectionMethod};");
            }

            return stringBuilder.ToString();
        }

        private static bool IsDatabaseReadOnly(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.IsReadOnly;
        }

        public static void CloseDatabase()
        {
            _database?.Dispose();
            _database = null;
        }

        public static List<string> GetTableNames(string tableType)
        {
            var tableNames = new List<string>();
            var reader = _database.Execute($"SELECT name from $cols WHERE Type = '{tableType}'");

            while (reader.Read())
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

        public static ExecuteResult ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = null;
            Stopwatch stopwatch = new Stopwatch();
   
            // Execute query
            stopwatch.Start();
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

            stopwatch.Stop();

            return new ExecuteResult(dataTable, dataTable.Rows.Count, stopwatch.Elapsed);
        }

        public static ExecuteResult ExecuteNonQuery(string command)
        {
            Stopwatch stopwatch = new Stopwatch();
            int resultCount = 0;

            stopwatch.Start();
            var reader = _database.Execute(command);
            stopwatch.Stop();

            // Convert booleans to byte
            if (reader.Current.RawValue.GetType().Equals(typeof(bool)))
            {
                resultCount = ((bool)reader.Current.RawValue).ToByte();
            }
            else
            {
                resultCount = (int)reader.Current.RawValue;
            }

            return new ExecuteResult(null, resultCount, stopwatch.Elapsed);
        }

        public static string FormatFieldValue(object value)
        {
            Type valueType = value.GetType();

            // Treat null values as blank strings. This prevents errors when updating cell values to blank in DataGridView
            if (value == DBNull.Value)
            {
                return "''";
            }

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

        public static DataTable GetTableSchema(string tableName)
        {
            DataTable dataTable = new DataTable();

            // Get first row in passed table
            var bsonDocument = _database.GetCollection(tableName).FindAll().First();

            // Initialise return table
            dataTable.Columns.Add("Field");
            dataTable.Columns.Add("Type");
            
            // Add rows to return table
            foreach (var key in bsonDocument.Keys)
            {
                dataTable.Rows.Add(key, bsonDocument[key].Type.ToString());
                dataTable.AcceptChanges();
            }

            return dataTable;
        }

        public static void DeleteTable(string tableName)
        {
            ExecuteNonQuery($"DROP COLLECTION {tableName}");
        }
    }
}