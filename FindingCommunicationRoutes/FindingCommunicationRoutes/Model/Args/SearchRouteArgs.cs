using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearchRouteArgs
    {
        #region Public fields

        public SoughtConnection UserSoughtConnection { get; private set; }
        public Delegates.UpdateInformationAboutSearching DelegateToUpdatingInformationAboutSearching { get; private set; }
        public Delegates.DeliverResults DelegateToDeliverResultsToView { get; private set; }

        #endregion

        #region Constructors

        public SearchRouteArgs(Delegates.UpdateInformationAboutSearching delegateToUpdatingInformationAboutSearching,
            Delegates.DeliverResults delegateToDeliverResultsToView, SoughtConnection userSoughtConnection)
        {
            DelegateToUpdatingInformationAboutSearching = delegateToUpdatingInformationAboutSearching;
            UserSoughtConnection = userSoughtConnection;
            DelegateToDeliverResultsToView = delegateToDeliverResultsToView;
        }

        #endregion
    }
}
