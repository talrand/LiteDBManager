using System;
using System.Windows.Forms;

namespace LiteDBManager.Classes
{
    public static class Globals
    {
        public static void DisplayError(Exception ex)
        {
            try
            {
                new Forms.frmSystemError { Exception = ex }.ShowDialog();
            }
            catch
            {
                // Ignore errors that occur when attempting to display the error form
            }
        }

        public static bool IsPasteKeys(KeyEventArgs e)
        {
            return e.Shift && e.KeyCode == Keys.Insert || e.Control && e.KeyCode == Keys.V;
        }
    }
}