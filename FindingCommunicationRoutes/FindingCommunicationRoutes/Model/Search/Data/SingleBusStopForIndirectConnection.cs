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
        public DateTime ArrivalDateTimeAtThisBusStop { get; private set; }
        public DateTime DepartureDateTimeFromPreviousBusStop { get; private set; }

        #endregion

        #region Constructors

        public SingleBusStopForIndirectConnection(string busStopName, string previousBusStopName, string lineNumberWhichLeadToThisBusStop,
            TimeOfArrival totalTimeFromStartBus, DateTime arrivalDateTimeAtThisBusStop, DateTime departureDateTimeFromPreviousBusStop)
        {
            BusStopName = busStopName;
            PreviousBusStopName = previousBusStopName;
            LineNumberWhichLeadToThisBusStop = lineNumberWhichLeadToThisBusStop;
            TotalTimeFromStartBus = totalTimeFromStartBus;
            ArrivalDateTimeAtThisBusStop = arrivalDateTimeAtThisBusStop;
            DepartureDateTimeFromPreviousBusStop = departureDateTimeFromPreviousBusStop;
        }

        #endregion

        #region Public methods

        public void ChangeFields(string newPreviousBusStopName, string newLineNumberWhichLeadToThisBusStop,
            TimeOfArrival newTotalTimeFromStartBus, DateTime newArrivalTimeAtThisBusStop, DateTime newDepartureTimeFromPreviousBusStop)
        {
            this.PreviousBusStopName = newPreviousBusStopName;
            this.LineNumberWhichLeadToThisBusStop = newLineNumberWhichLeadToThisBusStop;
            this.TotalTimeFromStartBus = newTotalTimeFromStartBus;
            this.ArrivalTimeAtThisBusStop = newArrivalTimeAtThisBusStop;
            this.DepartureTimeFromPreviousBusStop = newDepartureTimeFromPreviousBusStop;
        }

        #endregion
    }
}
