using System;
using System.Windows.Forms;
using Talrand.Core;
using LiteDBManager.Classes.Exporters;

namespace LiteDBManager.Classes
{
    public static class DataGridViewExtensions
    {
        public static object DefaultValue(this DataGridViewCell cell)
        {
            Type columnValueType = cell.DataGridView.Columns[cell.ColumnIndex].ValueType;

            // Default numeric columns to 0
            if (columnValueType.Equals(typeof(int)) || columnValueType.Equals(typeof(byte)) || columnValueType.Equals(typeof(decimal)) || columnValueType.Equals(typeof(double)))
            {
                return 0;
            }

            // Default boolean columns to false
            if (columnValueType.Equals(typeof(bool)))
            {
                return false;
            }

            // Default string columns to blank string
            if (columnValueType.Equals(typeof(string)))
            {
                return String.Empty;
            }

            return null;
        }
    }
}