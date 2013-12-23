using System;
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
            _threadsList = new List<System.Threading.Thread>();
        }

        #endregion

        #region Public fields

        public event Delegates.LoadNewSchedule LoadNewScheduleFromFile = null;

        #endregion

        #region Public methods

        public bool CheckIfInvokeRequired()
        {
            return this.InvokeRequired;
        }

        public void SaveThread(System.Threading.Thread threadName)
        {
            _threadsList.Add(threadName);
        }

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

        public void UpdateInformationAndTimeForLoadingNewSchedule(string information, int valueOfProgressBar)
        {
            informationLabel.Text = information;
            informationAboutActualizationProgressBar.Value = valueOfProgressBar;
            if (valueOfProgressBar != 100)
            {
                searchButton.Enabled = false;
                upperMenuStrip.Enabled = false;
            }
            else
            {
                searchButton.Enabled = true;
                upperMenuStrip.Enabled = true;
            }
        }

        #endregion

        #region Private fields

        List<System.Threading.Thread> _threadsList;

        #endregion

        #region Private methods

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void updateScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadNewScheduleFromFile != null)
            {
                LoadNewScheduleFromFile();
            }
        }

        #endregion
    }
}
