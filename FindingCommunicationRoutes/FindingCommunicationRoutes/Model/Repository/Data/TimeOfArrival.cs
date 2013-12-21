using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Represents the hour and minutes when bus is arriving at the bus stop.
    /// </summary>
    [Serializable]
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

        public TimeOfArrival(TimeOfArrival toa)
        {
            _hour = toa.Hour;
            _minutes = toa.Minutes;
        }

        public TimeOfArrival(int hour, int minutes)
        {
            if (hour <= 23 && hour >= 0 && minutes <= 59 && minutes >= 0)
            {
                _hour = hour;
                _minutes = minutes;
            }
            else
            {
                throw new ArgumentException("Bad value for 'hour' or 'minutes'");
            }
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

        public static TimeOfArrival operator +(TimeOfArrival arg1, TimeOfArrival arg2)
        {
            TimeOfArrival tmpTime = new TimeOfArrival(0,0);
            tmpTime._minutes += arg1._minutes + arg2._minutes;
            if (tmpTime.Minutes > 59)
            {
                tmpTime._minutes -= 60;
                ++tmpTime._hour;
            }
            tmpTime._hour += arg1._hour + arg2._hour;
            if (tmpTime._hour > 23)
            {
                tmpTime._hour -= 24;
            }
            return tmpTime;
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
