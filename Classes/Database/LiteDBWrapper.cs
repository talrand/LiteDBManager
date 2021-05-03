using System;
using LiteDB;

namespace LiteDBManager.Classes.Database
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
     
        public static void SetDatabaseInfo(Database.DatabaseInfo databaseInfo)
        {
            _database = databaseInfo.Database;
            _databaseFileName = databaseInfo.FileName;
            _databaseName = databaseInfo.Name;
            _databaseReadOnly = databaseInfo.IsReadOnly;
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