using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SoughtConnection : EventArgs
    {
        #region Properties

        public string StartBusStop
        {
            get { return _startBusStop; }
        }

        public string EndBusStop
        {
            get { return _endBusStop; }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
        }

        public bool IsDeparture
        {
            get { return _isDeparture; }
        }

        #endregion

        #region Constructors

        public SoughtConnection(string startBusStop, string endBusStop, int year, int month, 
            int day, int hour, int minutes, bool isDeparture)
            : this(startBusStop, endBusStop,
                new DateTime(year, month, day, hour, minutes, 0),
                isDeparture)
        {
        }

        public SoughtConnection(string startBusStop, string endBusStop, DateTime date,
            int hour, int minutes, bool isDeparture)
            : this(startBusStop, endBusStop, 
                    new DateTime(date.Year, date.Month, date.Day, hour, minutes, 0), 
                    isDeparture)
        {
        }

        public SoughtConnection(string startBusStop, string endBusStop, DateTime date, 
            bool isDeparture)
        {
            _startBusStop = startBusStop;
            _endBusStop = endBusStop;
            _dateTime = date;
            _isDeparture = isDeparture;
        }

        #endregion

        #region Private fields

        private string _startBusStop;
        private string _endBusStop;
        private DateTime _dateTime;
        private bool _isDeparture;

        #endregion
    }
}
