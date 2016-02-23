using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Additions.ChangeLanguage changeLanguage = new Additions.ChangeLanguage();
            changeLanguage.LoadSettings();
            changeLanguage = null;

            Application.Run(new MainForm());
        }
    }
}
