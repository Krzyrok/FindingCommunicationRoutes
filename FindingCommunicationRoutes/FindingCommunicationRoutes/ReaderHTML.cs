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

        public ReaderHTML(String filePathHTML)
        {
            if (filePathHTML.EndsWith(".html"))
            {
                _filePath = filePathHTML;
            }
        }

        public void ChangeBusStop(string link)
        {
            int whereCut = _filePath.LastIndexOf('\\');
            _filePath = _filePath.Substring(0, whereCut+1);
            _filePath += link;
        }

        public List<string>[] GetBusStops()
        {
            List<string>[] busStops = new List<string>[4];

            for (int i = 0; i < busStops.Length; ++i)
            {
                busStops[i] = new List<string>();
            }

            HtmlDocument doc = new HtmlDocument();
            StreamReader reader = new StreamReader(WebRequest.Create(_filePath).GetResponse().GetResponseStream(), Encoding.UTF8); //put your encoding
            doc.Load(reader);
            HtmlNode directions = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'lewo')]");
            HtmlNodeCollection dir1 = directions.SelectNodes("*/tr/td[contains(@class, ' td_przystanek ')]/a");
                    directions = doc.DocumentNode.SelectSingleNode("//div[contains(@id, 'prawo')]");
            HtmlNodeCollection dir2 = directions.SelectNodes("*/tr/td[contains(@class, ' td_przystanek ')]/a");

            foreach (HtmlNode node in dir1)
            {
                HtmlAttributeCollection atributes = node.Attributes;
                string link = atributes["href"].Value;
                busStops[0].Add(link);
                busStops[1].Add(node.InnerText);
            }

            foreach (HtmlNode node in dir2)
            {
                HtmlAttributeCollection atributes = node.Attributes;
                string link = atributes["href"].Value;
                busStops[2].Add(link);
                busStops[3].Add(node.InnerText);
            }

            return busStops;
        }

        public List<TemporaryTrackNode>[] GetLineBusStopData(string busLine, string busStop, string nextBusStop)
        {
            
            List<string> daysType = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            int counter = 0;

            doc.Load(_filePath);
            HtmlNode busStopTable = doc.DocumentNode.SelectSingleNode("//table[contains(@id, 'tabliczka_przystankowo')]"); // nie, tu nie ma literówki!
            HtmlNodeCollection daysTypeNames = busStopTable.SelectNodes("*/th");

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
                            if(!int.TryParse(nodes.ElementAt(i).InnerHtml, out minutes))
                            {
                                minutes = int.Parse(nodes.ElementAt(i).InnerHtml.Substring(0, 2));
                                HtmlNode letterInside = nodes.ElementAt(i).SelectSingleNode("span");
                                letter = letterInside.InnerHtml;
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
