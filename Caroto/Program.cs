using Caroto.RecurringTasks;
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
        /// 
        private static ComposeRecurringTasks _recurringTaskComposer;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.IsActivated)
            {
                _recurringTaskComposer = new ComposeRecurringTasks();
                _recurringTaskComposer.ComposeTasks();
                Application.Run(new StatusForm());
            }
            else
            {
                Application.Run(new InitialForm(new ComposeRecurringTasks()));
            }
        }
    }
}
