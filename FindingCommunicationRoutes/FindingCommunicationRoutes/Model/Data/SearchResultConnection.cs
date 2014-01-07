using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearchResultConnection
    {
        #region Properties

        public bool IsDirectConnection { get; private set; }
        public string LineNumber { get; private set; }
        public DateTime DepartureDateTime { get; private set; }
        public DateTime ArrivalDateTime { get; private set; }
        public TimeOfArrival TimeDistanceBetweenBusStops { get; private set; }
        public string StartBusStopName { get; private set; }
        public string EndBusStopName { get; private set; }

        #endregion
        
        #region Constructors

        public SearchResultConnection(bool isDirectConnection, string lineNumber, DateTime departureDateTime, DateTime arrivalDateTime, 
            TimeOfArrival timeDistanceBetweenBusStops, string startBusStopName, string endBusStopName) 
        {
            IsDirectConnection = isDirectConnection;
            LineNumber = lineNumber;
            DepartureDateTime = departureDateTime;
            ArrivalDateTime = arrivalDateTime;
            TimeDistanceBetweenBusStops = timeDistanceBetweenBusStops;
            StartBusStopName = startBusStopName;
            EndBusStopName = endBusStopName;
        }

        #endregion

        #region Public methods

        public void ChangeFieldsForDepartureOption(DateTime newDepartureDateTime, string newStartBusStopName)
        {
            DepartureDateTime = newDepartureDateTime;
            StartBusStopName = newStartBusStopName;

            TimeOfArrival departureTime = new TimeOfArrival(DepartureDateTime.Hour, DepartureDateTime.Minute);
            TimeOfArrival arrivalTime = new TimeOfArrival(ArrivalDateTime.Hour, ArrivalDateTime.Minute);
            TimeDistanceBetweenBusStops = arrivalTime - departureTime;
        }

        public void ChangeFieldsForArrivalOption(DateTime newArrivalDateTime, string newEndBusStopName)
        {
            ArrivalDateTime = newArrivalDateTime;
            EndBusStopName = newEndBusStopName;

            TimeOfArrival departureTime = new TimeOfArrival(DepartureDateTime.Hour, DepartureDateTime.Minute);
            TimeOfArrival arrivalTime = new TimeOfArrival(ArrivalDateTime.Hour, ArrivalDateTime.Minute);
            TimeDistanceBetweenBusStops = arrivalTime - departureTime;
        }

        public override string ToString()
        {
            return LineNumber;
        }

        #endregion
    }
}
