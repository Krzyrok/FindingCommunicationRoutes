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

            if (_repository.BusStops == null)
            {
                return null;
            }

            foreach (BusStop busStop in _repository.BusStops)
            {
                listOfBusStopsNames.Add(busStop.BusStopName);
            }
            listOfBusStopsNames.Sort();
            return listOfBusStopsNames;
        }

        public void SearchRoute(object args)
        {
            Delegates.UpdateInformationAboutSearching updateInformation = ((SearchRouteArgs)args).DelegateToUpdatingInformationAboutSearching;
            updateInformation("Searching direct connection", 0);
            
            List<SearchResultConnection> result = new List<SearchResultConnection>();
            
            SoughtConnection soughtConnection = ((SearchRouteArgs)args).UserSoughtConnection;      
            SearcherOfDirectRoutes searcherDirectRoutes = new SearcherOfDirectRoutes();
            SearchResultConnection directConnection = searcherDirectRoutes.FindDirectConnection(_repository, soughtConnection);
            if (directConnection != null)
            {
                result.Add(directConnection);
            }

            updateInformation("Searching indirect connection", 50);
            SearcherOfIndirectRoutes searcherOfIndirectRoutes = new SearcherOfIndirectRoutes();
            List<SearchResultConnection> indirectConnection = searcherOfIndirectRoutes.FindIndirectConnection(_repository, soughtConnection);
            if (indirectConnection != null)
            {
                result.AddRange(indirectConnection);
            }
            Delegates.DeliverResults deliverResults = ((SearchRouteArgs)args).DelegateToDeliverResultsToView;
            deliverResults(result);
        }

        public void ActualizeRepository(object args)
        {
            string pathForHtmlFiles = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            pathForHtmlFiles += "\\HtmlSchedule";
            _repository.ActualizeFromChm(((ActualizeRepositoryArgs)args).PathToChmFile, pathForHtmlFiles, ((ActualizeRepositoryArgs)args).UpdateTimeAboutActualization);
        }

        #endregion

        #region Private methods
        private string FindPathForNewSchedule()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }
        #endregion

        #region Private fields

        Repository _repository;

        #endregion
    }
}
