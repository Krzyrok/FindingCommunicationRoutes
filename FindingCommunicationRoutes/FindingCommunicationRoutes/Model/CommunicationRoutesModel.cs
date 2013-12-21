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

            foreach (BusStop busStop in _repository.BusStops)
            {
                listOfBusStopsNames.Add(busStop.BusStopName);
            }

            return listOfBusStopsNames;
        }

        public void ActualizeRepository(UpdateInformationAboutActualization updateDelegate)
        {
            string path = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            path += "\\HtmlSchedule";
            //_repository.ActualizeFromChm(@"2013-12-18.chm", path);
            // should be
            _repository.ActualizeFromChm(@"2013-12-18.chm", path, updateDelegate);
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
