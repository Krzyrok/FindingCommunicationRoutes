using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class Delegates
    {
        public delegate void UpdateInformationAboutActualization(double value);
        public delegate void UpdateInformationAboutSearching(string information, double value);
        public delegate void DeliverResults(List<SearchResultConnection> results);
    }
}
