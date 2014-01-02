using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class LineForSpecifiedDayType
    {
        #region Public fields

        public string Number { get; private set; }
        public List<Track> TracksForSpecifiedDayType { get; private set; }

        #endregion

        #region Constructors

        public LineForSpecifiedDayType(string lineNumber, List<Track> tracks)
        {
            Number = lineNumber;
            TracksForSpecifiedDayType = tracks;
        }

        #endregion
    }
}
