using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public delegate void InformationAboutActualization(double value);
    public class CommunicationRoutesController
    {
        private InformationAboutActualization ShowNewTimeForActualization = null;
        public CommunicationRoutesController(ICommunicationRoutesGui communicationRoutesGui, CommunicationRoutesModel communicationRoutesModel)
        {
            _communicationRoutesGui = communicationRoutesGui;
            _communicationRoutesModel = communicationRoutesModel;
            SetEventHandler();
            SetDayTypesInGui();
            SetBusStops();
            SetDelagete();


        }

        private ICommunicationRoutesGui _communicationRoutesGui;
        private CommunicationRoutesModel _communicationRoutesModel;

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

        }

        private void SetBusStops()
        {
            _communicationRoutesGui.DisplayBusStops(_communicationRoutesModel.GiveListOfBusStopsNames());
        }     
    }
}
