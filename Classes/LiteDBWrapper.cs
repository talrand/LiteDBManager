using System;
using System.Collections.Generic;
using LiteDB;
using System.IO;
using System.Data;
using System.Text;
using System.Diagnostics;
using static LiteDBManager.Classes.BsonTypeToSystemTypeAdapter;
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
        private static string _databaseFileName = "";
        private static bool _databaseReadOnly = false;

        public static string DatabaseName { get { return _databaseName; } }
        public static string DatabaseFileName { get { return _databaseFileName; } }
        public static bool DatabaseReadOnly { get { return _databaseReadOnly; } }
        internal static LiteDatabase Database { get { return _database; } }


        public static void OpenDatabase(string fileName, string password, string connectionMethod)
        {
            // Connect to database - this will create it if it doesn't exist
            _database = new LiteDatabase(BuildConnectionString(fileName, password, connectionMethod));
            
            // Store database name + path for display in database explorer
            _databaseName = Path.GetFileName(fileName);
            _databaseFileName = fileName;

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
            _databaseName = "";
            _databaseFileName = "";
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

        public static void RebuildDatabase()
        {
            _database.Rebuild();
        }
    }
}