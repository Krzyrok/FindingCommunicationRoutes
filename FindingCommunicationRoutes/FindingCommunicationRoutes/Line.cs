﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Represents the particular bus line (bus number).
    /// </summary>
    public class Line
    {
        #region properties

        public string Number
        {
            get { return _number; }
        }

        public List<Track>[] DayTypeTracks
        {
            get { return _dayTypeTracks; }
        }

        
        #endregion

        #region constructors

        public Line(Line lineToCopy)
        {
            _number = lineToCopy.Number;
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

        public Line(string lineNumber,
            List<Track>[] DayTypeTracksArg)
        {

            _number = lineNumber;
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

        private string _number;

        private List<Track>[] _dayTypeTracks;
        //private List<Track> _saturdayTracks;
        //private List<Track> _holidaysTracks;
        //private List<Track> _newYearEveTracks;
        //private List<Track> _soulDayTracks;
        //private List<Track> _changeSummerTimeTracks;
        //private List<Track> _christmasEveTracks;
        #endregion
    }
}
