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
            _actualValueOfProgressBar = informationAboutActualizationProgressBar.Value;
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
            _actualValueOfProgressBar = valueOfProgressBar;
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
                timeOfDepartureDirectResultTextBox.Text = directConnection.DepartureTime.ToString();
                timeOfArrivalDirectResultTextBox.Text = directConnection.ArrivalTime.ToString();
                totalTravelTimeDirectResultTextBox.Text = directConnection.TimeDistanceBetweenBusStops.ToString();
                departureDateDirectResultTextBox.Text = directConnection.DateOfDeparture.Day + "."
                    + directConnection.DateOfDeparture.Month + "." + directConnection.DateOfDeparture.Year;
                
                results.Remove(directConnection);
            }


            if (results.Count > 0)
            {
                SearchResultConnection firstSearchResult = results.First();
                SearchResultConnection lastSearchResult = results.Last();
                totalTravelTimeIndirectResultTextBox.Text = (lastSearchResult.ArrivalTime - firstSearchResult.DepartureTime).ToString();

                for (int i = 0; i < results.Count; i++)
                {
                    ListViewItem singleDirectRouteOfIndirectConnection = new ListViewItem();
                    singleDirectRouteOfIndirectConnection.Text = results[i].StartBusStopName;
                    singleDirectRouteOfIndirectConnection.SubItems.Add(results[i].EndBusStopName);
                    indirectConnectionDetailsListView.Items.Add(singleDirectRouteOfIndirectConnection);
                }
                _indirectConnection = results;
            }
        }

        #endregion

        #region Private fields

        int _actualValueOfProgressBar;
        List<SearchResultConnection> _indirectConnection;
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
            ResetTextBoxes();
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
            if (destinationBusStopComboBox.Text != "" && _actualValueOfProgressBar == 100)
            {
                searchButton.Enabled = true;  
            }
        }

        private void destinationBusStopComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startBusStopComboBox.Text != "" && _actualValueOfProgressBar == 100)
            {
                searchButton.Enabled = true;  
            }
        }

        private void ResetTextBoxes()
        {
            lineDirectResultTextBox.Text = "";
            timeOfDepartureDirectResultTextBox.Text = "";
            timeOfArrivalDirectResultTextBox.Text = "";
            departureDateDirectResultTextBox.Text = "";
            totalTravelTimeDirectResultTextBox.Text = "";

            lineIndirectResultTextBox.Text = "";
            timeOfDepartureIndirectResultTextBox.Text = "";
            timeOfArrivalIndirectResultTextBox.Text = "";
            departureDateIndirectResultTextBox.Text = "";
            travelTimeIndirectResultTextBox.Text = "";
            totalTravelTimeIndirectResultTextBox.Text = "";

            indirectConnectionDetailsListView.Items.Clear();
        }

        private void indirectConnectionDetailsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection selectedIndexCollection = indirectConnectionDetailsListView.SelectedIndices;
            if (selectedIndexCollection.Count > 0)
            {
                int index = selectedIndexCollection[0];
                SearchResultConnection directFragmentOfConnection = _indirectConnection[index];

                lineIndirectResultTextBox.Text = directFragmentOfConnection.LineNumber;
                timeOfDepartureIndirectResultTextBox.Text = directFragmentOfConnection.DepartureTime.ToString();
                timeOfArrivalIndirectResultTextBox.Text = directFragmentOfConnection.ArrivalTime.ToString();
                departureDateIndirectResultTextBox.Text = directFragmentOfConnection.DateOfDeparture.Day 
                    + "." + directFragmentOfConnection.DateOfDeparture.Month + "." + directFragmentOfConnection.DateOfDeparture.Year;
                travelTimeIndirectResultTextBox.Text = directFragmentOfConnection.TimeDistanceBetweenBusStops.ToString();
            }
            
        }

        #endregion
    }
}
