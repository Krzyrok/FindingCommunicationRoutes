using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<TemporaryTrackNode> GetLineBusStopData(string busLine, string busStop, string nextBusStop)
        {
            List<TemporaryTrackNode> trackNodesLst = new List<TemporaryTrackNode>();
            List<string> daysType = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            int counter = 0;

            doc.Load(_filePath);
            HtmlNode busStopTable = doc.DocumentNode.SelectSingleNode("//table[contains(@id, 'tabliczka_przystankowo')]"); // nie, tu nie ma literówki!
            HtmlNodeCollection daysTypeNames = busStopTable.SelectNodes("*/th");

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
                            trackNodesLst.Add(new TemporaryTrackNode(new TimeOfArrival(hour, minutes), busLine, busStop, nextBusStop, letter, daysType.ElementAt(counter)));
                        }
                    }
                }
                ++counter;
            }
            return trackNodesLst;
        }
    }
}
