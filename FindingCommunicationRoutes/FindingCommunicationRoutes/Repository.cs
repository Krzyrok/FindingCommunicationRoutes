using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Repository of bus stops.
    /// </summary>
    public class Repository
    {
        public List<BusStop> BusStops
        {
            get { return _busStops; }
        }

        public Repository(List<BusStop> BusStops)
        {
            if (BusStops != null)
            {
                _busStops = new List<BusStop>();
                _busStops.AddRange(BusStops);
            }
        }

        private List<BusStop> _busStops;
    }
}
