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

            Repository repository = new Repository(new List<BusStop>());
            CommunicationRoutesModel model = new CommunicationRoutesModel(repository);


            CommunicationRoutesController controller = new CommunicationRoutesController(gui, model);
            Application.Run(gui);
        }
    }
}
