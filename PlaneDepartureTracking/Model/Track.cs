using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    public class Track
    {
        private String name;
        private double length;
        private Plane plane;

        public Track() { }

        public Track(String name, double length, Plane plane)
        {
            this.name = name;
            this.length = length;
            this.plane = plane;
        }

        public String GetName()
        {
            return name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public double GetLength()
        {
            return length;
        }

        public void SetLength(double length)
        {
            this.length = length;
        }

        public Plane GetPlane()
        {
            return plane;
        }

        public void SetPlane(Plane plane)
        {
            this.plane = plane;
        }
    }
}
