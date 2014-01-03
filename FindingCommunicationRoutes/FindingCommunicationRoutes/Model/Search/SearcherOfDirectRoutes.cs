using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearcherOfDirectRoutes
    {
        #region Constructors

        public SearcherOfDirectRoutes()
        {
        }

        #endregion

        #region Public methods

        public SearchResultConnection FindDirectConnection(Repository repository, SoughtConnection soughtConnection)
        {
            List<BusStop> busStops = repository.BusStops;
            if (busStops == null)
            {
                return null;
            }

            TypeOfDayRecognizer dayRecognizer = new TypeOfDayRecognizer();
            List<string> dayTypes = dayRecognizer.RecognizeTypeOfDay(soughtConnection.DateAndTime);

            BusStop startBusStop = null;
            BusStop endBusStop = null;
            FindStartAndEndBusStop(ref startBusStop, ref endBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop, busStops);

            List<Line> linesPlyingThroughBothBusStops = GiveLinesPlyingThroughTwoBusStops(startBusStop, endBusStop);

            List<LineForSpecifiedDayType> allTracksFromStartToEndBusStopInSpecifiedDayType = GiveLinesForSpecifiedDayType(linesPlyingThroughBothBusStops, dayTypes);

            SearchResultConnection result = GiveDirectConnection(allTracksFromStartToEndBusStopInSpecifiedDayType, 
                startBusStop.BusStopName, endBusStop.BusStopName, soughtConnection);

            return result;
        }

        #endregion

        #region Private methods

        private void FindStartAndEndBusStop(ref BusStop startBusStop, ref BusStop endBusStop, 
            string startBusStopName, string endBusStopName, List<BusStop> busStops)
        {
            foreach (BusStop busStop in busStops)
            {
                if (startBusStopName.Equals(busStop.BusStopName))
                {
                    startBusStop = busStop;
                }
                else if (endBusStopName.Equals(busStop.BusStopName))
                {
                    endBusStop = busStop;
                }
            }
        }

        private List<Line> GiveLinesPlyingThroughTwoBusStops(BusStop firstBusStop, BusStop secondBusStop)
        {
            List<Line> linesPlyingThroughStartBusStop = firstBusStop.LinesPlyingThroughBusStop;
            List<Line> linesPlyingThroughEndBusStop = secondBusStop.LinesPlyingThroughBusStop;
            List<Line> linesPlyingThroughBothBusStops = new List<Line>();
            foreach (Line lineFromStartBusStop in linesPlyingThroughStartBusStop)
            {
                foreach (Line lineFromEndBusStop in linesPlyingThroughEndBusStop)
                {
                    if (lineFromStartBusStop.Number.Equals(lineFromEndBusStop.Number))
                    {
                        linesPlyingThroughBothBusStops.Add(lineFromStartBusStop);
                        break;
                    }
                }
            }

            return linesPlyingThroughBothBusStops;
        }

        private List<LineForSpecifiedDayType> GiveLinesForSpecifiedDayType(List<Line> allLines, List<string> specifiedDayTypes)
        {
            List<LineForSpecifiedDayType> allTracksInSpecifiedDayType = new List<LineForSpecifiedDayType>();
            foreach (Line line in allLines)
            {
                for (int i = 0; i < specifiedDayTypes.Count; i++)
                {
                    for (int j = 0; j < line.DayTypeTracks.Length; j++)
                    {
                        if (line.DayTypeTracks[j].Count == 0)
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

        private SearchResultConnection GiveDirectConnection(List<LineForSpecifiedDayType> allSpecifiedLines, string startBusStopName, string endBusStopName, SoughtConnection soughtConnection)
        {
            SearchResultConnection searchConnection = null;
            foreach (LineForSpecifiedDayType line in allSpecifiedLines)
            {
                foreach (Track track in line.TracksForSpecifiedDayType)
                {
                    try
                    {
                        TimeOfArrival startBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[startBusStopName];
                        TimeOfArrival endBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[endBusStopName];
                        TimeOfArrival timeDistanceBetweenStartAndEndBusStop = endBusStopTimeOfArrival - startBusStopTimeOfArrival;
                        if (soughtConnection.IsDeparture)
                        {
                            if (startBusStopTimeOfArrival >= new TimeOfArrival(soughtConnection.DateAndTime.Hour, soughtConnection.DateAndTime.Minute))
                            {
                                if (searchConnection == null)
                                {

                                    searchConnection = new SearchResultConnection(true, line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival,
                                        timeDistanceBetweenStartAndEndBusStop, startBusStopName, endBusStopName);
                                }
                                else
                                {
                                    if (searchConnection.ArrivalTime > endBusStopTimeOfArrival)
                                    {
                                        searchConnection = new SearchResultConnection(true, line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival,
                                            timeDistanceBetweenStartAndEndBusStop, startBusStopName, endBusStopName);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (endBusStopTimeOfArrival <= new TimeOfArrival(soughtConnection.DateAndTime.Hour, soughtConnection.DateAndTime.Minute))
                            {
                                if (searchConnection == null)
                                {
                                    searchConnection = new SearchResultConnection(true, line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival,
                                        timeDistanceBetweenStartAndEndBusStop, startBusStopName, endBusStopName);
                                }
                                else
                                {
                                    if (searchConnection.DepartureTime < startBusStopTimeOfArrival)
                                    {
                                        searchConnection = new SearchResultConnection(true, line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival,
                                            timeDistanceBetweenStartAndEndBusStop, startBusStopName, endBusStopName);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            return searchConnection;
        }

        #endregion
    }
}
