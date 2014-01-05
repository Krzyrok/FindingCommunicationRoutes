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

            // ------------

            
            // initialize
            SearcherOfDirectRoutes searcherOfDirectConnections = new SearcherOfDirectRoutes();

            List<SingleBusStopForIndirectConnection> busStopsToCheckList = new List<SingleBusStopForIndirectConnection>();
            List<SingleBusStopForIndirectConnection> busStopsToCheckTmpList = new List<SingleBusStopForIndirectConnection>();
            List<SingleBusStopForIndirectConnection> busStopsCheckedList = new List<SingleBusStopForIndirectConnection>();
            int numberOfAllBusStops = repository.BusStops.Count;
            List<BusStop> allBusStops = repository.BusStops;
            for (int i = 0; i < numberOfAllBusStops; i++)
            {
                if (allBusStops[i].BusStopName.Equals(soughtConnection.StartBusStop))
                {
                    busStopsCheckedList.Add(new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", new TimeOfArrival(0, 0), new DateTime(), new DateTime()));
                }
                else
                {
                    busStopsToCheckList.Add(new SingleBusStopForIndirectConnection(allBusStops[i].BusStopName, "", "", null, new DateTime(), new DateTime()));
                }
            }
            SingleBusStopForIndirectConnection startBusStop = busStopsCheckedList.Last();

            busStopsToCheckTmpList.AddRange(busStopsToCheckList);
            busStopsToCheckList.Clear();
            foreach (SingleBusStopForIndirectConnection oneBusStop in busStopsToCheckTmpList)
            {
                SoughtConnection tmpSoughtConnection = new SoughtConnection(startBusStop.BusStopName, oneBusStop.BusStopName, 
                    soughtConnection.DateAndTime, soughtConnection.IsDeparture);
                SearchResultConnection singleResult = searcherOfDirectConnections.FindDirectConnection(repository, tmpSoughtConnection);

                if (singleResult == null)
                {
                    busStopsToCheckList.Add(new SingleBusStopForIndirectConnection(oneBusStop.BusStopName, "", "", null, 
                        new DateTime(), new DateTime()));
                }
                else
                {
                    busStopsToCheckList.Add(new SingleBusStopForIndirectConnection(oneBusStop.BusStopName, startBusStop.BusStopName, singleResult.LineNumber,
                        singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalDateTime, singleResult.DepartureDateTime));
                }
            }

            busStopsToCheckTmpList.Clear();
            // end initialize

            // main fragments
            do
            {
                busStopsToCheckTmpList.AddRange(busStopsToCheckList);
                busStopsToCheckList.Clear();

                int indexOfTheNearestBusStop = -1;
                TimeOfArrival actualTotalTimeTravel = new TimeOfArrival(23, 59);
                for (int i = 0; i < busStopsToCheckTmpList.Count; i++)
                {
                    if (busStopsToCheckTmpList[i].TotalTimeFromStartBus == null)
                    {
                        continue;
                    }
                    if (busStopsToCheckTmpList[i].TotalTimeFromStartBus < actualTotalTimeTravel)
                    {
                        indexOfTheNearestBusStop = i;
                        actualTotalTimeTravel = busStopsToCheckTmpList[i].TotalTimeFromStartBus;
                    }
                }
                // probably route doesn;t exist
                if (indexOfTheNearestBusStop == -1)
                {
                    break;
                }
                // one of the conditions
                if (busStopsToCheckTmpList[indexOfTheNearestBusStop].BusStopName.Equals(soughtConnection.EndBusStop))
                {
                    busStopsCheckedList.Add(busStopsToCheckTmpList[indexOfTheNearestBusStop]);
                    busStopsToCheckTmpList.RemoveAt(indexOfTheNearestBusStop);
                    busStopsToCheckList.AddRange(busStopsToCheckTmpList);
                    break;
                }

                startBusStop = busStopsToCheckTmpList[indexOfTheNearestBusStop];
                busStopsToCheckTmpList.RemoveAt(indexOfTheNearestBusStop);
                foreach (SingleBusStopForIndirectConnection oneBusStop in busStopsToCheckTmpList)
                {
                    SoughtConnection tmpSoughtConnection = new SoughtConnection(startBusStop.BusStopName, oneBusStop.BusStopName,
                        startBusStop.ArrivalTimeAtThisBusStop, true);
                    SearchResultConnection singleResult = searcherOfDirectConnections.FindDirectConnection(repository, tmpSoughtConnection);

                    if (singleResult != null)
                    {
                        TimeOfArrival newArrivalTimeOnEndBusStop = new TimeOfArrival(singleResult.ArrivalDateTime.Hour, singleResult.ArrivalDateTime.Minute);
                        if (oneBusStop.TotalTimeFromStartBus == null)
                        {
                            // po prostu zapisz
                            oneBusStop.ChangeFields(startBusStop.BusStopName, singleResult.LineNumber, 
                                startBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalDateTime, singleResult.DepartureDateTime);
                        }
                        else
                        {
                            // sprawdz czy nowy czss jest mniejszy i zapisz wtedy
                            if ((startBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops) < oneBusStop.TotalTimeFromStartBus) // condition
                            {
                                oneBusStop.ChangeFields(startBusStop.BusStopName, singleResult.LineNumber, 
                                    startBusStop.TotalTimeFromStartBus + singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalDateTime, singleResult.DepartureDateTime);
                            }
                        }
                    }
                }

            } while (true);

            // end main fragments

            // -----------

            return resultList;
        }

        #endregion
    }
}
