﻿using System;
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
            SetBusStops();                    
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
            else if (value == 100.0)
            {
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
            //_communicationRoutesGui.DisplayBusStops(_communicationRoutesModel.GiveListOfBusStopsNames());
        }

        private void UpdateScheduleWasPressed()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Schedule file|*.chm";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string pathToChm = openFileDialog.FileName;
                ActualizeRepositoryArgs args = new ActualizeRepositoryArgs(ShowNewTimeForActualization, pathToChm);
                Thread actualizeRepositoryThread = new Thread(new ParameterizedThreadStart(_communicationRoutesModel.ActualizeRepository));
                actualizeRepositoryThread.Start(args);
            }
        }

        #endregion

    }
}
