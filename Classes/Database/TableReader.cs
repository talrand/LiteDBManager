using System;
using System.Collections.Generic;
using LiteDB;
using System.Linq;
using System.Data;

namespace LiteDBManager.Classes.Database
{
    public class TableReader
    {
        public List<string> ReadNames(string tableType)
        {
            var tableNames = new List<string>();
            var reader = LiteDBWrapper.Database.Execute($"SELECT name from $cols WHERE Type = '{tableType}'");

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

        public DataTable ReadSchema(string tableName)
        {
            DataTable dataTable = new DataTable();

            // Get first row in passed table
            var bsonDocument = LiteDBWrapper.Database.GetCollection(tableName).FindAll().First();

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
    }
}