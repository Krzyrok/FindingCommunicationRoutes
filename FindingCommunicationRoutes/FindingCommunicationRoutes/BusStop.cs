using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Represents a single bus stop.
    /// </summary>
    public class BusStop
    {
        #region public fields
        /// <summary>
        /// Gets the lines plying through bus stop.
        /// </summary>
        /// <value>
        /// The lines plying through bus stop.
        /// </value>
        public List<Line> LinesPlyingThroughBusStop
        {
            get { return _linesPlyingThroughBusStop; }
        }

        /// <summary>
        /// Gets the name of the bus stop.
        /// </summary>
        /// <value>
        /// The name of the bus stop.
        /// </value>
        public string BusStopName
        {
            get { return _busStopName; }
        }
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BusStop"/> class.
        /// </summary>
        /// <param name="LinesPlyingThroughBusStop">The lines plying through bus stop.</param>
        /// <param name="busStopName">Name of the bus stop.</param>
        public BusStop(List<Line> LinesPlyingThroughBusStop, string busStopName)
        {
            if (LinesPlyingThroughBusStop != null)
            {
                _linesPlyingThroughBusStop = new List<Line>();
                _linesPlyingThroughBusStop.AddRange(LinesPlyingThroughBusStop);
            }

            _busStopName = busStopName;
        }
        #endregion

        #region private fields

        /// <summary>
        /// The lines plying through bus stop
        /// </summary>
        private List<Line> _linesPlyingThroughBusStop;

        /// <summary>
        /// The bus stop name
        /// </summary>
        private string _busStopName;
        #endregion
    }
}
