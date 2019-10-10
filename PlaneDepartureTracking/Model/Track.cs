using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    class Track
    {
        private double length;

        public Track() { }

        public Track(double length)
        {
            this.length = length;
        }

        public double GetLength()
        {
            return length;
        }

        public void SetLength(double length)
        {
            this.length = length;
        }
    }
}
