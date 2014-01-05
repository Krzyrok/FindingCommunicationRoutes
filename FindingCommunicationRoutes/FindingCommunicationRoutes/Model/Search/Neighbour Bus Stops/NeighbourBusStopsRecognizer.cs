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

        public void RecognizeNeighbourBusStops()
        {

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

        private List<Line> GiveLinesPlyingThroughBusStop(BusStop busStop)
        {
            return busStop.LinesPlyingThroughBusStop;
        }

        #endregion
    }
}
