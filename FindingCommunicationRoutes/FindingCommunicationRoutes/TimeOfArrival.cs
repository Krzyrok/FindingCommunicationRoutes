using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Represents the hour and minutes when bus is arriving at the bus stop.
    /// </summary>
    public class TimeOfArrival
    {
        public int Hour
        {
            get { return _hour; }
        }

        public int Minutes
        {
            get { return _minutes; }
        }

        public TimeOfArrival(int hour, int minutes)
        {
            _hour = hour;
            _minutes = minutes;
        }

        private int _hour;
        private int _minutes;
    }
}
