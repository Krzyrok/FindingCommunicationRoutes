﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FindingCommunicationRoutes
{
    public partial class CommunicationRoutesGui : Form, ICommunicationRoutesGui
    {

        public CommunicationRoutesGui()
        {
            InitializeComponent();
        }

        public void DisplayDayTypes(List<string> listOfDayTypes)
        {      
            foreach (string typeOfDay in listOfDayTypes)
            {
                typeOfDayComboBox.Items.Add(typeOfDay);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
