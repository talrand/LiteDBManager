using System;
using System.Windows.Forms;

namespace LiteDBManager.Classes.Exporters
{
    public interface IDataExporter
    {
        string FileName { set; }
        DataGridView DataGridView { set; }
        void Run();
    }
}