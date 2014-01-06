using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearcherOfIndirectRoutes
    {
        #region Constructors

        public SearcherOfIndirectRoutes()
        {
        }

        #endregion

        #region Public methods

        public List<SearchResultConnection> FindIndirectConnection(Repository repository, SoughtConnection soughtConnection)
        {
            List<SearchResultConnection> resultList = new List<SearchResultConnection>();
            //return resultList;
            List<SingleBusStopForIndirectConnection> unprocessedBusStopsList = FindBusStopsWithLinesWhichAreCloseToTheTarget(repository, soughtConnection);

            if (unprocessedBusStopsList == null || unprocessedBusStopsList.Count < 2)
            {
                return null;
            }

            SingleBusStopForIndirectConnection lastBusStop = unprocessedBusStopsList.Last();
            unprocessedBusStopsList.RemoveAt(unprocessedBusStopsList.Count - 1);
            if (soughtConnection.IsDeparture)
            {
                if (!lastBusStop.BusStopName.Equals(soughtConnection.EndBusStop))
                {
                    return null;
                }
            }
            else
            {
                if (!lastBusStop.BusStopName.Equals(soughtConnection.StartBusStop))
                {
                    return null;
                }
            }

            string busStopNameForStopCondition = "";
            if (soughtConnection.IsDeparture)
            {
                busStopNameForStopCondition = soughtConnection.StartBusStop;
            }
            else
            {
                busStopNameForStopCondition = soughtConnection.EndBusStop;
            }
            bool stopCondition = false;
            TimeOfArrival arrivalTime = new TimeOfArrival(lastBusStop.ArrivalDateTimeAtThisBusStop.Hour, lastBusStop.ArrivalDateTimeAtThisBusStop.Minute);
            TimeOfArrival departureTime = new TimeOfArrival(lastBusStop.DepartureDateTimeFromPreviousBusStop.Hour, lastBusStop.DepartureDateTimeFromPreviousBusStop.Minute);
            TimeOfArrival timeDistanceBetweenBusStops = arrivalTime - departureTime;
            resultList.Add(new SearchResultConnection(false, lastBusStop.LineNumberWhichLeadToThisBusStop, lastBusStop.DepartureDateTimeFromPreviousBusStop, 
                lastBusStop.ArrivalDateTimeAtThisBusStop, timeDistanceBetweenBusStops, lastBusStop.PreviousBusStopName, lastBusStop.BusStopName));
            do
            {
                string previousBusStopName = lastBusStop.PreviousBusStopName;

                int indexOfUnprocessedBusStop = -1;
                for (int i = unprocessedBusStopsList.Count - 1; i >= 0; i--)
                {
                    if (unprocessedBusStopsList[i].BusStopName.Equals(previousBusStopName))
                    {
                        lastBusStop = unprocessedBusStopsList[i];
                        indexOfUnprocessedBusStop = i;
                        unprocessedBusStopsList.RemoveAt(indexOfUnprocessedBusStop);
                        break;
                    }
                }
                if (indexOfUnprocessedBusStop == -1)
                {
                    return null;
                }

                if (lastBusStop.LineNumberWhichLeadToThisBusStop.Equals(resultList.Last().LineNumber))
                {
                    resultList.Last().ChangeFields(lastBusStop.DepartureDateTimeFromPreviousBusStop, lastBusStop.PreviousBusStopName);
                }
                else
                {
                    arrivalTime = new TimeOfArrival(lastBusStop.ArrivalDateTimeAtThisBusStop.Hour, lastBusStop.ArrivalDateTimeAtThisBusStop.Minute);
                    departureTime = new TimeOfArrival(lastBusStop.DepartureDateTimeFromPreviousBusStop.Hour, lastBusStop.DepartureDateTimeFromPreviousBusStop.Minute);
                    timeDistanceBetweenBusStops = arrivalTime - departureTime;
                    resultList.Add(new SearchResultConnection(false, lastBusStop.LineNumberWhichLeadToThisBusStop, lastBusStop.DepartureDateTimeFromPreviousBusStop,
                        lastBusStop.ArrivalDateTimeAtThisBusStop, timeDistanceBetweenBusStops, lastBusStop.PreviousBusStopName, lastBusStop.BusStopName));
                }

                if (lastBusStop.BusStopName.Equals(busStopNameForStopCondition))
                {
                    stopCondition = true;
                }
            } while (!stopCondition);

            resultList.RemoveAt(resultList.Count - 1);
            resultList.Reverse();
            // -----------

            return resultList;
        }

        #endregion

        #region Private methods

        List<SingleBusStopForIndirectConnection> FindBusStopsWithLinesWhichAreCloseToTheTarget(Repository repository, SoughtConnection soughtConnection)
        {
            // initialize
            SearcherOfDirectRoutes searcherOfDirectConnections = new SearcherOfDirectRoutes();

            List<SingleBusStopForIndirectConnection> busStopsToCheckList = new List<SingleBusStopForIndirectConnection>();
            List<SingleBusStopForIndirectConnection> busStopsCheckedList = new List<SingleBusStopForIndirectConnection>();
            int numberOfAllBusStops = repository.BusStops.Count;
            List<BusStop> allBusStops = repository.BusStops;
            if (soughtConnection.IsDeparture)
            {
                for (int i = 0; i < numberOfAllBusStops; i++)
                {
                    if (allBusStops[i].BusStopName.Equals(soughtConnection.StartBusStop))
                    {
                        busStopsCheckedList.Add(new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", new TimeOfArrival(0, 0), soughtConnection.DateAndTime, soughtConnection.DateAndTime));
                    }
                    else
                    {
                        busStopsToCheckList.Add(new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", null, new DateTime(), new DateTime()));
                    }
                }
            }
            else
            {
                for (int i = 0; i < numberOfAllBusStops; i++)
                {
                    if (allBusStops[i].BusStopName.Equals(soughtConnection.EndBusStop))
                    {
                        busStopsCheckedList.Add(new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", new TimeOfArrival(0, 0), soughtConnection.DateAndTime, soughtConnection.DateAndTime));
                    }
                    else
                    {
                        busStopsToCheckList.Add(new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", null, new DateTime(), new DateTime()));
                    }
                }
            }
            SingleBusStopForIndirectConnection previousBusStop = busStopsCheckedList.Last();
            // end initialize

            // main fragments
            do
            {
                List<string> neighbourBusStopsList = new NeighbourBusStopsRecognizer().RecognizeNeighbourBusStops(previousBusStop.BusStopName,
                    repository, previousBusStop.ArrivalDateTimeAtThisBusStop);
                foreach (SingleBusStopForIndirectConnection oneBusStop in busStopsToCheckList)
                {
                    bool oneBusStopWasFound = false;
                    foreach (string neighbourBusStop in neighbourBusStopsList)
                    {
                        if (neighbourBusStop.Equals(oneBusStop.BusStopName))
                        {
                            oneBusStopWasFound = true;
                            break;
                        }
                    }
                    if (!oneBusStopWasFound)
                    {
                        continue;
                    }
                    SoughtConnection singleSoughtConnection = null;
                    if (soughtConnection.IsDeparture)
                    {
                        singleSoughtConnection = new SoughtConnection(previousBusStop.BusStopName, oneBusStop.BusStopName,
                            previousBusStop.ArrivalDateTimeAtThisBusStop, soughtConnection.IsDeparture);
                    }
                    else
                    {
                        singleSoughtConnection = new SoughtConnection(oneBusStop.BusStopName, previousBusStop.BusStopName,
                            previousBusStop.ArrivalDateTimeAtThisBusStop, soughtConnection.IsDeparture);
                    }

                    SearchResultConnection singleResult = searcherOfDirectConnections.FindDirectConnection(repository, singleSoughtConnection);

                    if (singleResult != null)
                    {
                        TimeOfArrival newArrivalTimeOnEndBusStop = new TimeOfArrival(singleResult.ArrivalDateTime.Hour, singleResult.ArrivalDateTime.Minute);
                        if (oneBusStop.TotalTimeFromStartBus == null)
                        {
                            // po prostu zapisz
                            oneBusStop.ChangeFields(previousBusStop.BusStopName, singleResult.LineNumber,
                                previousBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalDateTime, singleResult.DepartureDateTime);
                        }
                        else
                        {
                            // sprawdz czy nowy czss jest mniejszy i zapisz wtedy
                            if ((previousBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops) < oneBusStop.TotalTimeFromStartBus) // condition
                            {
                                oneBusStop.ChangeFields(previousBusStop.BusStopName, singleResult.LineNumber,
                                    previousBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalDateTime, singleResult.DepartureDateTime);
                            }
                        }
                    }
                }
                int indexOfTheNearestBusStop = -1;
                TimeOfArrival actualTotalTimeTravel = new TimeOfArrival(23, 59);
                for (int i = 0; i < busStopsToCheckList.Count; i++)
                {
                    if (busStopsToCheckList[i].TotalTimeFromStartBus == null)
                    {
                        continue;
                    }
                    if (busStopsToCheckList[i].TotalTimeFromStartBus < actualTotalTimeTravel)
                    {
                        indexOfTheNearestBusStop = i;
                        actualTotalTimeTravel = busStopsToCheckList[i].TotalTimeFromStartBus;
                    }
                }
                // probably route doesn't exist
                if (indexOfTheNearestBusStop == -1)
                {
                    break;
                }
                // one of the conditions - found end bus stop and route to this bus stop
                if (busStopsToCheckList[indexOfTheNearestBusStop].BusStopName.Equals(soughtConnection.EndBusStop))
                {
                    busStopsCheckedList.Add(busStopsToCheckList[indexOfTheNearestBusStop]);
                    busStopsToCheckList.RemoveAt(indexOfTheNearestBusStop);
                    break;
                }

                previousBusStop = busStopsToCheckList[indexOfTheNearestBusStop];
                busStopsToCheckList.RemoveAt(indexOfTheNearestBusStop);
                busStopsCheckedList.Add(previousBusStop);
            } while (busStopsToCheckList.Count > 0);
            // end main fragments

            return busStopsCheckedList;
        }

        #endregion
    }
}
