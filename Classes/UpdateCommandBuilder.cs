using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Talrand.Core.Extensions;
using static LiteDBManager.Classes.DatabaseWrapper;

namespace LiteDBManager.Classes
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
            //       Database.Execute($"UPDATE {_currentTable} SET {columnName} = {FormatFieldValue(cell.Value)} WHERE _id = {{\"$oid\": \"{id}\"}}");


            string setClause = "";

            foreach(var field in _fields)
            {
                setClause = setClause.Join(", ", $"{field.Key} = {FormatFieldValue(field.Value)}");
            }

            return $"UPDATE {_tableName} SET {setClause} WHERE {_whereClause}";
        }
    }
}
