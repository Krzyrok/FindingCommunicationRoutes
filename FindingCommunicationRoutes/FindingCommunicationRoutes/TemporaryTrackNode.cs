using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class TemporaryTrackNode
    {
        public string Line;
        public string BusStop;
        public TimeOfArrival Hour;
        public string NextBusStop;
        public string Letter;
        public string DayType;

        public TemporaryTrackNode()
        {
        }

        public TemporaryTrackNode(TimeOfArrival hour, string line, string busStop,
            string nextBusStop, string letter, string dayType)
        {
            Line = line;
            BusStop = busStop;
            Hour = hour;
            NextBusStop = nextBusStop;
            Letter = letter;
            DayType = dayType;
        }
    }
}
