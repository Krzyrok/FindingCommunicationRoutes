using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearcherOfRoutes
    {
        #region Constructors

        public SearcherOfRoutes()
        {
        }

        #endregion

        #region Public methods

        public SearchResultDirectConnection FindDirectConnection(Repository repository, SoughtConnection soughtConnection)
        {
            List<BusStop> busStops = repository.BusStops;
            if (busStops == null)
            {
                return null;
            }

            TypeOfDayRecognizer dayRecognizer = new TypeOfDayRecognizer();
            List<string> dayType = dayRecognizer.RecognizeTypeOfDay(soughtConnection.DateAndTime);

            BusStop startBusStop = null;
            BusStop endBusStop = null;

            foreach (BusStop busStop in busStops)
            {
                if (soughtConnection.StartBusStop.Equals(busStop.BusStopName))
                {
                    startBusStop = busStop;
                }
                else if (soughtConnection.EndBusStop.Equals(busStop.BusStopName))
                {
                    endBusStop = busStop;
                }
            }

            List<Line> linesPlyingThroughStartBusStop = startBusStop.LinesPlyingThroughBusStop;
            List<Line> linesPlyingThroughEndBusStop = endBusStop.LinesPlyingThroughBusStop;
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

            List<LineForSpecifiedDayType> allTracksFromStartToEndBusStopInSpecifiedDayType = new List<LineForSpecifiedDayType>();
            foreach (Line line in linesPlyingThroughBothBusStops)
            {
                for (int i = 0; i < dayType.Count; i++)
                {
                    for (int j = 0; j < line.DayTypeTracks.Length; j++)
                    {
                        if (line.DayTypeTracks[j].Count == 0)
                        {
                            break;
                        }
                        if (line.DayTypeTracks[j][0].DayType.Equals(dayType[i]))
                        {
                            allTracksFromStartToEndBusStopInSpecifiedDayType.Add(new LineForSpecifiedDayType(line.Number, line.DayTypeTracks[j]));
                            i = dayType.Count;
                            break;
                        }
                    }
                }
            }

            SearchResultDirectConnection result = null;
            foreach (LineForSpecifiedDayType line in allTracksFromStartToEndBusStopInSpecifiedDayType)
            {
                foreach(Track track in line.TracksForSpecifiedDayType)
                {
                    try
                    {
                        TimeOfArrival startBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[startBusStop.BusStopName];
                        TimeOfArrival endBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[endBusStop.BusStopName];
                        TimeOfArrival timeDistanceBetweenStartAndEndBusStop = endBusStopTimeOfArrival - startBusStopTimeOfArrival;
                        if (soughtConnection.IsDeparture)
                        {
                            if (startBusStopTimeOfArrival >= new TimeOfArrival(soughtConnection.DateAndTime.Hour, soughtConnection.DateAndTime.Minute))
                            {
                                if (result == null)
                                {
                                    
                                    result = new SearchResultDirectConnection(line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival, 
                                        timeDistanceBetweenStartAndEndBusStop, startBusStop.BusStopName, endBusStop.BusStopName);
                                }
                                else
                                {
                                    if (result.ArrivalTime > endBusStopTimeOfArrival)
                                    {
                                        result = new SearchResultDirectConnection(line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival,
                                            timeDistanceBetweenStartAndEndBusStop, startBusStop.BusStopName, endBusStop.BusStopName);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (endBusStopTimeOfArrival <= new TimeOfArrival(soughtConnection.DateAndTime.Hour, soughtConnection.DateAndTime.Minute))
                            {
                                if (result == null)
                                {
                                    result = new SearchResultDirectConnection(line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival,
                                        timeDistanceBetweenStartAndEndBusStop, startBusStop.BusStopName, endBusStop.BusStopName);
                                }
                                else
                                {
                                    if (result.DepartureTime < startBusStopTimeOfArrival)
                                    {
                                        result = new SearchResultDirectConnection(line.Number, startBusStopTimeOfArrival, endBusStopTimeOfArrival,
                                            timeDistanceBetweenStartAndEndBusStop, startBusStop.BusStopName, endBusStop.BusStopName);
                                    }
                                }
                            }
                        }
                    }
                    catch(Exception)
                    {

                    }
                }
            }

            return result;
        }

        #endregion
    }
}
