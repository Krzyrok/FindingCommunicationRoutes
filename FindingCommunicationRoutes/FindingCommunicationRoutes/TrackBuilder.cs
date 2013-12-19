using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Building tracks for one type of day (eg. working days, holidays)
    /// </summary>
    public class TrackBuilder
    {
        private List<List<TemporaryTrackNode>> _tracksData;

        public string DayType
        {
            get;
            private set;
        }

        public string Line
        {
            get;
            private set;
        }

        public TrackBuilder()
        {
            DayType = "";
            _tracksData = new List<List<TemporaryTrackNode>>();
        }

        public List<Track> BuildTracks()
        {
            List<Track> tracks = new List<Track>();

            foreach (List<TemporaryTrackNode> list in _tracksData)
            {
                Dictionary<string, TimeOfArrival> track = new Dictionary<string, TimeOfArrival>();
                foreach (TemporaryTrackNode tmp in list)
                {
                    try
                    {
                        track.Add(tmp.BusStop, tmp.Hour);
                    }
                    catch { }
                }
                tracks.Add(new Track(track));
            }

            return tracks;
        }

        public void Sort()
        {
            TimeOfArrival tmp = new TimeOfArrival(0,0);
            List<TemporaryTrackNode> o = new List<TemporaryTrackNode>();
            List<List<TemporaryTrackNode>> _tracksData2 = new List<List<TemporaryTrackNode>>();
            do
            {
                foreach (List<TemporaryTrackNode> t in _tracksData)
                {
                    if (t.First().Hour > tmp)
                    {
                        tmp = new TimeOfArrival(t.First().Hour);
                        o = t;
                    }
                }
                _tracksData2.Add(o);
                _tracksData.Remove(o);
                tmp = new TimeOfArrival(0, 0);
            } while (_tracksData.Count > 0);
            _tracksData.AddRange(_tracksData2);
        }

        public void AddListOfNodes(List<TemporaryTrackNode> list)
        {
            foreach (TemporaryTrackNode tmp in list)
            {
                AddNode(tmp);
            }
            Sort();
        }

        public void AddNode(TemporaryTrackNode node)
        {
            if (_tracksData.Count == 0)
            {
                DayType = node.DayType;
                Line = node.Line;
            }
            int bestPosition = -1;
            for ( int i = _tracksData.Count-1; i >= 0 ; --i )
            {
                TemporaryTrackNode tmp = _tracksData.ElementAt(i).Last();

                if (node.DayType.Equals(DayType) &&
                    node.Line.Equals(tmp.Line) &&
                    node.BusStop.Equals(tmp.NextBusStop))
                {
                    
                    if (node.Hour > tmp.Hour &&
                        node.Letter.Equals(tmp.Letter))
                    {
                        bestPosition = i;
                        //_tracksData.ElementAt(i).Add(node);
                        //return;
                    }
                }
            }
            if (bestPosition != -1)
            {
                _tracksData.ElementAt(bestPosition).Add(node);
            }
            else
            {
                List<TemporaryTrackNode> tmp2 = new List<TemporaryTrackNode>();
                tmp2.Add(node);
                _tracksData.Insert(0, tmp2);
            }
        }
    }
}
