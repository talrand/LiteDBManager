using System;
using System.Collections.Generic;

namespace LiteDBManager.Classes
{
    public abstract class CommandBuilder
    {
        protected string _tableName = "";
        protected readonly Dictionary<string, object> _fields = new Dictionary<string, object>();

        public void SetTableName(string tableName)
        {
            _tableName = tableName;
        }

        public void AddField(string name, object value)
        {
            _fields.Add(name, value);
        }

        public abstract override string ToString();
    }
}