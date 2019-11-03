using PlaneDepartureTracking.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    public class TrackType : IComparable<TrackType>, Utils.IIDRetrieval
    {
        public double Length { get; set; }

        public List<Track> Tracks { get; set; }

        public PairingHeap<Plane, double> WaitingPlanes { get; set; }

        public SplayTree<Plane> WaitingPlanesForSearch { get; set; }

        public TrackType(double length)
        {
            Length = length;
            Tracks = new List<Track>();
            WaitingPlanes = new PairingHeap<Plane, double>();
            WaitingPlanesForSearch = new SplayTree<Plane>();
        }

        public int CompareTo(TrackType other)
        {
            if(this.Length == other.Length)
            {
                return 0;
            }
            return this.Length > other.Length ? 1 : - 1;
        }

        public string GetInternationalID()
        {
            throw new NotImplementedException();
        }
    }
}
