using System;
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
        public int Number
        {
            get { return _number; }
        }

        public List<Track> WorkingDaysTracks
        {
            get { return _workingDaysTracks; }
        }

        public List<Track> SaturdayTracks
        {
            get { return _saturdayTracks; }
        }

        public List<Track> HolidaysTracks
        {
            get { return _holidaysTracks; }
        }

        public List<Track> NewYearEveTracks
        {
            get { return _newYearEveTracks; }
        }

        public Line(int lineNumber,
            List<Track> WorkingDaysTracks,
            List<Track> SaturdayTracks,
            List<Track> HolidaysTracks,
            List<Track> NewYearEveTracks)
        {

            _number = lineNumber;

            if (WorkingDaysTracks != null)
            {
                _workingDaysTracks = new List<Track>();
                _workingDaysTracks.AddRange(WorkingDaysTracks);
            }
            if (SaturdayTracks != null)
            {
                _saturdayTracks = new List<Track>();
                _saturdayTracks.AddRange(SaturdayTracks);
            }
            if (HolidaysTracks != null)
            {
                _holidaysTracks = new List<Track>();
                _holidaysTracks.AddRange(HolidaysTracks);
            }
            if (NewYearEveTracks != null)
            {
                _newYearEveTracks = new List<Track>();
                _newYearEveTracks.AddRange(NewYearEveTracks);
            }
        }

        private int _number;

        private List<Track> _workingDaysTracks;
        private List<Track> _saturdayTracks;
        private List<Track> _holidaysTracks;
        private List<Track> _newYearEveTracks;
    }
}
