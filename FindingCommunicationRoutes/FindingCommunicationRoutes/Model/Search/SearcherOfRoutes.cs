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

            return result;
        }

        #endregion
    }
}
