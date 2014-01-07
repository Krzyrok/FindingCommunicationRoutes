using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class NeighbourBusStopsRecognizer
    {
        #region Enum

        public enum Direction { Previous = -1, Next = 1 };

        #endregion

        #region Constructors

        public NeighbourBusStopsRecognizer()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Recognizes the neighbour bus stops in specified direction.
        /// </summary>
        /// <param name="busStopName">Name of the bus stop. For this bus stop function will search neighbours.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="date">The date for searching (in some days one bus stop is next to another, in another - no).</param>
        /// <param name="direction">The direction. 
        /// For 'Next' value, function will search only bus stops which are next to the 'busStopName' in the tracks.
        /// For 'Previous' value, function will search only bus stops which are before the 'busStopName' in the tracks.</param>
        /// <returns>List of the names of neighbour bus stops</returns>
        public List<string> RecognizeNeighbourBusStopsInSpecifiedDirection(string busStopName, Repository repository, DateTime date, Direction direction)
        {
            BusStop busStop = FindBusStop(busStopName, repository.BusStops);
            List<string> dayTypes = new TypeOfDayRecognizer().RecognizeTypeOfDay(date);
            List<LineForSpecifiedDayType> specifiedTracksForLines = new TracksGiverForSpecifiedDayType().GiveLinesForSpecifiedDayType(busStop.LinesPlyingThroughBusStop, dayTypes);
            if (specifiedTracksForLines == null)
            {
                return null;
            }

            List<string> result = new List<string>();
            foreach (LineForSpecifiedDayType specifiedTracksInLine in specifiedTracksForLines)
            {
                // simplified - check only first Track
                for (int j = 0; j < 4 && j < specifiedTracksInLine.TracksForSpecifiedDayType.Count; j++)
                {
                    Track firstTrack = specifiedTracksInLine.TracksForSpecifiedDayType[j];
                    int index = -1;
                    bool wasFoundBusStop = false;
                    for (int i = 0; i < firstTrack.TimeOfArrivalOnBusStops.Count; i++)
                    {
                        if (firstTrack.TimeOfArrivalOnBusStops.ElementAt(i).Key == busStopName)
                        {
                            index = i + (int)direction;
                            wasFoundBusStop = true;
                            break;
                        }
                    }
                    if (wasFoundBusStop)
                    {
                        if (index > -1 && index < firstTrack.TimeOfArrivalOnBusStops.Count)
                        {
                            result.Add(firstTrack.TimeOfArrivalOnBusStops.ElementAt(index).Key);
                        }
                        break;
                    }
                }


                // in another way

                for (int j = specifiedTracksInLine.TracksForSpecifiedDayType.Count - 1; j > specifiedTracksInLine.TracksForSpecifiedDayType.Count - 5 && j > 3; j--)
                {
                    Track lastTrackTrack = specifiedTracksInLine.TracksForSpecifiedDayType[j];
                    int index = -1;
                    bool wasFoundBusStop = false;
                    for (int i = 0; i < lastTrackTrack.TimeOfArrivalOnBusStops.Count; i++)
                    {
                        if (lastTrackTrack.TimeOfArrivalOnBusStops.ElementAt(i).Key == busStopName)
                        {
                            index = i + (int)direction;
                            wasFoundBusStop = true;
                            break;
                        }
                    }
                    if (wasFoundBusStop)
                    {
                        if (index > -1 && index < lastTrackTrack.TimeOfArrivalOnBusStops.Count)
                        {
                            if (result.Count == 0 || !(result.Last().Equals(lastTrackTrack.TimeOfArrivalOnBusStops.ElementAt(index).Key)))
                            {
                                result.Add(lastTrackTrack.TimeOfArrivalOnBusStops.ElementAt(index).Key);
                            }
                        }
                        break;
                    }
                }

            }

            return result;
        }

        #endregion

        #region Private methods

        private BusStop FindBusStop(string busStopName, List<BusStop> busStops)
        {
            foreach (BusStop busStop in busStops)
            {
                if (busStopName.Equals(busStop.BusStopName))
                {
                    return busStop;
                }
            }

            return null;
        }

        #endregion
    }
}
