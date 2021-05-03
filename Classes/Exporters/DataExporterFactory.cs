using System;
using System.Windows.Forms;
using System.IO;

namespace LiteDBManager.Classes.Exporters
{
    public static class DataExporterFactory
    {
        public static IDataExporter Create(string fileName, DataGridView dataGridView)
        {
            switch (Path.GetExtension(fileName).ToLower())
            {
                case ".xml":
                    return new XmlDataExporter() { FileName = fileName, DataGridView = dataGridView };

                case ".json":
                    return new JsonDataExporter() { FileName = fileName, DataGridView = dataGridView };

                case ".csv":
                    return new CsvDataExporter() { FileName = fileName, DataGridView = dataGridView };

                default: throw new NotImplementedException();
            }
        }
    }
}