using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class NeighbourBusStopsRecognizer
    {
        #region Constructors

        public NeighbourBusStopsRecognizer()
        {
        }

        #endregion

        #region Public methods

        public List<string> RecognizeNeighbourBusStops(string busStopName, Repository repository, DateTime date)
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
                            index = i + 1;
                            wasFoundBusStop = true;
                            break;
                        }
                    }
                    if (wasFoundBusStop)
                    {
                        if (index != -1 && index < firstTrack.TimeOfArrivalOnBusStops.Count)
                        {
                            result.Add(firstTrack.TimeOfArrivalOnBusStops.ElementAt(index).Key);
                        }
                        break;
                    }
                }


                // in another way

                for (int j = specifiedTracksInLine.TracksForSpecifiedDayType.Count - 1; j > specifiedTracksInLine.TracksForSpecifiedDayType.Count - 5 && j > 0; j--)
                {
                    Track lastTrackTrack = specifiedTracksInLine.TracksForSpecifiedDayType[j];
                    int index = -1;
                    bool wasFoundBusStop = false;
                    for (int i = 0; i < lastTrackTrack.TimeOfArrivalOnBusStops.Count; i++)
                    {
                        if (lastTrackTrack.TimeOfArrivalOnBusStops.ElementAt(i).Key == busStopName)
                        {
                            index = i + 1;
                            wasFoundBusStop = true;
                            break;
                        }
                    }
                    if (wasFoundBusStop)
                    {
                        if (index != -1 && index < lastTrackTrack.TimeOfArrivalOnBusStops.Count)
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
