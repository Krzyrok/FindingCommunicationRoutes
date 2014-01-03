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

        public void UpdateInformationAndTimeForProgressBar(string information, int valueOfProgressBar)
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

        public void ShowResultsOfSearching(List<SearchResultConnection> results)
        {
            SearchResultConnection directConnection = results.First();
            if (directConnection == null)
            {
                return;
            }
            if (directConnection.IsDirectConnection)
            {
                lineDirectResultTextBox.Text = directConnection.LineNumber;
                timeOfDepartureDirectResultTextBox.Text = ("0" + directConnection.DepartureTime.Hour.ToString()).GetLastCharacters(2)
                    + ":" + ("0" + directConnection.DepartureTime.Minutes.ToString()).GetLastCharacters(2);
                timeOfArrivalDirectResultTextBox.Text = ("0" + directConnection.ArrivalTime.Hour.ToString()).GetLastCharacters(2)
                    + ":" + ("0" + directConnection.ArrivalTime.Minutes.ToString()).GetLastCharacters(2);
                totalTravelTimeDirectResultTextBox.Text = ("0" + directConnection.TimeDistanceBetweenBusStops.Hour.ToString()).GetLastCharacters(2)
                    + ":" + ("0" + directConnection.TimeDistanceBetweenBusStops.Minutes.ToString()).GetLastCharacters(2);
                results.Remove(directConnection);
                departureDateDirectResultTextBox.Text = directConnection.DateOfDeparture.Day.ToString() + "." 
                    + directConnection.DateOfDeparture.Month.ToString() + "." + directConnection.DateOfDeparture.Year.ToString();
            }

            for (int i = 0; i < results.Count; i++)
            {
                // Display indirect connection
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

    #region Extension for 'string'

    public static class ExtensionForString
    {
        public static string GetLastCharacters(this string sourceString, int howManyCharactersFromTheLastPosition)
        {
            if (howManyCharactersFromTheLastPosition >= sourceString.Length)
                return sourceString;
            if (howManyCharactersFromTheLastPosition < 0)
            {
                throw new ArgumentException("Negative value of 'howManyCharactersFromTheLastPosition'");
            }
            return sourceString.Substring(sourceString.Length - howManyCharactersFromTheLastPosition);
        }
    }

    #endregion
}
