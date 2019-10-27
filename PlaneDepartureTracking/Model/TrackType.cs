using PlaneDepartureTracking.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    public class TrackType : IComparable<TrackType>
    {
        public double Length { get; set; }

        public SplayTree<Track> Tracks { get; set; }

        public PairingHeap<Plane, int> WaitingPlanes { get; set; }

        public SplayTree<Plane> WaitingPlanesForSearch { get; set; }

        public TrackType(double length)
        {
            Length = length;
        }

        public int CompareTo(TrackType other)
        {
            if(this.Length == other.Length)
            {
                return 0;
            }
            return this.Length > other.Length ? 1 : - 1;
        }
    }
}
