using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;

namespace WindowsNewFileContextMenuEditor
{
    static class Program
    {
        const string MutexId = "{1429FD76-3F35-4B1E-A5EB-BA18BA2F3F4A}";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            if (arguments.Contains("/register") &&
                arguments.Contains("/unregister"))
            {
                Console.WriteLine("Error: Either register or unregister must be specified.");
            }
            else if (arguments.Contains("/register"))
            {
                ControlPanelRegistration.Register();
            }
            else if (arguments.Contains("/unregister"))
            {
                ControlPanelRegistration.Unregister();
            }
            else
            {
                using (var mutex = new Mutex(true, MutexId, out bool createdNew))
                {
                    if (createdNew)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                }
            }
        }
    }
}
