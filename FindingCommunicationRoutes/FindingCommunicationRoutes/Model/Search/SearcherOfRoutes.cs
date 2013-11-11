using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearcherOfRoutes
    {
        #region "Constructors"

        public SearcherOfRoutes(Repository repository)
        {
            _repository = repository;
        }

        #endregion

        #region "Public methods"

        public SearchResultDirectConnection FindDirectConnection(SoughtConnection soughtConnection)
        {
            TimeOfArrival time = new TimeOfArrival(0, 0);
            SearchResultDirectConnection result = new SearchResultDirectConnection("", time, time);
            return result;
        }

        #endregion

        #region "Private fields"

        Repository _repository;

        #endregion
    }
}
