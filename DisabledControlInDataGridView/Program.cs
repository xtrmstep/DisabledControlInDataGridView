using System;
using System.Windows.Forms;

namespace DisabledControlInDataGridView
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}