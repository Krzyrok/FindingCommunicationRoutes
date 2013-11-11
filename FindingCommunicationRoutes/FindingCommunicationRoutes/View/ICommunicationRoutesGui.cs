using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public interface ICommunicationRoutesGui
    {
        void DisplayDayTypes(List<string> listOfDayTypes);
        void DisplayBusStops(List<string> listOfBusStopsNames);
    }
}
