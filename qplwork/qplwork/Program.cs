using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace qplwork
{
    static class Program
    {
        public static Form1 form;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            if (args.Length == 1)
                form.OpenFile(args[0]);
            Application.Run(form);
        }
    }
}
