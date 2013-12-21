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

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="BusStops">The bus stops list to store.</param>
        public Repository()
        {
        }

        public void ActualizeFromChm(string chmFilePath, string outPath)
        {
            ReaderCHM chm = new ReaderCHM(chmFilePath, outPath);
            chm.Decompile();
            ReaderHTML html = new ReaderHTML(chm.GetIndexFileFromOutputPath().First());
            SaveDataAboutBusStops(html.GetBusStops(html.GetBusLines()));
        }

        private void SaveDataAboutBusStops(List<BusStop> BusStops)
        {
            _fs = new FileStream(_repositoryPath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(_fs, BusStops);
            _fs.Close();
        }

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

        private string _repositoryPath = @"repository.bin";

        private FileStream _fs;
    }
}
