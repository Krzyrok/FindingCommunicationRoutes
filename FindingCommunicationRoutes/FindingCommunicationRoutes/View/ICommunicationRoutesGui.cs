using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public interface ICommunicationRoutesGui
    {
        void DisplayBusStops(List<string> listOfBusStopsNames);
        void SetDateAndTime(DateTime dateTime);
    }
}
