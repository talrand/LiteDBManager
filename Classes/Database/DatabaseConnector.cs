using System;
using System.IO;
using System.Text;
using LiteDB;
using static LiteDBManager.Classes.Database.LiteDBWrapper;
using static Talrand.Core.ProcessManager;

namespace LiteDBManager.Classes.Database
{
    public class DatabaseConnector
    {
        public DatabaseInfo Connect(string fileName, string password, string connectionMethod)
        {
            DatabaseInfo databaseInfo = new DatabaseInfo();

            if (IsFileLocked(fileName))
            {
                throw new FileLoadException("File is in use by another process");
            }

            if (IsDatabaseReadOnly(fileName))
            {
                databaseInfo.IsReadOnly = true;
            }

            // Connect to database - this will create it if it doesn't exist
            databaseInfo.Database = new LiteDatabase(BuildConnectionString(fileName, password, connectionMethod, databaseInfo.IsReadOnly));

            // Store database name + path for display in database explorer
            databaseInfo.FileName = fileName;
            databaseInfo.Name = Path.GetFileName(fileName);

            // Store database file in recent files log
            RecentFiles.Write(fileName);

            return databaseInfo;
        }

        private static string BuildConnectionString(string fileName, string password, string connectionMethod, bool databaseReadOnly)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"filename={fileName};");

            if (password != "")
            {
                stringBuilder.Append($"password={password};");
            }

            if (databaseReadOnly)
            {
                // Read only & locked databases can only be opened in shared mode
                stringBuilder.Append($"connection={ConnectionMethod.Shared};readonly=true;");
            }
            else
            {
                stringBuilder.Append($"connection={connectionMethod};");
            }

            return stringBuilder.ToString();
        }

        private static bool IsDatabaseReadOnly(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.IsReadOnly;
        }
    }
}