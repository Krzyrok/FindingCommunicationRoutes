﻿using System;
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
            Application.Run(new Form1());
            // test for git gui - commit 1
            // test git gui - commit 2
        }
    }
}
