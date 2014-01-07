﻿using System;
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
            Delegates.DeliverResults deliverResults = ((SearchRouteArgs)args).DelegateToDeliverResultsToView;
            
            List<SearchResultConnection> result = new List<SearchResultConnection>();
            
            SoughtConnection soughtConnection = ((SearchRouteArgs)args).UserSearchArgs.SoughtConnectionByUser;      
            SearcherOfDirectRoutes searcherDirectRoutes = new SearcherOfDirectRoutes();
            SearchResultConnection directConnection = searcherDirectRoutes.FindDirectConnection(_repository, soughtConnection);
            if (directConnection != null)
            {
                result.Add(directConnection);
                deliverResults(result);
                result.Remove(directConnection);
            }

            if (((SearchRouteArgs)args).UserSearchArgs.ShouldSearchOnlyDirectConnections)
            {
                if (directConnection == null)
                {
                    updateInformation("No results", 100);
                }
                else
                {
                    updateInformation("Searching done", 100);
                }
                return;
            }
            updateInformation("Searching indirect connection", 10);
            SearcherOfIndirectRoutes searcherOfIndirectRoutes = new SearcherOfIndirectRoutes();
            List<SearchResultConnection> indirectConnection = searcherOfIndirectRoutes.FindIndirectConnection(_repository, soughtConnection, updateInformation);
            if (indirectConnection != null)
            {
                result.AddRange(indirectConnection);
            }
            deliverResults(result);

            if (directConnection == null && (indirectConnection == null || indirectConnection.Count == 0))
            {
                updateInformation("No results", 100);
            }
            else
            {
                updateInformation("Searching done", 100);
            }
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
