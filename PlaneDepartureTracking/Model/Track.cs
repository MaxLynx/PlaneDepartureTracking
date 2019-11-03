using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    public class Track : IComparable<Track>, Utils.IIDRetrieval
    {
        private String name;
        private TrackType lengthType;
        private Plane plane;

        public List<PlaneShortInfo> DepartureHistory { get; set; }

        public Track() {
            DepartureHistory = new List<PlaneShortInfo>();
        }

        public Track(String name)
        {
            DepartureHistory = new List<PlaneShortInfo>();
            this.name = name;
        }

        public Track(String name, TrackType lengthType)
        {
            DepartureHistory = new List<PlaneShortInfo>();
            this.name = name;
            this.lengthType = lengthType;
        }

        public String GetName()
        {
            return name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public TrackType GetLengthType()
        {
            return lengthType;
        }

        public void SetLengthType(TrackType lengthType)
        {
            this.lengthType = lengthType;
        }

        public Plane GetPlane()
        {
            return plane;
        }

        public void SetPlane(Plane plane)
        {
            this.plane = plane;
        }

        public int CompareTo(Track other)
        {            
            return this.GetName().CompareTo(other.GetName());
        }

        public string GetInternationalID()
        {
            throw new NotImplementedException();
        }
    }
}
