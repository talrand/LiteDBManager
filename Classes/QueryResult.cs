using System;
using System.Data;

namespace LiteDBManager.Classes
{
    public class QueryResult
    {
        public DataTable Data { get; set; }
        public int Count { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public QueryResult(DataTable data, int count, TimeSpan elapsedTime)
        {
            Data = data;
            Count = count;
            ElapsedTime = elapsedTime;
        }
    }
}