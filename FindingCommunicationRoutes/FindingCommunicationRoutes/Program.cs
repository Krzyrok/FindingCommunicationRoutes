using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FindingCommunicationRoutes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CommunicationRoutesGui gui = new CommunicationRoutesGui();
            CommunicationRoutesController controller = new CommunicationRoutesController(gui);
            Application.Run(gui);
        }
    }
}
