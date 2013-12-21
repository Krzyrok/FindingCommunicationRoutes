using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Structure used for storing data about track node before track build.
    /// </summary>
    public class TemporaryTrackNode
    {
        #region public fields
        /// <summary>
        /// The bus line number.
        /// </summary>
        public string BusLineNumber;

        /// <summary>
        /// The bus stop name.
        /// </summary>
        public string BusStopName;

        /// <summary>
        /// The hour of arrival.
        /// </summary>
        public TimeOfArrival Hour;

        /// <summary>
        /// The next bus stop name.
        /// </summary>
        public string NextBusStopName;

        /// <summary>
        /// The letter next to arrival time.
        /// </summary>
        public string Letter;

        /// <summary>
        /// Type of the day.
        /// </summary>
        public string DayType;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryTrackNode"/> class.
        /// </summary>
        public TemporaryTrackNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporaryTrackNode"/> class.
        /// </summary>
        /// <param name="hour">The hour of arrival.</param>
        /// <param name="busLineNumber">The bus line number.</param>
        /// <param name="busStopName">Name of the bus stop.</param>
        /// <param name="nextBusStopName">Name of the next bus stop.</param>
        /// <param name="letter">The letter next to arrival time.</param>
        /// <param name="dayType">Type of the day.</param>
        public TemporaryTrackNode(TimeOfArrival hour, string busLineNumber, string busStopName,
            string nextBusStopName, string letter, string dayType)
        {
            BusLineNumber = busLineNumber;
            BusStopName = busStopName;
            Hour = hour;
            NextBusStopName = nextBusStopName;
            Letter = letter;
            DayType = dayType;
        }
        #endregion
    }
}
