using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SoughtConnection
    {
        #region "Properties"
        public string StartBusStop
        {
            get { return _startBusStop; }
        }

        public string EndBusStop
        {
            get { return _endBusStop; }
        }

        public string TypeOfDay
        {
            get { return _typeOfDay; }
        }

        public TimeOfArrival TimeOfArrival
        {
            get { return _timeOfArrival; }
        }

        public bool IsDeparture
        {
            get { return _isDeparture; }
        }

        public bool IsDirectConnection
        {
            get { return _isDirectConnection; }
        }
        #endregion

        #region "Constructors"

        public SoughtConnection(string startBusStop, string endBusStop, string typeOfDay,
            int hour, int minutes, bool isDeparture, bool isDirectConnection) 
            : this(startBusStop, endBusStop, typeOfDay,
                    new TimeOfArrival(hour, minutes), isDeparture, isDirectConnection)
        {
        }

        public SoughtConnection(string startBusStop, string endBusStop, string typeOfDay,
            TimeOfArrival timeOfArrival, bool isDeparture, bool isDirectConnection)
        {
            _startBusStop = startBusStop;
            _endBusStop = endBusStop;
            _typeOfDay = typeOfDay;
            _timeOfArrival = timeOfArrival;
            _isDeparture = isDeparture;
            _isDirectConnection = isDirectConnection;
        }

        #endregion

        #region "Private fields"

        private string _startBusStop;
        private string _endBusStop;
        private string _typeOfDay;
        private TimeOfArrival _timeOfArrival;
        private bool _isDeparture;
        private bool _isDirectConnection;

        #endregion
    }
}
