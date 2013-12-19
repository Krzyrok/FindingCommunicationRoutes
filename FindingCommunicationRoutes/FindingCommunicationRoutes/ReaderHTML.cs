using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace FindingCommunicationRoutes
{
    public class ReaderHTML
    {
        private String _filePath;

        private Dictionary<string, List<string>> _preRepository;

        public ReaderHTML(String filePathHTML)
        {
            if (filePathHTML.EndsWith(".html"))
            {
                _filePath = filePathHTML;
            }
            _preRepository = new Dictionary<string, List<string>>();
        }

        public void ChangeSite(string lastPartOfLink)
        {
            int whereCut = _filePath.LastIndexOf('\\');
            _filePath = _filePath.Substring(0, whereCut+1);
            _filePath += lastPartOfLink;
        }

        private List<string>[] GetBusStopsLinksAndNames()
        {
            List<string>[] busStopsLinksAndNamesx2Directions = new List<string>[4];
            HtmlNode directions;
            HtmlNodeCollection dir1, dir2;

            for (int i = 0; i < busStopsLinksAndNamesx2Directions.Length; ++i)
            {
                busStopsLinksAndNamesx2Directions[i] = new List<string>();
            }

            HtmlDocument doc = new HtmlDocument();
            StreamReader reader = new StreamReader(WebRequest.Create(_filePath).GetResponse().GetResponseStream(), Encoding.UTF8); //put your encoding
            doc.Load(reader);
            try
            {
                directions = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'lewo')]");
                dir1 = directions.SelectNodes("*/tr/td[contains(@class, ' td_przystanek ')]/a");
                directions = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'prawo')]");
                dir2 = directions.SelectNodes("*/tr/td[contains(@class, ' td_przystanek ')]/a");

                foreach (HtmlNode node in dir1)
                {
                    HtmlAttributeCollection atributes = node.Attributes;
                    string link = atributes["href"].Value;
                    busStopsLinksAndNamesx2Directions[0].Add(link);
                    busStopsLinksAndNamesx2Directions[1].Add(node.InnerText);
                }

                foreach (HtmlNode node in dir2)
                {
                    HtmlAttributeCollection atributes = node.Attributes;
                    string link = atributes["href"].Value;
                    busStopsLinksAndNamesx2Directions[2].Add(link);
                    busStopsLinksAndNamesx2Directions[3].Add(node.InnerText);
                }
            }
            catch
            {
            }

            return busStopsLinksAndNamesx2Directions;
        }

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

        public Repository Repository
        {
            get { return new Repository(GetBusStops(GetBusLines())); }
        }

        public List<Line> GetBusLines()
        {
            string indexSite = _filePath;
            List<Line> lines = new List<Line>();
            List<string>[] linksAndNames = new List<string>[2];
            linksAndNames[0] = new List<string>();
            linksAndNames[1] = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            StreamReader reader = new StreamReader(WebRequest.Create(_filePath).GetResponse().GetResponseStream(), Encoding.UTF8); //put your encoding
            doc.Load(reader);
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//a[@href]");

            foreach(HtmlNode nd in collection)
            {
               string link = nd.Attributes.First(x => x.Name.Equals("href")).Value.Substring(2);
               link = link.Replace('/', '\\');
               string[] tmp = nd.Attributes.First(x => x.Name.Equals("href")).Value.Split('/');
               string name = tmp[1];
               linksAndNames[0].Add(link);
               linksAndNames[1].Add(name);
            }

            for (int i = 0; i < linksAndNames[0].Count; ++i )
            {
                _filePath = indexSite;
                ChangeSite(linksAndNames[0][i]);
                lines.Add(GetBusLine(_filePath, linksAndNames[1][i]));
            }

            return lines;
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
        /// Gets the bus line.
        /// </summary>
        /// <param name="linkToTrackSite">The link to the track site.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <returns>Line class representing the bus line.</returns>
        private Line GetBusLine(string linkToTrackSite, string lineNumber)
        {
            int greaterThenDayTypesQuantity = 10;
            _filePath = linkToTrackSite;
            List<String>[] list = GetBusStopsLinksAndNames();
            CollectDataToPrerepository(list[1], lineNumber);
            CollectDataToPrerepository(list[3], lineNumber);

            TrackBuilder[] trackBuilders = new TrackBuilder[greaterThenDayTypesQuantity];
            for (int i = 0; i < greaterThenDayTypesQuantity; ++i)
            {
                trackBuilders[i] = new TrackBuilder();
            }

            for (int i = 0; i + 1 < list[1].Count; ++i)
            {
                ChangeSite(list[0][i]);
                List<TemporaryTrackNode>[] tmplst = GetLineBusStopData(lineNumber, list[1][i], list[1][i + 1]);
                if (tmplst == null) continue;
                for (int j = 0; j < tmplst.Length; ++j)
                    trackBuilders[j].AddListOfNodes(tmplst[j]);
            }

            List<Track>[] tracks = new List<Track>[greaterThenDayTypesQuantity];

            for (int i = 0; i < greaterThenDayTypesQuantity; ++i)
            {
                tracks[i] = trackBuilders[i].BuildTracks();
            }

            return new Line(lineNumber, tracks);
        }

        private List<TemporaryTrackNode>[] GetLineBusStopData(string busLine, string busStop, string nextBusStop)
        {
            
            List<string> daysType = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            int counter = 0;

            doc.Load(_filePath);
            HtmlNode busStopTable = doc.DocumentNode.SelectSingleNode("//table[contains(@id, 'tabliczka_przystankowo')]"); // nie, tu nie ma literówki!
            HtmlNodeCollection daysTypeNames;
            if (busStopTable != null)
                daysTypeNames = busStopTable.SelectNodes("*/th");
            else
                return null;

            List<TemporaryTrackNode>[] trackNodesLst = new List<TemporaryTrackNode>[daysTypeNames.Count];
            for (int i = 0; i < trackNodesLst.Length; ++i )
            {
                trackNodesLst[i] = new List<TemporaryTrackNode>();
            }

            // nazwy typów dni (dni robocze, wolne itp.)
            foreach (HtmlNode daysTypeName in daysTypeNames)
            {
                daysType.Add(daysTypeName.InnerHtml);
            }

            HtmlNodeCollection daysTypeHours = busStopTable.SelectNodes("*/td");
            foreach (HtmlNode dayTypeHours in daysTypeHours)
            {
                //TrackBuilder builder = new TrackBuilder(daysType.ElementAt(counter));

                HtmlNodeCollection times = dayTypeHours.SelectNodes("span[contains(@id, 'blok_godzina')]");
                foreach (HtmlNode time in times)
                {
                    int hour = 25;  // if hour remain 25 it means that something goes wrong
                    int minutes;
                    string letter = "";
                    List<TemporaryTrackNode> dataForBuildingATrack = new List<TemporaryTrackNode>();
                    HtmlNodeCollection nodes = time.SelectNodes("*");
                    for (int i = 0; i < nodes.Count; ++i)
                    {
                        if (i == 0)
                        {
                            hour = int.Parse(nodes.ElementAt(i).InnerHtml);
                        }
                        else
                        {
                            if(!int.TryParse(nodes.ElementAt(i).InnerText, out minutes))
                            {
                                minutes = int.Parse(nodes.ElementAt(i).InnerText.Substring(0, 2));
                                //HtmlNode letterInside = nodes.ElementAt(i).SelectSingleNode("span");
                                letter = nodes.ElementAt(i).InnerText.Substring(2, 1);
                            }
                            trackNodesLst[counter].Add(new TemporaryTrackNode(new TimeOfArrival(hour, minutes), busLine, busStop, nextBusStop, letter, daysType.ElementAt(counter)));
                        }
                    }
                }
                ++counter;
            }
            return trackNodesLst;
        }
    }
}
