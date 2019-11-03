using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model 
{
    public class Plane : IComparable<Plane>, Utils.IPriority<double, Plane>, Utils.IIDRetrieval
    {
        private String producerName;
        private String planeType;
        private String internationalID;
        private double minimalTrackLength;
        private DateTime arrivalTime;
        private DateTime trackRequirementTime;
        private DateTime departureTime;
        private double priority;

        private Utils.TreeNode<Plane> heapNode;

        public Track Track { set; get; }

        public Utils.TreeNode<Plane> GetHeapNode() 
        {
            return heapNode;
        }

        public void SetHeapNode(Utils.TreeNode<Plane> treeNode)
        {
            this.heapNode = treeNode;
        }

        public Plane() { }
        public Plane(String internationalID)
        {
            this.internationalID = internationalID;
        }

        public Plane(String producerName, String type, String internationalID, double minimalTrackLength,
                     DateTime arrivalTime, double priority)
        {
            this.producerName = producerName;
            this.planeType = type;
            this.internationalID = internationalID;
            this.minimalTrackLength = minimalTrackLength;
            this.arrivalTime = arrivalTime;
            this.priority = priority;
        }

        public String GetProducerName()
        {
            return producerName;
        }

        public void SetProducerName(String producerName)
        {
            this.producerName = producerName;
        }

        public String GetPlaneType()
        {
            return planeType;
        }

        public void SetPlaneType(String type)
        {
            this.planeType = type;
        }

        public String GetInternationalID()
        {
            return internationalID;
        }

        public void SetInternationalID(String internationalID)
        {
            this.internationalID = internationalID;
        }

        public double GetMinimalTrackLength()
        {
            return minimalTrackLength;
        }

        public void SetMinimalTrackLength(double minimalTrackLength)
        {
            this.minimalTrackLength = minimalTrackLength;
        }

        public DateTime GetArrivalTime()
        {
            return arrivalTime;
        }

        public void SetArrivalTime(DateTime arrivalTime)
        {
            this.arrivalTime = arrivalTime;
        }

        public DateTime GetTrackRequirementTime()
        {
            return trackRequirementTime;
        }

        public void SetTrackRequirementTime(DateTime trackRequirementTime)
        {
            this.trackRequirementTime = trackRequirementTime;
        }

        public DateTime GetDepartureTime()
        {
            return departureTime;
        }

        public void SetDepartureTime(DateTime departureTime)
        {
            this.departureTime = departureTime;
        }

        public double GetPriority()
        {
            return priority;
        }

        public void SetPriority(double priority)
        {
            this.priority = priority;
        }

        public double GetMaxPriority()
        {
            return -1;
        }

        public int CompareTo(Plane other)
        {
            return this.GetInternationalID().CompareTo(other.GetInternationalID());
        }

        public override String ToString()
        {
            if (Track == null)
            {
                return producerName + "," + planeType + "," + internationalID + "," + minimalTrackLength + "," + arrivalTime +
                    "," + trackRequirementTime + "," + priority.ToString().Replace(',', '.') + ",";
            }
            else
            {
                return producerName + "," + planeType + "," + internationalID + "," + minimalTrackLength + "," + arrivalTime +
                    "," + trackRequirementTime + "," + priority.ToString().Replace(',', '.') + "," + Track.GetName();
            }
        }
    }
}
