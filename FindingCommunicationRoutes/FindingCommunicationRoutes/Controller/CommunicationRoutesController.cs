using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class CommunicationRoutesController
    {
        private ICommunicationRoutesGui _communicationRoutesGui;

        public CommunicationRoutesController(ICommunicationRoutesGui communicationRoutesGui)
        {
            _communicationRoutesGui = communicationRoutesGui;
            SetEventHandler();
        }

        private void SetEventHandler()
        {
        }
    }
}
