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

        public event EventHandler LoadNewScheduleFromFile = null;
        public event EventHandler<SoughtConnection> SearchRoute = null;

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

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
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
                upperMenuStrip.Enabled = true;
                if (startBusStopComboBox.Text != "" && destinationBusStopComboBox.Text != "")
                {
                    searchButton.Enabled = true;                 
                }
            }
        }

        public void ShowResultOfSearching(SearchResultDirectConnection result)
        {
            lineResultTextBox.Text = result.LineNumber;
            timeOfDepartureResultTextBox.Text = result.DepartureTime.Hour.ToString() + ":" + result.DepartureTime.Minutes.ToString();
            timeOfArrivalResultTextBox.Text = result.ArrivalTime.Hour.ToString() + ":" + result.ArrivalTime.Minutes.ToString();
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
                LoadNewScheduleFromFile(sender, e);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (SearchRoute != null)
            {
                string startBusStop = startBusStopComboBox.Text;
                string endBusStop = destinationBusStopComboBox.Text;
                DateTime day = dateOfJourneyDateTimePicker.Value;
                int hour = (int)hourTimeNumericUpDown.Value;
                int minute = (int)minuteTimeNumericUpDown.Value;
                DateTime dateTime = new DateTime(day.Year, day.Month, day.Day, hour, minute, 0);
                bool departureChecked = departureRadioButton.Checked;
                
                SoughtConnection soughtConnection = new SoughtConnection(startBusStop, endBusStop, dateTime, departureChecked);
                SearchRoute(sender, soughtConnection);
            }
        }

        private void startBusStopComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (destinationBusStopComboBox.Text != "")
            {
                searchButton.Enabled = true;  
            }
        }

        private void destinationBusStopComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startBusStopComboBox.Text != "")
            {
                searchButton.Enabled = true;  
            }
        }

        #endregion
    }
}
