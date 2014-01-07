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
        public string PreviousBusStopNameForDepartureOrNextBusStopForArrival { get; private set; }
        public string LineNumberWhichLeadToThisBusStop { get; private set; }
        public TimeOfArrival TotalTimeFromStartBus { get; private set; }
        public DateTime ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival { get; private set; }
        public DateTime DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival { get; private set; }

        #endregion

        #region Constructors

        public SingleBusStopForIndirectConnection(string busStopName, string previousBusStopNameForDepartureOrNextBusStopForArrival, string lineNumberWhichLeadToThisBusStop, TimeOfArrival totalTimeFromStartBus,
            DateTime arrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival, DateTime departureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival)
        {
            BusStopName = busStopName;
            PreviousBusStopNameForDepartureOrNextBusStopForArrival = previousBusStopNameForDepartureOrNextBusStopForArrival;
            LineNumberWhichLeadToThisBusStop = lineNumberWhichLeadToThisBusStop;
            TotalTimeFromStartBus = totalTimeFromStartBus;
            ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival = arrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival;
            DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival = departureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival;
        }

        #endregion

        #region Public methods

        public void ChangeFields(string newPreviousBusStopNameForDepartureOrNextBusStopForArrival, string newLineNumberWhichLeadToThisBusStop,
            TimeOfArrival newTotalTimeFromStartBus, DateTime newArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival, DateTime newDepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival)
        {
            this.PreviousBusStopNameForDepartureOrNextBusStopForArrival = newPreviousBusStopNameForDepartureOrNextBusStopForArrival;
            this.LineNumberWhichLeadToThisBusStop = newLineNumberWhichLeadToThisBusStop;
            this.TotalTimeFromStartBus = newTotalTimeFromStartBus;
            this.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival = newArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival;
            this.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival = newDepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival;
        }

        #endregion
    }
}
