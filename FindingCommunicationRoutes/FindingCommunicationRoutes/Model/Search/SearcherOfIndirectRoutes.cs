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
            List<SingleBusStopForIndirectConnection> unprocessedBusStopsList = FindBusStopsWithLinesWhichAreCloseToTheTarget(repository, soughtConnection);
            return ProcessBusStopsCheckedList(soughtConnection, unprocessedBusStopsList);
        }

        #endregion

        #region Private methods

        private List<SingleBusStopForIndirectConnection> FindBusStopsWithLinesWhichAreCloseToTheTarget(Repository repository, SoughtConnection soughtConnection)
        {
            SearcherOfDirectRoutes searcherOfDirectConnections = new SearcherOfDirectRoutes();

            List<SingleBusStopForIndirectConnection> busStopsToCheckList = InitializeBusStopsToCheckList(soughtConnection, repository.BusStops);
            List<SingleBusStopForIndirectConnection> busStopsCheckedList = new List<SingleBusStopForIndirectConnection>();

            string busStopNameStopCondition = "";
            NeighbourBusStopsRecognizer.Direction direction;
            if (soughtConnection.IsDeparture)
            {
                busStopNameStopCondition = soughtConnection.EndBusStop;
                direction = NeighbourBusStopsRecognizer.Direction.Next;
            }
            else
            {
                busStopNameStopCondition = soughtConnection.StartBusStop;
                direction = NeighbourBusStopsRecognizer.Direction.Previous;
            }

            // lastBusStop is (only in the first step)
            // - start bus stop for departure
            // - end bus stop for arrival
            SingleBusStopForIndirectConnection lastBusStop = busStopsCheckedList.Last();     
            do
            {
                DateTime dateTomeForRecognizerOfNeighbourBusStops = new DateTime();
                if (soughtConnection.IsDeparture)
                {
                    dateTomeForRecognizerOfNeighbourBusStops = lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival;
                }
                else
                {
                    dateTomeForRecognizerOfNeighbourBusStops = lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival;
                }
                List<string> neighbourBusStopsList = new NeighbourBusStopsRecognizer().RecognizeNeighbourBusStopsInSpecifiedDirection(lastBusStop.BusStopName,
                    repository, dateTomeForRecognizerOfNeighbourBusStops, direction);
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
                        singleSoughtConnection = new SoughtConnection(lastBusStop.BusStopName, oneBusStop.BusStopName,
                            lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival, soughtConnection.IsDeparture);
                    }
                    else
                    {
                        singleSoughtConnection = new SoughtConnection(oneBusStop.BusStopName, lastBusStop.BusStopName,
                            lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival, soughtConnection.IsDeparture);
                    }

                    SearchResultConnection singleResult = searcherOfDirectConnections.FindDirectConnection(repository, singleSoughtConnection);

                    if (singleResult != null)
                    {
                        TimeOfArrival newArrivalTimeOnEndBusStop = new TimeOfArrival(singleResult.ArrivalDateTime.Hour, singleResult.ArrivalDateTime.Minute);
                        if (oneBusStop.TotalTimeFromStartBus == null)
                        {
                            // only save this informations
                            oneBusStop.ChangeFields(lastBusStop.BusStopName, singleResult.LineNumber,
                                lastBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalDateTime, singleResult.DepartureDateTime);
                        }
                        else
                        {
                            // check if new time is lower and then save this
                            if ((lastBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops) < oneBusStop.TotalTimeFromStartBus) // condition
                            {
                                oneBusStop.ChangeFields(lastBusStop.BusStopName, singleResult.LineNumber,
                                    lastBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalDateTime, singleResult.DepartureDateTime);
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

                lastBusStop = busStopsToCheckList[indexOfTheNearestBusStop];
                busStopsToCheckList.RemoveAt(indexOfTheNearestBusStop);
                busStopsCheckedList.Add(lastBusStop);

                // one of the conditions - found end bus stop and route to this bus stop
                if (lastBusStop.BusStopName.Equals(busStopNameStopCondition))
                {
                    break;
                }

            } while (busStopsToCheckList.Count > 0);

            return busStopsCheckedList;
        }

        private List<SearchResultConnection> ProcessBusStopsCheckedList(SoughtConnection soughtConnection, List<SingleBusStopForIndirectConnection> unprocessedBusStopsList)
        {
            List<SearchResultConnection> resultList = new List<SearchResultConnection>();
            if (unprocessedBusStopsList == null || unprocessedBusStopsList.Count < 2)
            {
                return null;
            }

            SingleBusStopForIndirectConnection lastBusStop = unprocessedBusStopsList.Last(); // for departure it's endBusStop, for arrival it's startBusStop
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
            string startBusStopNameInDirectConnection = "";
            string endBusStopNameInDirectConnection = "";
            if (soughtConnection.IsDeparture)
            {
                busStopNameForStopCondition = soughtConnection.StartBusStop;
                startBusStopNameInDirectConnection = lastBusStop.PreviousBusStopNameForDepartureOrNextBusStopForArrival;
                endBusStopNameInDirectConnection = lastBusStop.BusStopName;
            }
            else
            {
                busStopNameForStopCondition = soughtConnection.EndBusStop;
                startBusStopNameInDirectConnection = lastBusStop.BusStopName;
                endBusStopNameInDirectConnection = lastBusStop.PreviousBusStopNameForDepartureOrNextBusStopForArrival;
            }
            bool stopCondition = false;
            TimeOfArrival arrivalTime = new TimeOfArrival(lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival.Hour, lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival.Minute);
            TimeOfArrival departureTime = new TimeOfArrival(lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival.Hour, lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival.Minute);
            TimeOfArrival timeDistanceBetweenBusStops = arrivalTime - departureTime;
            resultList.Add(new SearchResultConnection(false, lastBusStop.LineNumberWhichLeadToThisBusStop, lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival,
                lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival, timeDistanceBetweenBusStops, startBusStopNameInDirectConnection, endBusStopNameInDirectConnection));
            do
            {
                string previousOrNextBusStopName = lastBusStop.PreviousBusStopNameForDepartureOrNextBusStopForArrival;

                int indexOfUnprocessedBusStop = -1;
                for (int i = unprocessedBusStopsList.Count - 1; i >= 0; i--)
                {
                    if (unprocessedBusStopsList[i].BusStopName.Equals(previousOrNextBusStopName))
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
                    if (soughtConnection.IsDeparture)
                    {
                        resultList.Last().ChangeFieldsForDepartureOption(lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival, lastBusStop.PreviousBusStopNameForDepartureOrNextBusStopForArrival);
                    }
                    else
                    {
                        resultList.Last().ChangeFieldsForArrivalOption(lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival, lastBusStop.PreviousBusStopNameForDepartureOrNextBusStopForArrival);
                    }
                }
                else
                {
                    if (soughtConnection.IsDeparture)
                    {
                        startBusStopNameInDirectConnection = lastBusStop.PreviousBusStopNameForDepartureOrNextBusStopForArrival;
                        endBusStopNameInDirectConnection = lastBusStop.BusStopName;
                    }
                    else
                    {
                        startBusStopNameInDirectConnection = lastBusStop.BusStopName;
                        endBusStopNameInDirectConnection = lastBusStop.PreviousBusStopNameForDepartureOrNextBusStopForArrival;
                    }

                    arrivalTime = new TimeOfArrival(lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival.Hour, lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival.Minute);
                    departureTime = new TimeOfArrival(lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival.Hour, lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival.Minute);
                    timeDistanceBetweenBusStops = arrivalTime - departureTime;
                    resultList.Add(new SearchResultConnection(false, lastBusStop.LineNumberWhichLeadToThisBusStop, lastBusStop.DepartureDateTimeFromPreviousBusStopForDepartureOrFromCurrentBusStopForArrival,
                        lastBusStop.ArrivalDateTimeAtThisBusStopForDepartureOrAtTheNextBusStopForArrival, timeDistanceBetweenBusStops, startBusStopNameInDirectConnection, endBusStopNameInDirectConnection));
                }

                if (lastBusStop.BusStopName.Equals(busStopNameForStopCondition))
                {
                    stopCondition = true;
                }
            } while (!stopCondition);

            resultList.RemoveAt(resultList.Count - 1);
            if (soughtConnection.IsDeparture)
            {
                resultList.Reverse();
            }

            return resultList;
        }

        private List<SingleBusStopForIndirectConnection> InitializeBusStopsToCheckList(SoughtConnection soughtConnection, List<BusStop> allBusStops)
        {
            int numberOfAllBusStops = allBusStops.Count;
            List<SingleBusStopForIndirectConnection> busStopsToCheckList = new List<SingleBusStopForIndirectConnection>();
            SingleBusStopForIndirectConnection startingBusStop = null;
            if (soughtConnection.IsDeparture)
            {
                for (int i = 0; i < numberOfAllBusStops; i++)
                {
                    if (allBusStops[i].BusStopName.Equals(soughtConnection.StartBusStop))
                    {
                        startingBusStop = new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", new TimeOfArrival(0, 0), soughtConnection.DateAndTime, soughtConnection.DateAndTime);
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
                        startingBusStop = new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", new TimeOfArrival(0, 0), soughtConnection.DateAndTime, soughtConnection.DateAndTime);
                    }
                    else
                    {
                        busStopsToCheckList.Add(new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", null, new DateTime(), new DateTime()));
                    }
                }
            }
            busStopsToCheckList.Add(startingBusStop);
            return busStopsToCheckList;
        }

        #endregion
    }
}
