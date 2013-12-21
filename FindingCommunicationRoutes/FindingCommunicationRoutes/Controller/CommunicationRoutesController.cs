using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public delegate void UpdateInformationAboutActualization(double value);
    public class CommunicationRoutesController
    {
        #region Constructors
        public CommunicationRoutesController(ICommunicationRoutesGui communicationRoutesGui, CommunicationRoutesModel communicationRoutesModel)
        {
            _communicationRoutesGui = communicationRoutesGui;
            _communicationRoutesModel = communicationRoutesModel;
            SetEventHandler();
            SetDayTypesInGui();
            SetBusStops();
            SetDelagete();

            
        }
        #endregion

        #region Private fields
        private UpdateInformationAboutActualization ShowNewTimeForActualization = null;
        private ICommunicationRoutesGui _communicationRoutesGui;
        private CommunicationRoutesModel _communicationRoutesModel;
        #endregion

        #region Private methods
        private void SetEventHandler()
        {
        }

        private void SetDayTypesInGui()
        {
            Data.GeneratorOfDayTypes generatorOfDataTypes = new Data.GeneratorOfDayTypes();
            _communicationRoutesGui.DisplayDayTypes(generatorOfDataTypes.GenerateListOfDayTypes());
        }

        private void SetDelagete()
        {
            ShowNewTimeForActualization += ShowNewTime;
        }

        private void ShowNewTime(double value)
        {
            // here will be update for gui
        }

        private void SetBusStops()
        {
            //_communicationRoutesGui.DisplayBusStops(_communicationRoutesModel.GiveListOfBusStopsNames());
        }

        private void UpdateScheduleWasPressed()
        {
            _communicationRoutesModel.ActualizeRepository(ShowNewTime);
        }
        #endregion

    }
}
