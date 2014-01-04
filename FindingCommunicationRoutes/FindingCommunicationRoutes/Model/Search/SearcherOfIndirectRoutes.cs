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

            SearchResultConnection singleResult = new SearchResultConnection(false, "50", soughtConnection.DateAndTime, 
                new TimeOfArrival(12, 5), new TimeOfArrival(12, 15), new TimeOfArrival(0, 10), "bus stop1 name", "bus stop2 name");
            resultList.Add(singleResult);

            singleResult = new SearchResultConnection(false, "66", soughtConnection.DateAndTime,
                new TimeOfArrival(12, 25), new TimeOfArrival(12, 45), new TimeOfArrival(0, 20), "bus stop2 name", "bus stop3 name");
            resultList.Add(singleResult);

            singleResult = new SearchResultConnection(false, "76", soughtConnection.DateAndTime,
                new TimeOfArrival(12, 59), new TimeOfArrival(13, 15), new TimeOfArrival(0, 16), "bus stop3 name", "bus stop4 name");
            resultList.Add(singleResult);

            return resultList;
        }

        #endregion
    }
}
