using Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caroto
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (CarotoSettings.Default.Mock)
            {
                Properties.Settings.Default.Identidad = "";
                Properties.Settings.Default.ApiKey = "";
                Properties.Settings.Default.IsActivated = false;

                CarotoSettings.Default.AccessToken = "";

                Properties.Settings.Default.Save();
                CarotoSettings.Default.Save();
            }
            if (Properties.Settings.Default.IsActivated)
            {
                Application.Run(new StatusForm());
            }
            else
            {
                Application.Run(new InitialForm());
            }
        }
    }
}
