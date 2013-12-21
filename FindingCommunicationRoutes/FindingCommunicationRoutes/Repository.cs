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
        /// <summary>
        /// Gets the list of bus stops.
        /// </summary>
        /// <value>
        /// The bus stops list.
        /// </value>
        public List<BusStop> BusStops
        {
            get { return _busStops; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="BusStops">The bus stops list to store.</param>
        public Repository(List<BusStop> BusStops)
        {
            if (BusStops != null)
            {
                _busStops = new List<BusStop>();
                _busStops.AddRange(BusStops);
            }
        }

        /// <summary>
        /// The stored bus stops list.
        /// </summary>
        private List<BusStop> _busStops;
    }
}
