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

        public static bool operator >(TimeOfArrival arg1, TimeOfArrival arg2)
        {
            if (arg1.Hour > arg2.Hour)
            {
                return true;
            }
            else if (arg1.Hour < arg2.Hour)
            {
                return false;
            }
            else if (arg1.Minutes >= arg2.Minutes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <(TimeOfArrival arg1, TimeOfArrival arg2)
        {
            if (arg1.Hour < arg2.Hour)
            {
                return true;
            }
            else if (arg1.Hour > arg2.Hour)
            {
                return false;
            }
            else if (arg1.Minutes < arg2.Minutes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int _hour;
        private int _minutes;
    }
}
