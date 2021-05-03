using System;
using System.Linq;
using static Talrand.Core.Extensions;
using static LiteDBManager.Classes.Database.LiteDBWrapper;

namespace LiteDBManager.Classes.Database
{
    public class UpdateCommandBuilder : CommandBuilder
    {
        private string _whereClause = "";

        public void SetWhereClause(string whereClause)
        {
            _whereClause = whereClause;
        }

        public override string ToString()
        {
            string setClause = "";

            foreach(var field in _fields)
            {
                setClause = setClause.Join(", ", $"{field.Key} = {FormatFieldValue(field.Value)}");
            }

            return $"UPDATE {_tableName} SET {setClause} WHERE {_whereClause}";
        }
    }
}
