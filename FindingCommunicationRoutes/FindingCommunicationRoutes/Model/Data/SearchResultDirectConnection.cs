using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearchResultDirectConnection
    {
        #region Properties

        public string LineNumber { get; private set; }
        public TimeOfArrival DepartureTime { get; private set; }
        public TimeOfArrival ArrivalTime { get; private set; }
        public TimeOfArrival TimeDistanceBetweenBusStops { get; private set; }

        #endregion
        
        #region Constructors

        public SearchResultDirectConnection(string lineNumber, TimeOfArrival departureTime, TimeOfArrival arrivalTime, TimeOfArrival timeDistanceBetweenBusStops) 
        {
            LineNumber = lineNumber;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            TimeDistanceBetweenBusStops = timeDistanceBetweenBusStops;
        }

        #endregion

        #region Public methods

        public override string ToString()
        {
            return LineNumber;
        }

        #endregion
    }
}
