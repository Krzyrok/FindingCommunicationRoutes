using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Represents the particular bus line (bus number).
    /// </summary>
    [Serializable]
    public class Line
    {
        #region properties

        /// <summary>
        /// Gets the number of bus line.
        /// </summary>
        /// <value>
        /// The number of bus line.
        /// </value>
        public string Number
        {
            get { return _busLineNumber; }
        }

        /// <summary>
        /// Gets the lists of tracks for every specified day type.
        /// </summary>
        /// <value>
        /// The lists of tracks.
        /// </value>
        public List<Track>[] DayTypeTracks
        {
            get { return _dayTypeTracks; }
        }

        
        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class from another instance.
        /// </summary>
        /// <param name="lineToCopy">The line to copy.</param>
        public Line(Line lineToCopy)
        {
            _busLineNumber = lineToCopy.Number;
            _dayTypeTracks = new List<Track>[lineToCopy.DayTypeTracks.Length];

            for (int i = 0; i < lineToCopy.DayTypeTracks.Length; ++i)
            {
                if (lineToCopy.DayTypeTracks[i] != null)
                {
                    _dayTypeTracks[i] = new List<Track>();
                    _dayTypeTracks[i].AddRange(lineToCopy.DayTypeTracks[i]);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="busLineNumber">The bus line number.</param>
        /// <param name="DayTypeTracksArg">Lists of tracks for every specified day type to store.</param>
        public Line(string busLineNumber,
            List<Track>[] DayTypeTracksArg)
        {

            _busLineNumber = busLineNumber;
            _dayTypeTracks = new List<Track>[DayTypeTracksArg.Length];

            for (int i = 0; i < DayTypeTracksArg.Length; ++i)
            {
                if (DayTypeTracksArg[i] != null)
                {
                    _dayTypeTracks[i] = new List<Track>();
                    _dayTypeTracks[i].AddRange(DayTypeTracksArg[i]);
                }
            }
           
        }
        #endregion

        #region private fields

        /// <summary>
        /// The bul line number.
        /// </summary>
        private string _busLineNumber;

        /// <summary>
        /// Lists of tracks for every specified day type stored in Line class.
        /// </summary>
        private List<Track>[] _dayTypeTracks;
        #endregion
    }
}
