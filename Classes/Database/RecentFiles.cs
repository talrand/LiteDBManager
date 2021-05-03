using System;
using System.Collections.Generic;
using System.IO;

namespace LiteDBManager.Classes.Database
{
    public static class RecentFiles
    {
        private static List<string> _files = new List<string>();
        private static string _logFileName = "";

        public static List<string> Files { get { return _files; } }

        public static void Read()
        {
            string[] fileNames = null;

            _logFileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\LiteDBManager\\RecentFiles.txt";

            // Don't continue if file hasn't been created yet
            if (File.Exists(_logFileName) == false)
            {
                return;
            }

            // Get collection of files
            fileNames = File.ReadAllLines(_logFileName);

            // Add fileName to list
            foreach(string fileName in fileNames)
            {
                _files.Add(fileName);
            }
        }
         
        public static void Write(string fileName)
        {
            // Remove file from collection
            if (_files.Contains(fileName) == true)
            {
                _files.Remove(fileName);
            }

            // Insert passed file to list
            _files.Insert(0, fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(_logFileName));

            // Write 
            using (var writer = new StreamWriter(_logFileName, false))
            {
                for(int i=0; i < _files.Count; i++)
                {
                    // Only keep last 10 files in log
                    if (i > 9)
                    {
                        break;
                    }

                    // Write to file
                    writer.WriteLine(_files[i]);
                }
            }
        }
    }
}