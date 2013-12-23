using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Repository of bus stops.
    /// </summary>
    [Serializable]
    public class Repository
    {
        #region public fields

        /// <summary>
        /// Gets the list of bus stops.
        /// </summary>
        /// <value>
        /// The bus stops list.
        /// </value>
        public List<BusStop> BusStops
        {
            get { return LoadDataAboutBusStops(); }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="BusStops">The bus stops list to store.</param>
        public Repository()
        {
        }

        #endregion

        #region public methods

        /// <summary>
        /// Actualizes binary file with data from CHM file.
        /// </summary>
        /// <param name="chmFilePath">The CHM file path.</param>
        /// <param name="outputPath">The output path for html files.</param>
        public void ActualizeFromChm(string chmFilePath, string outputPath, Delegates.UpdateInformationAboutActualization updateDelegate)
        {
            updateDelegate(0.0);
            ReaderCHM chm = new ReaderCHM(chmFilePath, outputPath);
            chm.Decompile();
            ReaderHTML html = new ReaderHTML(chm.GetIndexFileFromOutputPath().First());
            SaveDataAboutBusStops(html.GetBusStops(html.GetBusLines(updateDelegate)));
        }

        #endregion

        #region private methods

        /// <summary>
        /// Saves the data about bus stops in binary file.
        /// </summary>
        /// <param name="BusStops">The list of bus stops.</param>
        private void SaveDataAboutBusStops(List<BusStop> BusStops)
        {
            _fs = new FileStream(_repositoryPath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(_fs, BusStops);
            _fs.Close();
        }

        /// <summary>
        /// Loads the data about bus stops from binary file.
        /// </summary>
        /// <returns>List with loaded bus stops</returns>
        private List<BusStop> LoadDataAboutBusStops()
        {
            try
            {
                _fs = new FileStream(_repositoryPath, FileMode.Open);
            }
            catch
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            List<BusStop> busStopsList = (List<BusStop>)bf.Deserialize(_fs);
            _fs.Close();
            return busStopsList;
        }

        #endregion

        #region private fields

        /// <summary>
        /// The path to the binary file with repository data.
        /// </summary>
        private string _repositoryPath = @"repository.bin";

        /// <summary>
        /// The file stream to binary file.
        /// </summary>
        private FileStream _fs;

        #endregion
    }
}
