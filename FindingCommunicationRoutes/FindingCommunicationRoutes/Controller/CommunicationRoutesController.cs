using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FindingCommunicationRoutes
{
    
    public class CommunicationRoutesController
    {
        #region Constructors

        public CommunicationRoutesController(ICommunicationRoutesGui communicationRoutesGui, CommunicationRoutesModel communicationRoutesModel)
        {
            _communicationRoutesGui = communicationRoutesGui;
            _communicationRoutesModel = communicationRoutesModel;
            SetEventHandlers();
            SetDelagetes();
            SetCurrentDateAndTime();

            Thread setBusStopsThread = new Thread(new ThreadStart(SetBusStops));
            setBusStopsThread.Name = "Initialize Bus Stops Names";
            setBusStopsThread.Start();
            _communicationRoutesGui.SaveThread(setBusStopsThread);
        }

        #endregion

        #region Private fields

        private Delegates.UpdateInformationAboutActualization ShowNewTimeForActualization = null;
        private ICommunicationRoutesGui _communicationRoutesGui;
        private CommunicationRoutesModel _communicationRoutesModel;

        #endregion

        #region Private methods

        private void SetEventHandlers()
        {
            _communicationRoutesGui.LoadNewScheduleFromFile += UpdateScheduleWasPressed;
            _communicationRoutesGui.SearchRoute += SearchRoute;
        }

        private void SetDelagetes()
        {
            ShowNewTimeForActualization += ActualizeTime;
        }

        private void ActualizeTime(double value)
        {
            Action<string, int> updateTime = new Action<string, int>((valueString, valueInt) => _communicationRoutesGui.UpdateInformationAndTimeForLoadingNewSchedule(valueString, valueInt));
            if (value == 0.0)
            {      
                _communicationRoutesGui.Invoke(updateTime, "Decompilation is running, please wait.", 0); 
            }
            else if (value < 100.0)
            {
                _communicationRoutesGui.Invoke(updateTime, "Loading new schedules, please wait.", (int)value); 
            }
            else if (value == 99.0)
            {
                _communicationRoutesGui.Invoke(updateTime, "Displaying new bus stops.", 99);
            }
            else if (value == 100.0)
            {
                SetBusStops();
                _communicationRoutesGui.Invoke(updateTime, "Completed. New schedules loaded.", 100);
            }
        }

        private void SetCurrentDateAndTime()
        {
            DateTime actualDateTime = DateTime.Now;
            _communicationRoutesGui.SetDateAndTime(actualDateTime);
        }

        private void SetBusStops()
        {
            Action<string, int> updateTime = new Action<string, int>((valueString, valueInt) => _communicationRoutesGui.UpdateInformationAndTimeForLoadingNewSchedule(valueString, valueInt));
            _communicationRoutesGui.Invoke(updateTime, "Loading bus stops.", 0); 

            List<string> busStopsNamesList = _communicationRoutesModel.GiveListOfBusStopsNames();
            if (busStopsNamesList == null)
            {
                _communicationRoutesGui.Invoke(updateTime, "You have to choose the CHM file.", 100); 
                return;
            }

            Action<List<string>> displayBusStops = new Action<List<string>>((list) => _communicationRoutesGui.DisplayBusStops(list));
            _communicationRoutesGui.Invoke(displayBusStops, busStopsNamesList);

            _communicationRoutesGui.Invoke(updateTime, "Bus stops are loaded.", 100); 
        }

        private void UpdateScheduleWasPressed(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Schedule file|*.chm";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string pathToChm = openFileDialog.FileName;
                ActualizeRepositoryArgs args = new ActualizeRepositoryArgs(ShowNewTimeForActualization, pathToChm);
                System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
                Thread actualizeRepositoryThread = new Thread(new ParameterizedThreadStart(_communicationRoutesModel.ActualizeRepository));
                actualizeRepositoryThread.Name = "Actualize Repository Thread";
                actualizeRepositoryThread.Start(args);
                _communicationRoutesGui.SaveThread(actualizeRepositoryThread);
            }
        }

        private void SearchRoute(object sender, SoughtConnection arg)
        {
            if (arg.StartBusStop == "" || arg.EndBusStop == "" || arg.StartBusStop.Equals(arg.EndBusStop))
            {
                _communicationRoutesGui.ShowMessage("Wrong data. You have to choose proper bus stops");
                return;
            }
            SearchResultDirectConnection result = _communicationRoutesModel.SearchRoute(arg);
            _communicationRoutesGui.ShowResultOfSearching(result);
        }

        #endregion

    }
}
