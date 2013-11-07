using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Represents a single track of a bus line.
    /// </summary>
    public class Track
    {
        public Dictionary<String, TimeOfArrival> TimeOfArrivalOnBusStops
        {
            get { return _timeOfArrivalOnBusStops; }
        }

        public Track(Dictionary<String, TimeOfArrival> TimeOfArrivalOnBusStops)
        {
            _timeOfArrivalOnBusStops = TimeOfArrivalOnBusStops;
        }

        private Dictionary<String, TimeOfArrival> _timeOfArrivalOnBusStops;
    }
}
