using MyAstroGrep.Core;
using MyAstroGrep.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAstroGrep
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Language.Load(GeneralSettings.Language);

            Application.Run(new Windows.Forms.MainForm());
        }
    }
}
