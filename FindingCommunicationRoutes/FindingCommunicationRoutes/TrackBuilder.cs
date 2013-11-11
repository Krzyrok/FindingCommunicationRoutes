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
                    track.Add(tmp.BusStop, tmp.Hour);
                }
                tracks.Add(new Track(track));
            }

            return tracks;
        }

        public void AddListOfNodes(List<TemporaryTrackNode> list)
        {
            foreach (TemporaryTrackNode tmp in list)
            {
                AddNode(tmp);
            }
        }

        public void AddNode(TemporaryTrackNode node)
        {
            if (_tracksData.Count == 0)
            {
                DayType = node.DayType;
                Line = node.Line;
            }
            bool flag = true;
            int place = 0;
            for ( int i = _tracksData.Count-1; i >= 0 ; --i )
            {
                TemporaryTrackNode tmp = _tracksData.ElementAt(i).Last();
                if (node.DayType.Equals(DayType) &&
                    node.Line.Equals(tmp.Line) &&
                    node.BusStop.Equals(tmp.NextBusStop))
                {
                    if (flag && node.Hour > tmp.Hour)
                    {
                        flag = false;
                        place = i + 1;
                    }
                    if (node.Hour > tmp.Hour &&
                        node.Letter.Equals(tmp.Letter))
                    {
                        _tracksData.ElementAt(i).Add(node);
                        return;
                    }
                }
                else
                    return;
            }
            List<TemporaryTrackNode> tmp2 = new List<TemporaryTrackNode>();
            tmp2.Add(node);
            _tracksData.Insert(place, tmp2);
        }
    }
}
