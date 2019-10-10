using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    class Plane
    {
        private String producerName;
        private String planeType;
        private String internationalID;
        private double minimalTrackLength;
        private DateTime arrivalTime;
        private DateTime trackRequirementTime;
        private DateTime departureTime;
        private int priority;

        public Plane() { }

        public Plane(String producerName, String type, String internationalID, double minimalTrackLength,
                     DateTime arrivalTime, DateTime trackRequirementTime, int priority)
        {
            this.producerName = producerName;
            this.planeType = type;
            this.internationalID = internationalID;
            this.minimalTrackLength = minimalTrackLength;
            this.arrivalTime = arrivalTime;
            this.trackRequirementTime = trackRequirementTime;
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

        public int GetPriority()
        {
            return priority;
        }

        public void SetPriority(int priority)
        {
            this.priority = priority;
        }
    }
}
