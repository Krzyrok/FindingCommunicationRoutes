using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class CommunicationRoutesModel
    {
        #region Constructors

        public CommunicationRoutesModel(Repository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public methods

        public List<string> GiveListOfBusStopsNames()
        {
            List<string> listOfBusStopsNames = new List<string>();

            foreach (BusStop busStop in _repository.BusStops)
            {
                listOfBusStopsNames.Add(busStop.BusStopName);
            }

            return listOfBusStopsNames;
        }

        #endregion

        #region Private fields

        Repository _repository;

        #endregion
    }
}
