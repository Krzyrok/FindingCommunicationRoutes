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
            MAX_WAITING_TIME_FOR_START_BUS_FOR_DEPARTUE_OR_FOR_END_BUS_FOR_ARRIVAL = new TimeOfArrival(2, 0);
            MAX_LENGTH_OF_TRACK = new TimeOfArrival(2, 0);
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
            TracksGiverForSpecifiedDayType tracksGiverForSpecfiedDayType = new TracksGiverForSpecifiedDayType();

            BusStop startBusStop = null;
            BusStop endBusStop = null;
            FindStartAndEndBusStop(ref startBusStop, ref endBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop, busStops);

            List<Line> linesPlyingThroughBothBusStops = GiveLinesPlyingThroughTwoBusStops(startBusStop, endBusStop);

            List<LineForSpecifiedDayType> allTracksFromStartToEndBusStopInSpecifiedDayType = 
                tracksGiverForSpecfiedDayType.GiveLinesForSpecifiedDayType(linesPlyingThroughBothBusStops, dayTypes);

            SearchResultConnection result = GiveDirectConnection(allTracksFromStartToEndBusStopInSpecifiedDayType, soughtConnection);
            if (result != null)
            {
                return result;
            }

            DateTime newDateForSoughtConnection = new DateTime();
            if (soughtConnection.IsDeparture)
            {
                DateTime dayAfterDaySpecifiedByUser = soughtConnection.DateAndTime.AddDays(1);
                newDateForSoughtConnection = new DateTime(dayAfterDaySpecifiedByUser.Year, dayAfterDaySpecifiedByUser.Month, dayAfterDaySpecifiedByUser.Day, 0, 0, 0);
            }
            else
            {
                DateTime dayBeforeDaySpecifiedByUser = soughtConnection.DateAndTime.AddDays(-1);
                newDateForSoughtConnection = new DateTime(dayBeforeDaySpecifiedByUser.Year, dayBeforeDaySpecifiedByUser.Month, dayBeforeDaySpecifiedByUser.Day, 23, 59, 0);
            }

            soughtConnection = new SoughtConnection(startBusStop.BusStopName, endBusStop.BusStopName, newDateForSoughtConnection, soughtConnection.IsDeparture);
            dayTypes = dayRecognizer.RecognizeTypeOfDay(newDateForSoughtConnection);
            allTracksFromStartToEndBusStopInSpecifiedDayType =
                tracksGiverForSpecfiedDayType.GiveLinesForSpecifiedDayType(linesPlyingThroughBothBusStops, dayTypes);
            result = GiveDirectConnection(allTracksFromStartToEndBusStopInSpecifiedDayType, soughtConnection);
            return result;
        }

        public SearchResultConnection FindDirectConnectionWitMaxWaitingTime(Repository repository, SoughtConnection soughtConnection, TimeOfArrival maxWaitingTime)
        {
            List<BusStop> busStops = repository.BusStops;
            if (busStops == null)
            {
                return null;
            }

            TypeOfDayRecognizer dayRecognizer = new TypeOfDayRecognizer();
            List<string> dayTypes = dayRecognizer.RecognizeTypeOfDay(soughtConnection.DateAndTime);
            TracksGiverForSpecifiedDayType tracksGiverForSpecfiedDayType = new TracksGiverForSpecifiedDayType();

            BusStop startBusStop = null;
            BusStop endBusStop = null;
            FindStartAndEndBusStop(ref startBusStop, ref endBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop, busStops);

            List<Line> linesPlyingThroughBothBusStops = GiveLinesPlyingThroughTwoBusStops(startBusStop, endBusStop);

            List<LineForSpecifiedDayType> allTracksFromStartToEndBusStopInSpecifiedDayType =
                tracksGiverForSpecfiedDayType.GiveLinesForSpecifiedDayType(linesPlyingThroughBothBusStops, dayTypes);

            SearchResultConnection result = GiveDirectConnectionWitMaxWaitingTime(allTracksFromStartToEndBusStopInSpecifiedDayType, 
                soughtConnection, maxWaitingTime);
            if (result != null)
            {
                return result;
            }

            DateTime newDateForSoughtConnection = new DateTime();
            if (soughtConnection.IsDeparture)
            {
                DateTime dayAfterDaySpecifiedByUser = soughtConnection.DateAndTime.AddDays(1);
                newDateForSoughtConnection = new DateTime(dayAfterDaySpecifiedByUser.Year, dayAfterDaySpecifiedByUser.Month, dayAfterDaySpecifiedByUser.Day, 0, 0, 0);
            }
            else
            {
                DateTime dayBeforeDaySpecifiedByUser = soughtConnection.DateAndTime.AddDays(-1);
                newDateForSoughtConnection = new DateTime(dayBeforeDaySpecifiedByUser.Year, dayBeforeDaySpecifiedByUser.Month, dayBeforeDaySpecifiedByUser.Day, 23, 59, 0);
            }

            soughtConnection = new SoughtConnection(startBusStop.BusStopName, endBusStop.BusStopName, newDateForSoughtConnection, soughtConnection.IsDeparture);
            dayTypes = dayRecognizer.RecognizeTypeOfDay(newDateForSoughtConnection);
            allTracksFromStartToEndBusStopInSpecifiedDayType =
                tracksGiverForSpecfiedDayType.GiveLinesForSpecifiedDayType(linesPlyingThroughBothBusStops, dayTypes);
            result = GiveDirectConnectionWitMaxWaitingTime(allTracksFromStartToEndBusStopInSpecifiedDayType,
                soughtConnection, maxWaitingTime);
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

        private SearchResultConnection GiveDirectConnection(List<LineForSpecifiedDayType> allSpecifiedLines, SoughtConnection soughtConnection)
        {
            SearchResultConnection foundConnection = null;
            foreach (LineForSpecifiedDayType line in allSpecifiedLines)
            {
                foreach (Track track in line.TracksForSpecifiedDayType)
                {
                    try
                    {
                        TimeOfArrival startBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[soughtConnection.StartBusStop];
                        TimeOfArrival endBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[soughtConnection.EndBusStop];
                        TimeOfArrival timeDistanceBetweenStartAndEndBusStop = endBusStopTimeOfArrival - startBusStopTimeOfArrival;
                        if (timeDistanceBetweenStartAndEndBusStop > new TimeOfArrival(8, 0))
                        {
                            continue;
                        }
                        
                        DateTime startBusStopDateTime = new DateTime();
                        DateTime endBusStopDateTime = new DateTime();
                        if (endBusStopTimeOfArrival >= startBusStopTimeOfArrival)
                        {
                            // the same dates                 
                            startBusStopDateTime = new DateTime(soughtConnection.DateAndTime.Year, soughtConnection.DateAndTime.Month, soughtConnection.DateAndTime.Day,
                                startBusStopTimeOfArrival.Hour, startBusStopTimeOfArrival.Minutes, 0);
                            endBusStopDateTime = new DateTime(soughtConnection.DateAndTime.Year, soughtConnection.DateAndTime.Month, soughtConnection.DateAndTime.Day,
                                endBusStopTimeOfArrival.Hour, endBusStopTimeOfArrival.Minutes, 0);
                        }
                        else
                        {
                            // end date is one day after start date
                            startBusStopDateTime = new DateTime(soughtConnection.DateAndTime.Year, soughtConnection.DateAndTime.Month, soughtConnection.DateAndTime.Day,
                                startBusStopTimeOfArrival.Hour, startBusStopTimeOfArrival.Minutes, 0);
                            DateTime dayAfterDayInSoughtConnection = soughtConnection.DateAndTime.AddDays(1);
                            endBusStopDateTime = new DateTime(dayAfterDayInSoughtConnection.Year, dayAfterDayInSoughtConnection.Month, dayAfterDayInSoughtConnection.Day,
                                endBusStopTimeOfArrival.Hour, endBusStopTimeOfArrival.Minutes, 0);
                        }

                        TimeOfArrival timeSpecifiedByUser = new TimeOfArrival(soughtConnection.DateAndTime.Hour, soughtConnection.DateAndTime.Minute);
                        if (soughtConnection.IsDeparture)
                        {
                            if (startBusStopTimeOfArrival >= timeSpecifiedByUser)
                            {
                                if (foundConnection == null)
                                {

                                    foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                        endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
                                }
                                else
                                {
                                    TimeOfArrival foundConnectionArrivalTime = new TimeOfArrival(foundConnection.ArrivalDateTime.Hour, foundConnection.ArrivalDateTime.Minute);
                                    if (foundConnectionArrivalTime > endBusStopTimeOfArrival)
                                    {
                                        foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                            endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (endBusStopTimeOfArrival <= timeSpecifiedByUser)
                            {
                                if (foundConnection == null)
                                {
                                    foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                        endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
                                }
                                else
                                {
                                    TimeOfArrival foundConnectionArrivalTime = new TimeOfArrival(foundConnection.ArrivalDateTime.Hour, foundConnection.ArrivalDateTime.Minute);
                                    if (foundConnectionArrivalTime < startBusStopTimeOfArrival)
                                    {
                                        foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                            endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
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

            return foundConnection;
        }

        private SearchResultConnection GiveDirectConnectionWitMaxWaitingTime(List<LineForSpecifiedDayType> allSpecifiedLines, SoughtConnection soughtConnection, TimeOfArrival maxWaitingTime)
        {
            SearchResultConnection foundConnection = null;
            TimeOfArrival soughConnectionTimeOfArrival = new TimeOfArrival(soughtConnection.DateAndTime.Hour, soughtConnection.DateAndTime.Minute);
            foreach (LineForSpecifiedDayType line in allSpecifiedLines)
            {
                foreach (Track track in line.TracksForSpecifiedDayType)
                {
                    try
                    {
                        if (track.TimeOfArrivalOnBusStops.Count < 2) 
                        {
                            continue;
                        }
                        if (soughtConnection.IsDeparture)
                        {
                            if (track.TimeOfArrivalOnBusStops.Last().Value - soughConnectionTimeOfArrival > MAX_LENGTH_OF_TRACK
                                || (track.TimeOfArrivalOnBusStops.First().Value - soughConnectionTimeOfArrival > MAX_WAITING_TIME_FOR_START_BUS_FOR_DEPARTUE_OR_FOR_END_BUS_FOR_ARRIVAL 
                                    && soughConnectionTimeOfArrival - track.TimeOfArrivalOnBusStops.First().Value > MAX_LENGTH_OF_TRACK))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (soughConnectionTimeOfArrival - track.TimeOfArrivalOnBusStops.First().Value > MAX_LENGTH_OF_TRACK
                                || (soughConnectionTimeOfArrival - track.TimeOfArrivalOnBusStops.Last().Value > MAX_WAITING_TIME_FOR_START_BUS_FOR_DEPARTUE_OR_FOR_END_BUS_FOR_ARRIVAL 
                                    && track.TimeOfArrivalOnBusStops.Last().Value - soughConnectionTimeOfArrival > MAX_LENGTH_OF_TRACK))

                            {
                                continue;
                            }
                        }

                        TimeOfArrival startBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[soughtConnection.StartBusStop];
                        TimeOfArrival endBusStopTimeOfArrival = track.TimeOfArrivalOnBusStops[soughtConnection.EndBusStop];

                        if (soughtConnection.IsDeparture)
                        {
                            if (startBusStopTimeOfArrival - soughConnectionTimeOfArrival > maxWaitingTime)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (soughConnectionTimeOfArrival - endBusStopTimeOfArrival > maxWaitingTime)
                            {
                                continue;
                            }
                        }

                        TimeOfArrival timeDistanceBetweenStartAndEndBusStop = endBusStopTimeOfArrival - startBusStopTimeOfArrival;
                        if (timeDistanceBetweenStartAndEndBusStop > new TimeOfArrival(4, 0))
                        {
                            continue;
                        }

                        DateTime startBusStopDateTime = new DateTime();
                        DateTime endBusStopDateTime = new DateTime();
                        if (endBusStopTimeOfArrival >= startBusStopTimeOfArrival)
                        {
                            // the same dates                 
                            startBusStopDateTime = new DateTime(soughtConnection.DateAndTime.Year, soughtConnection.DateAndTime.Month, soughtConnection.DateAndTime.Day,
                                startBusStopTimeOfArrival.Hour, startBusStopTimeOfArrival.Minutes, 0);
                            endBusStopDateTime = new DateTime(soughtConnection.DateAndTime.Year, soughtConnection.DateAndTime.Month, soughtConnection.DateAndTime.Day,
                                endBusStopTimeOfArrival.Hour, endBusStopTimeOfArrival.Minutes, 0);
                        }
                        else
                        {
                            // end date is one day after start date
                            startBusStopDateTime = new DateTime(soughtConnection.DateAndTime.Year, soughtConnection.DateAndTime.Month, soughtConnection.DateAndTime.Day,
                                startBusStopTimeOfArrival.Hour, startBusStopTimeOfArrival.Minutes, 0);
                            DateTime dayAfterDayInSoughtConnection = soughtConnection.DateAndTime.AddDays(1);
                            endBusStopDateTime = new DateTime(dayAfterDayInSoughtConnection.Year, dayAfterDayInSoughtConnection.Month, dayAfterDayInSoughtConnection.Day,
                                endBusStopTimeOfArrival.Hour, endBusStopTimeOfArrival.Minutes, 0);
                        }

                        TimeOfArrival timeSpecifiedByUser = new TimeOfArrival(soughtConnection.DateAndTime.Hour, soughtConnection.DateAndTime.Minute);
                        if (soughtConnection.IsDeparture)
                        {
                            if (startBusStopTimeOfArrival >= timeSpecifiedByUser)
                            {
                                if (foundConnection == null)
                                {

                                    foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                        endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
                                }
                                else
                                {
                                    TimeOfArrival foundConnectionArrivalTime = new TimeOfArrival(foundConnection.ArrivalDateTime.Hour, foundConnection.ArrivalDateTime.Minute);
                                    if (foundConnectionArrivalTime > endBusStopTimeOfArrival)
                                    {
                                        foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                            endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (endBusStopTimeOfArrival <= timeSpecifiedByUser)
                            {
                                if (foundConnection == null)
                                {
                                    foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                        endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
                                }
                                else
                                {
                                    TimeOfArrival foundConnectionArrivalTime = new TimeOfArrival(foundConnection.ArrivalDateTime.Hour, foundConnection.ArrivalDateTime.Minute);
                                    if (foundConnectionArrivalTime < startBusStopTimeOfArrival)
                                    {
                                        foundConnection = new SearchResultConnection(true, line.Number, startBusStopDateTime,
                                            endBusStopDateTime, timeDistanceBetweenStartAndEndBusStop, soughtConnection.StartBusStop, soughtConnection.EndBusStop);
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

            return foundConnection;
        }

        #endregion

        #region Private fields

        private TimeOfArrival MAX_WAITING_TIME_FOR_START_BUS_FOR_DEPARTUE_OR_FOR_END_BUS_FOR_ARRIVAL;
        private TimeOfArrival MAX_LENGTH_OF_TRACK;

        #endregion
    }
}
