using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Class used for load html file generated from chm with timetables and collect informations from it.
    /// </summary>
    public class ReaderHTML
    {
        #region private fields

        /// <summary>
        /// The html site path
        /// </summary>
        private String _htmlSitePath;

        /// <summary>
        /// The pre repository used for storing data and form Repository.
        /// </summary>
        private Dictionary<string, List<string>> _preRepository;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderHTML"/> class.
        /// </summary>
        /// <param name="filePathHTML">The html file path.</param>
        public ReaderHTML(String filePathHTML)
        {
            if (filePathHTML.EndsWith(".html"))
            {
                _htmlSitePath = filePathHTML;
            }
            _preRepository = new Dictionary<string, List<string>>();
        }

        #endregion

        #region public fields

        /// <summary>
        /// Gets the repository containing all timetables.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public Repository Repository
        {
            get { return new Repository(); }
        }

        /// <summary>
        /// Gets the bus stops from list of lines and prerepository (private variable).
        /// </summary>
        /// <param name="lines">The list of lines.</param>
        /// <returns>List of Bus Stops</returns>
        public List<BusStop> GetBusStops(List<Line> lines)
        {
            List<BusStop> busStops = new List<BusStop>();

            foreach (KeyValuePair<string, List<string>> pair in _preRepository)
            {
                List<Line> tmplst = new List<Line>();
                foreach (Line line in lines)
                {
                    if (pair.Value.Contains(line.Number))
                    {
                        tmplst.Add(new Line(line));
                    }
                }
                busStops.Add(new BusStop(tmplst, pair.Key));
            }

            return busStops;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Navigates to the link.
        /// </summary>
        /// <param name="link">The link.</param>
        public void NavigateTo(string link)
        {
            _htmlSitePath = link;
        }

        /// <summary>
        /// Gets the bus lines.
        /// </summary>
        /// <returns>List of bus lines</returns>
        public List<Line> GetBusLines(Delegates.UpdateInformationAboutActualization updateDelegate)
        {
            string indexSite = _htmlSitePath;
            List<Line> lines = new List<Line>();
            List<string>[] linksAndNames;
            HtmlDocument doc = new HtmlDocument();

            // preparing html document
            StreamReader reader = new StreamReader(WebRequest.Create(_htmlSitePath).GetResponse().GetResponseStream(), Encoding.UTF8); //put your encoding
            doc.Load(reader);

            linksAndNames = CollectLinesLinksAndNamesFromIndexSite(doc);

            for (int i = 0; i < linksAndNames[0].Count; ++i)
            {
                NavigateTo(indexSite);
                ChangeSite(linksAndNames[0][i]);
                // from line tracks site
                Line tmpline = GetBusLine(_htmlSitePath, linksAndNames[1][i]);
                updateDelegate((double)(i * 100.0 / linksAndNames[0].Count));
                if (tmpline != null)
                    lines.Add(tmpline);
            }

            return lines;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Changes the site, by replacing last part of the link.
        /// </summary>
        /// <param name="lastPartOfLink">The last part of link to be changed.</param>
        private void ChangeSite(string lastPartOfLink)
        {
            int whereCut = _htmlSitePath.LastIndexOf('\\');
            _htmlSitePath = _htmlSitePath.Substring(0, whereCut+1);
            _htmlSitePath += lastPartOfLink;
        }

        /// <summary>
        /// Selects the directions nodes.
        /// </summary>
        /// <param name="doc">The html document</param>
        /// <returns>Collections of nodes of each direction</returns>
        private HtmlNodeCollection[] SelectDirectionsNodes(HtmlDocument doc)
        {
            HtmlNode node;
            HtmlNodeCollection[] directions = new HtmlNodeCollection[2];

            node = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'lewo')]");
            directions[0] = node.SelectNodes("*/tr/td[contains(@class, ' td_przystanek ')]/a");
            node = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'prawo')]");
            directions[1] = node.SelectNodes("*/tr/td[contains(@class, ' td_przystanek ')]/a");

            return directions;
        }

        /// <summary>
        /// Helps getting the bus stops links and names for both directions.
        /// </summary>
        /// <param name="listsForLinkAndNamesx2Dirs">Lists for link and names for each direction.</param>
        /// <param name="doc">The html document</param>
        /// <returns>Lists with bus stops links and names</returns>
        private List<string>[] HelpsGettingBusStopsLinksAndNamesx2Dirs(List<string>[] listsForLinkAndNamesx2Dirs,HtmlDocument doc)
        {

            HtmlNodeCollection[] directions = SelectDirectionsNodes(doc);

            foreach (HtmlNode node in directions[0])
            {   // for first direction
                HtmlAttributeCollection atributes = node.Attributes;
                string link = atributes["href"].Value;
                listsForLinkAndNamesx2Dirs[0].Add(link); // link
                listsForLinkAndNamesx2Dirs[1].Add(node.InnerText); // name
            }

            foreach (HtmlNode node in directions[1])
            {   // for second direction
                HtmlAttributeCollection atributes = node.Attributes;
                string link = atributes["href"].Value;
                listsForLinkAndNamesx2Dirs[2].Add(link); // link
                listsForLinkAndNamesx2Dirs[3].Add(node.InnerText); // name
            }

            return listsForLinkAndNamesx2Dirs;
        }

        /// <summary>
        /// Gets the bus stops links and names for both directions.
        /// </summary>
        /// <returns>Lists with bus stops links and names</returns>
        private List<string>[] GetBusStopsLinksAndNames()
        {
            StreamReader reader;
            HtmlDocument doc = new HtmlDocument();
            List<string>[] busStopsLinksAndNamesx2Directions = new List<string>[4];

            for (int i = 0; i < busStopsLinksAndNamesx2Directions.Length; ++i)
            {
                busStopsLinksAndNamesx2Directions[i] = new List<string>();
            }


            try
            {
                reader = new StreamReader
                    (WebRequest.Create(_htmlSitePath).GetResponse().GetResponseStream(),
                    Encoding.UTF8); //put your encoding
            }
            catch
            {
                return null;
            }

            doc.Load(reader);

            try
            {
                busStopsLinksAndNamesx2Directions = HelpsGettingBusStopsLinksAndNamesx2Dirs 
                    (busStopsLinksAndNamesx2Directions, doc);
            }
            catch
            {
            }

            return busStopsLinksAndNamesx2Directions;
        }

        /// <summary>
        /// Collect the lines links and names from the index site.
        /// </summary>
        /// <param name="doc">The html document with index site</param>
        /// <returns>List with line links[0] and names[1]</returns>
        private List<string>[] CollectLinesLinksAndNamesFromIndexSite(HtmlDocument doc)
        {
            List<string>[] linksAndNames = new List<string>[2];
            linksAndNames[0] = new List<string>();
            linksAndNames[1] = new List<string>();

            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//a[@href]");

            foreach (HtmlNode nd in collection)
            {
                string link = nd.Attributes.First(x => x.Name.Equals("href")).Value.Substring(2);
                link = link.Replace('/', '\\');
                string[] tmp = nd.Attributes.First(x => x.Name.Equals("href")).Value.Split('/');
                string name = tmp[1];
                linksAndNames[0].Add(link);
                linksAndNames[1].Add(name);
            }

            return linksAndNames;
        }

        /// <summary>
        /// Collecting data about through which bus stops the line runs.
        /// Data is storing to private varialble in ReaderHtml class.
        /// </summary>
        /// <param name="busStopsNames">Bus stops names through which the line runs.</param>
        /// <param name="lineNumber">Line number.</param>
        private void CollectDataToPrerepository(List<string> busStopsNames, string lineNumber)
        {
            foreach (string str in busStopsNames)
            {
                try
                {
                    if (!_preRepository[str].Contains(lineNumber))
                    {
                        _preRepository[str].Add(lineNumber);
                    }
                }
                catch
                {
                    // when bus stop is mentioned for the first time.
                    _preRepository.Add(str,new List<string>());
                    _preRepository[str].Add(lineNumber);
                }
            }

        }

        /// <summary>
        /// Initializes the track builders.
        /// </summary>
        /// <param name="numberOfTrackBuildersToInitialize">The number of track builders to initialize.</param>
        /// <returns>Instances of track builders.</returns>
        private TrackBuilder[] InitializeTrackBuilders(int numberOfTrackBuildersToInitialize)
        {
            TrackBuilder[] builders = new TrackBuilder[numberOfTrackBuildersToInitialize];
            for (int i = 0; i < numberOfTrackBuildersToInitialize; ++i)
            {
                builders[i] = new TrackBuilder();
            }
            return builders;
        }

        /// <summary>
        /// Loads the data from bus stop into track builders.
        /// </summary>
        /// <param name="dataStorageBuilders">The data storage builders.</param>
        /// <param name="busStopLinks">The bus stop links.</param>
        /// <param name="busStopNames">The bus stop names.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <returns>Tack Builders filled with data.</returns>
        private TrackBuilder[] LoadDataFromBusStops(TrackBuilder[] dataStorageBuilders, List<string> busStopLinks, List<string> busStopNames, string lineNumber)
        {
            for (int i = 0; i + 1 < busStopLinks.Count; ++i)
            {
                ChangeSite(busStopLinks[i]);
                List<TemporaryTrackNode>[] tmplst = GetLineBusStopData(lineNumber, busStopNames[i], busStopNames[i + 1]);
                if (tmplst == null) continue;
                for (int j = 0; j < tmplst.Length; ++j)
                    dataStorageBuilders[j].AddListOfNodes(tmplst[j]);
            }
            return dataStorageBuilders;
        }

        /// <summary>
        /// Gets the bus line containing all data from track site.
        /// </summary>
        /// <param name="linkToTrackSite">The link to the track site.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <returns>Line class representing the bus line.</returns>
        private Line GetBusLine(string linkToTrackSite, string lineNumber)
        {
            int greaterThenDayTypesQuantity = 10; // number that is greater than number of day types

            NavigateTo(linkToTrackSite);

            List<String>[] list = GetBusStopsLinksAndNames();
            if (list == null) return null;

            CollectDataToPrerepository(list[1], lineNumber);
            CollectDataToPrerepository(list[3], lineNumber);

            TrackBuilder[] trackBuilders = InitializeTrackBuilders(greaterThenDayTypesQuantity);
            TrackBuilder[] trackBuilders2 = InitializeTrackBuilders(greaterThenDayTypesQuantity);

            // preparing track lists
            List<Track>[] tracks = new List<Track>[greaterThenDayTypesQuantity*2];

            trackBuilders = LoadDataFromBusStops(trackBuilders, list[0],
                                        list[1], lineNumber);

            trackBuilders2 = LoadDataFromBusStops(trackBuilders2, list[2],
                                        list[3], lineNumber);

            // building tracks and loading them into track lists

            for (int i = 0; i < greaterThenDayTypesQuantity; ++i)
            {
                tracks[i] = trackBuilders[i].BuildTracks();
                tracks[i].AddRange(trackBuilders2[i].BuildTracks());
            }

            return new Line(lineNumber, tracks);
        }

        /// <summary>
        /// Initializes the temporary track node lists.
        /// </summary>
        /// <param name="numberOfListsToInitialize">The number of lists to initialize.</param>
        /// <returns>Instances of TemporaryTrackNode lists.</returns>
        private List<TemporaryTrackNode>[] InitializeTemporaryTrackNodeLists(int numberOfListsToInitialize)
        {
            List<TemporaryTrackNode>[] trackNodesLst = new List<TemporaryTrackNode>[numberOfListsToInitialize];
            for (int i = 0; i < trackNodesLst.Length; ++i)
            {
                trackNodesLst[i] = new List<TemporaryTrackNode>();
            }
            return trackNodesLst;
        }

        /// <summary>
        /// Gets the data of times within one hour.
        /// </summary>
        /// <param name="nodeWithAllTimesWithinOneHour">The node with all times within one hour.</param>
        /// <param name="busLine">The bus line.</param>
        /// <param name="busStop">The bus stop.</param>
        /// <param name="nextBusStop">The next bus stop.</param>
        /// <param name="daysType">Type of the days.</param>
        /// <param name="counterOfDaysType">Type of the counter of days.</param>
        /// <returns>Temporary Track Nodes for TrackBuilder</returns>
        private List<TemporaryTrackNode> GetDataOfTimesWithinOneHour(HtmlNode nodeWithAllTimesWithinOneHour, string busLine,
                                   string busStop, string nextBusStop, List<string> daysType, int counterOfDaysType)
        {
            int hour = 25;  // if hour remain 25 it means that something goes wrong
            int minutes;
            string letter = ""; // default there is no letter

            List<TemporaryTrackNode> dataForBuildingATrack = new List<TemporaryTrackNode>();

            // separating nodes for hour and minutes with optional letter
            HtmlNodeCollection nodes = nodeWithAllTimesWithinOneHour.SelectNodes("*");

            hour = int.Parse(nodes.First().InnerHtml);
            for (int i = 1; i < nodes.Count; ++i)
            {
                if (!int.TryParse(nodes.ElementAt(i).InnerText, out minutes))
                {
                    minutes = int.Parse(nodes.ElementAt(i).InnerText.Substring(0, 2));
                    letter = nodes.ElementAt(i).InnerText.Substring(2, 1);
                }
                dataForBuildingATrack.Add(new TemporaryTrackNode(new TimeOfArrival(hour, minutes), busLine, busStop, nextBusStop, letter, daysType.ElementAt(counterOfDaysType)));
            }
            return dataForBuildingATrack;
        }

        /// <summary>
        /// Selects the nodes with days type names and hours from bus stop site.
        /// </summary>
        /// <param name="doc">The html document.</param>
        /// <returns>Selected nodes with names[0] and hours[1]</returns>
        private HtmlNodeCollection[] SelectNodesWithDaysTypeNamesAndHoursFromBusStopSite(HtmlDocument doc)
        {
            HtmlNodeCollection[] daysTypeNamesAndHours = new HtmlNodeCollection[2];
            
            doc.Load(new StreamReader
                    (WebRequest.Create(_htmlSitePath).GetResponse().GetResponseStream(),
                    Encoding.UTF8));

            HtmlNode busStopTable = doc.DocumentNode.SelectSingleNode("//table[contains(@id, 'tabliczka_przystankowo')]"); // there is no typo here!

            if (busStopTable != null)
            {
                daysTypeNamesAndHours[0] = busStopTable.SelectNodes("*/th");
                daysTypeNamesAndHours[1] = busStopTable.SelectNodes("*/td");
                return daysTypeNamesAndHours;
            }
            else
                return null;
        }

        /// <summary>
        /// Gets the line bus stop data.
        /// </summary>
        /// <param name="busLine">The bus line.</param>
        /// <param name="busStop">The bus stop.</param>
        /// <param name="nextBusStop">The next bus stop.</param>
        /// <returns>Temporary Track Nodes for TrackBuilder</returns>
        private List<TemporaryTrackNode>[] GetLineBusStopData(string busLine, string busStop, string nextBusStop)
        {
            List<string> daysType = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            int counterOfDayTypes = 0;

            HtmlNodeCollection[] daysTypeNamesAndHours = SelectNodesWithDaysTypeNamesAndHoursFromBusStopSite(doc);
            if (daysTypeNamesAndHours == null) return null;
            HtmlNodeCollection daysTypeNames = daysTypeNamesAndHours[0];
            HtmlNodeCollection daysTypeHours = daysTypeNamesAndHours[1];

            List<TemporaryTrackNode>[] trackNodesLst = InitializeTemporaryTrackNodeLists(daysTypeNames.Count);

            // loading days type names 
            foreach (HtmlNode daysTypeName in daysTypeNames)
            {
                daysType.Add(daysTypeName.InnerHtml);
            }

            foreach (HtmlNode dayTypeHours in daysTypeHours)
            {
                // for each day type hours:

                HtmlNodeCollection times = dayTypeHours.SelectNodes("span[contains(@id, 'blok_godzina')]");
                foreach (HtmlNode time in times)
                {
                    // for each hour (times with the same hour and different minutes)

                    trackNodesLst[counterOfDayTypes].AddRange(
                        GetDataOfTimesWithinOneHour(time,busLine,busStop,nextBusStop,daysType,counterOfDayTypes)
                        );
                }
                ++counterOfDayTypes;
            }
            return trackNodesLst;
        }

#endregion
    }
}
