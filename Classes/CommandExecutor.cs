using System;
using System.Data;
using System.Diagnostics;
using static LiteDBManager.Classes.LiteDBWrapper;
using static Talrand.Core.Extensions;

namespace LiteDBManager.Classes
{
    public class CommandExecutor
    {
        public ExecuteResult ExecuteQuery(string query)
        {
            DataTable dataTable = null;
            Stopwatch stopwatch = new Stopwatch();
            BsonDataReaderToDataTableAdapter adapter = new BsonDataReaderToDataTableAdapter();

            // Execute query
            stopwatch.Start();
            var reader = Database.Execute(query);

            dataTable = adapter.Convert(reader);

            stopwatch.Stop();

            return new ExecuteResult(dataTable, dataTable.Rows.Count, stopwatch.Elapsed);
        }

        public ExecuteResult ExecuteNonQuery(string command)
        {
            Stopwatch stopwatch = new Stopwatch();
            int resultCount = 0;

            stopwatch.Start();
            var reader = Database.Execute(command);
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

        public void DeleteTable(string tableName)
        {
            ExecuteNonQuery($"DROP COLLECTION {tableName}");
        }
    }
}