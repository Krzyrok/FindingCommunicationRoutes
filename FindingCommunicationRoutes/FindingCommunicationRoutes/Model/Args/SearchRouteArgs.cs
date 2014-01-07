using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearchRouteArgs
    {
        #region Public fields

        public SearchArgs UserSearchArgs { get; private set; }
        public Delegates.UpdateInformationAboutSearching DelegateToUpdatingInformationAboutSearching { get; private set; }
        public Delegates.DeliverResults DelegateToDeliverResultsToView { get; private set; }

        #endregion

        #region Constructors

        public SearchRouteArgs(Delegates.UpdateInformationAboutSearching delegateToUpdatingInformationAboutSearching,
            Delegates.DeliverResults delegateToDeliverResultsToView, SearchArgs userSearchArgs)
        {
            DelegateToUpdatingInformationAboutSearching = delegateToUpdatingInformationAboutSearching;
            UserSearchArgs = userSearchArgs;
            DelegateToDeliverResultsToView = delegateToDeliverResultsToView;
        }

        #endregion
    }
}
