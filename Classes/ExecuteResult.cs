using System;
using System.Data;

namespace LiteDBManager.Classes
{
    public class ExecuteResult
    {
        public DataTable Data { get; set; }
        public int Count { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public ExecuteResult(DataTable data, int count, TimeSpan elapsedTime)
        {
            Data = data;
            Count = count;
            ElapsedTime = elapsedTime;
        }
    }
}