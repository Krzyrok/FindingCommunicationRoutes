﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    /// <summary>
    /// Represents a single bus stop.
    /// </summary>
    public class BusStop
    {
        public List<Line> LinesPlyingThroughBusStop
        {
            get { return _linesPlyingThroughBusStop; }
        }

        public BusStop(List<Line> LinesPlyingThroughBusStop)
        {
            if (LinesPlyingThroughBusStop != null)
            {
                _linesPlyingThroughBusStop = new List<Line>();
                _linesPlyingThroughBusStop.AddRange(LinesPlyingThroughBusStop);
            }
        }

        private List<Line> _linesPlyingThroughBusStop;
    }
}
