using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class CommunicationRoutesController
    {
        public CommunicationRoutesController(ICommunicationRoutesGui communicationRoutesGui, CommunicationRoutesModel communicationRoutesModel)
        {
            _communicationRoutesGui = communicationRoutesGui;
            _communicationRoutesModel = communicationRoutesModel;
            SetEventHandler();
            SetDayTypesInGui();
            SetBusStops();
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

        private void SetBusStops()
        {
            _communicationRoutesGui.DisplayBusStops(_communicationRoutesModel.GiveListOfBusStopsNames());
        }     
    }
}
