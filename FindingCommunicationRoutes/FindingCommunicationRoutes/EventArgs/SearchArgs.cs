using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class SearchArgs : EventArgs
    {
        #region Public fields

        public SoughtConnection SoughtConnectionByUser { get; private set; }
        public bool ShouldSearchOnlyDirectConnections { get; private set; }

        #endregion

        #region Constructors

        public SearchArgs(SoughtConnection soughtConnectionByUser, bool shouldSearchOnlyDirectConnections)
        {
            SoughtConnectionByUser = soughtConnectionByUser;
            ShouldSearchOnlyDirectConnections = shouldSearchOnlyDirectConnections;
        }

        #endregion
    }
}
