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

        public SearchResultDirectConnection SearchRoute(SoughtConnection soughtConnection)
        {
            SearcherOfRoutes searcher = new SearcherOfRoutes();
            return searcher.FindDirectConnection(_repository, soughtConnection);
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
