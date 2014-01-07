using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SoughtConnection
    {
        #region Properties

        public string StartBusStop { get; private set; }
        public string EndBusStop { get; private set; }
        public DateTime DateAndTime { get; private set; }
        public bool IsDeparture { get; private set; }

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
            StartBusStop = startBusStop;
            EndBusStop = endBusStop;
            DateAndTime = date;
            IsDeparture = isDeparture;
        }

        #endregion
    }
}
