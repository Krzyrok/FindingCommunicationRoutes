using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class TracksGiverForSpecifiedDayType
    {
        #region Public methods

        public List<LineForSpecifiedDayType> GiveLinesForSpecifiedDayType(List<Line> allLines, List<string> specifiedDayTypes)
        {
            List<LineForSpecifiedDayType> allTracksInSpecifiedDayType = new List<LineForSpecifiedDayType>();
            foreach (Line line in allLines)
            {
                for (int i = 0; i < specifiedDayTypes.Count; i++)
                {
                    for (int j = 0; j < line.DayTypeTracks.Length; j++)
                    {
                        if (line.DayTypeTracks[j] == null || line.DayTypeTracks[j].Count == 0)
                        {
                            break;
                        }
                        if (line.DayTypeTracks[j][0].DayType.Equals(specifiedDayTypes[i]))
                        {
                            allTracksInSpecifiedDayType.Add(new LineForSpecifiedDayType(line.Number, line.DayTypeTracks[j]));
                            i = specifiedDayTypes.Count;
                            break;
                        }
                    }
                }
            }

            return allTracksInSpecifiedDayType;
        }

        #endregion
    }
}
