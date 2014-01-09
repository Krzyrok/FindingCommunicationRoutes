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
        /// <summary>
        /// Structure to store information about tracks before building.
        /// </summary>
        private List<List<TemporaryTrackNode>> _tracksData;

        /// <summary>
        /// Gets the type of the day for tracks.
        /// </summary>
        /// <value>
        /// The type of the day for tracks.
        /// </value>
        public string DayType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the bus line.
        /// </summary>
        /// <value>
        /// The bus line.
        /// </value>
        public string Line
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackBuilder"/> class.
        /// </summary>
        public TrackBuilder()
        {
            DayType = "";
            _tracksData = new List<List<TemporaryTrackNode>>();
        }

        /// <summary>
        /// Builds the tracks from stored data.
        /// </summary>
        /// <returns>List of builded tracks.</returns>
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
                        track.Add(tmp.BusStopName, tmp.Hour);
                    }
                    catch { }
                }
                tracks.Add(new Track(track,list.First().DayType));
            }

            return tracks;
        }

        /// <summary>
        /// Sorts stored data for correct building process.
        /// </summary>
        private void Sort()
        {
            TimeOfArrival tmp = new TimeOfArrival(0,0);
            List<TemporaryTrackNode> o = new List<TemporaryTrackNode>();
            List<List<TemporaryTrackNode>> _tracksData2 = new List<List<TemporaryTrackNode>>();
            do
            {
                foreach (List<TemporaryTrackNode> t in _tracksData)
                {
                    if (t.First().Hour >= tmp)
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

        /// <summary>
        /// Adds the list of nodes.
        /// </summary>
        /// <param name="list">The list of nodes.</param>
        public void AddListOfNodes(List<TemporaryTrackNode> list)
        {
            foreach (TemporaryTrackNode tmp in list)
            {
                AddNode(tmp);
            }
            Sort();
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void AddNode(TemporaryTrackNode node)
        {
            if (_tracksData.Count == 0)
            {
                DayType = node.DayType;
                Line = node.BusLineNumber;
            }
            int bestPosition = -1;
            for ( int i = _tracksData.Count-1; i >= 0 ; --i )
            {
                TemporaryTrackNode tmp = _tracksData.ElementAt(i).Last();

                if (node.DayType.Equals(DayType) &&
                    node.BusLineNumber.Equals(tmp.BusLineNumber) &&
                    !node.BusStopName.Equals(tmp.BusStopName))
                {
                    // Hour of node must be later than tmp hour, but not more than 14 minutes! (example value)
                    if (node.Hour > tmp.Hour && node.Hour < tmp.Hour + new TimeOfArrival(0,14) &&
                        node.Letter.Equals(tmp.Letter))
                    {
                        bestPosition = i;
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
