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
        public string LineNumberWhichLeadToThisBusStop { get; private set; }
        public TimeOfArrival TotalTimeFromStartBus { get; private set; }
        public DateTime ArrivalTimeAtThisBusStop { get; private set; }
        public DateTime DepartureTimeFromPreviousBusStop { get; private set; }

        #endregion

        #region Constructors

        public SingleBusStopForIndirectConnection(string busStopName, string previousBusStopName, string lineNumberWhichLeadToThisBusStop,
            TimeOfArrival totalTimeFromStartBus, DateTime arrivalTimeAtThisBusStop, DateTime departureTimeFromPreviousBusStop)
        {
            BusStopName = busStopName;
            PreviousBusStopName = previousBusStopName;
            LineNumberWhichLeadToThisBusStop = lineNumberWhichLeadToThisBusStop;
            TotalTimeFromStartBus = totalTimeFromStartBus;
            ArrivalTimeAtThisBusStop = arrivalTimeAtThisBusStop;
            DepartureTimeFromPreviousBusStop = departureTimeFromPreviousBusStop;
        }

        #endregion
    }
}
