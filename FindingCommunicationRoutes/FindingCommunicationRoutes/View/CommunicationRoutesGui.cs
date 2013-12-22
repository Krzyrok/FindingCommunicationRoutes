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
        #region Constructors

        public CommunicationRoutesGui()
        {
            InitializeComponent();
        }

        #endregion

        #region Public methods

        public void DisplayBusStops(List<string> listOfBusStopsNames)
        {
            foreach (string busStopName in listOfBusStopsNames)
            {
                startBusStopComboBox.Items.Add(busStopName);
                destinationBusStopComboBox.Items.Add(busStopName);
            }
        }

        public void SetDateAndTime(DateTime dateTime)
        {
            dateOfJourneyDateTimePicker.Value = dateTime.Date;
            hourTimeNumericUpDown.Value = dateTime.Hour;
            minuteTimeNumericUpDown.Value = dateTime.Minute;
        }

        #endregion

        #region Private methods

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
