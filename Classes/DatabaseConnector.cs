using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using static Talrand.Core.ProcessManager;
using static LiteDBManager.Classes.Globals;

namespace LiteDBManager.Classes
{
    public class DatabaseConnector
    {
        public static void OpenDatabase(string fileName, string password, string connectionMethod)
        {
            // Connect to database - this will create it if it doesn't exist
            Database = new LiteDatabase(BuildConnectionString(fileName, password, connectionMethod));

            // Store database name + path for display in database explorer
            _databaseName = Path.GetFileName(fileName);
            _databaseFileName = fileName;

            // Store database file in recent files log
            RecentFiles.Write(fileName);
        }

        private static string BuildConnectionString(string fileName, string password, string connectionMethod)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"filename={fileName};");

            if (password != "")
            {
                stringBuilder.Append($"password={password};");
            }

            if (IsDatabaseReadOnly(fileName) || IsFileLocked(fileName))
            {
                _databaseReadOnly = true;

                // Read only & locked databases can only be opened in shared mode
                stringBuilder.Append($"connection={ConnectionMethod.Shared};readonly=true;");
            }
            else
            {
                _databaseReadOnly = false;

                stringBuilder.Append($"connection={connectionMethod};");
            }

            return stringBuilder.ToString();
        }

        private static bool IsDatabaseReadOnly(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.IsReadOnly;
        }

        public static void CloseDatabase()
        {
            _database?.Dispose();
            _database = null;
            _databaseName = "";
            _databaseFileName = "";
        }
    }
}
