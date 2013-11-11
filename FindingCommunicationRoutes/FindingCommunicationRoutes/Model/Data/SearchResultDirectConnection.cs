using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearchResultDirectConnection
    {
        #region "Properties"
        public string LineNumber
        {
            get { return _lineNumber; }
        }

        public TimeOfArrival DepartureTime
        {
            get { return _departureTime; }
        }

        public TimeOfArrival ArrivalTime
        {
            get { return _arrivalTime; }
        }

        #endregion
        
        #region "Constructors"

        public SearchResultDirectConnection(string lineNumber, TimeOfArrival departureTime, TimeOfArrival arrivalTime) 
        {
            _lineNumber = lineNumber;
            _departureTime = departureTime;
            _arrivalTime = arrivalTime;
        }

        #endregion

        #region "Private fields"

        private string _lineNumber;
        private TimeOfArrival _departureTime;
        private TimeOfArrival _arrivalTime;

        #endregion
    }
}
