using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public class ActualizeRepositoryArgs
    {
        #region Public fields

        public Delegates.UpdateInformationAboutActualization UpdateTimeAboutActualization { get; private set; }
        public string PathToChmFile { get; private set; }

        #endregion

        #region Constructors

        public ActualizeRepositoryArgs(Delegates.UpdateInformationAboutActualization updateDelegate, string pathToChmFile)
        {
            UpdateTimeAboutActualization = updateDelegate;
            PathToChmFile = pathToChmFile;
        }

        #endregion
    }
}
