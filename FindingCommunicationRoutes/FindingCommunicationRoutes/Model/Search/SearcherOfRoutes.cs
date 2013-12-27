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
            TimeOfArrival time = new TimeOfArrival(0, 0);
            SearchResultDirectConnection result = new SearchResultDirectConnection("", time, time);

            List<BusStop> busStops = repository.BusStops;
            if (busStops == null)
            {
                return null;
            }

            TypeOfDayRecognizer dayRecognizer = new TypeOfDayRecognizer();
            List<string> dayType = dayRecognizer.RecognizeTypeOfDay(soughtConnection.DateTime);

            BusStop startBusStop;
            BusStop endBusStop;

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

            return result;
        }

        #endregion
    }
}
