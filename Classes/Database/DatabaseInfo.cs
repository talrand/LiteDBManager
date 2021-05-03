using System;
using LiteDB;
using System.IO;

namespace LiteDBManager.Classes.Database
{
    public class DatabaseInfo
    {
        public LiteDatabase Database { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public bool IsReadOnly { get; set; }

        public DatabaseInfo(LiteDatabase database = null, string fileName = "", bool isReadOnly = false)
        {
            Database = database;
            FileName = fileName;
            Name = Path.GetFileName(fileName);
            IsReadOnly = isReadOnly;
        }
    }
}