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
                    //busStopsToCheckList.Add(new SingleBusStopForIndirectConnection(oneBusStop.BusStopName, startBusStop.BusStopName, singleResult.LineNumber, 
                    //    singleResult.TimeDistanceBetweenBusStops, singleResult.ArrivalTime));
                }
            }
            // end initialize

            // -----------

            return resultList;
        }

        #endregion
    }
}
