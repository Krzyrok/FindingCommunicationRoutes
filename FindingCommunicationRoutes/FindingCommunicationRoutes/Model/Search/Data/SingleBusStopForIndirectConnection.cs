using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SingleBusStopForIndirectConnection
    {
        #region Public fields

        public string BusStopName { get; private set; }
        public string PreviousBusStopName { get; private set; }
        public string LineNumber { get; private set; }
        public TimeOfArrival TotalTimeFromStartBus { get; private set; }
        public TimeOfArrival ArrivalTimeAtThisBusStop { get; private set; }
        public TimeOfArrival DepartureTimeFromPreviousBusStop { get; private set; }

        #endregion

        #region Constructors

        public SingleBusStopForIndirectConnection(string busStopName, string previousBusStopName, string lineNumber,
            TimeOfArrival totalTimeFromStartBus, TimeOfArrival arrivalTimeAtThisBusStop, TimeOfArrival departureTimeFromPreviousBusStop)
        {
            BusStopName = busStopName;
            PreviousBusStopName = previousBusStopName;
            LineNumber = lineNumber;
            TotalTimeFromStartBus = totalTimeFromStartBus;
            ArrivalTimeAtThisBusStop = arrivalTimeAtThisBusStop;
            DepartureTimeFromPreviousBusStop = departureTimeFromPreviousBusStop;
        }

        #endregion
    }
}
