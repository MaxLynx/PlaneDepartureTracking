using PlaneDepartureTracking.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    public class Airport
    {
        SplayTree<Plane> Planes { get; set; }
        SplayTree<String> TrackAllocations { get; set; }

        SplayTree<Plane> ArrivedPlanes { get; set; }

        SplayTree<String> PlaneArrivals { get; set; }

        SplayTree<Plane> PlaneDepartures { get; set; }

        SplayTree<TrackType> TrackTypes { get; set; }

        SplayTree<Plane> WaitingPlanes { get; set; }

        /*
         * Constructor substitution as for now
         */
        public Airport()
        {
            Planes = new SplayTree<Plane>();
            TrackAllocations = new SplayTree<String>();
            ArrivedPlanes = new SplayTree<Plane>();
            PlaneArrivals = new SplayTree<String>();
            PlaneDepartures = new SplayTree<Plane>();
            TrackTypes = new SplayTree<TrackType>();
            WaitingPlanes = new SplayTree<Plane>();

            TrackTypes.Add(new TrackType(1500));
            TrackTypes.Add(new TrackType(2000));
            TrackTypes.Add(new TrackType(2500));
        }


        /*
         * Returns true if plane id is already in system database
         */
        public bool NotifyArrival(String internationalID)
        {
            Plane planeToSearch = new Plane(internationalID);
            TreeNode<Plane> planeFound = Planes.Find(planeToSearch);
            if(planeFound == null)
            {
                return false;
            }
            else
            {
                planeFound.Data.SetArrivalTime(DateTime.Now);
                ArrivedPlanes.Add(planeFound.Data);
                return true;
            }
        }

        public void AddNewPlane(String producerName, String type, String internationalID, double minimalTrackLength)
        {
            Plane plane = new Plane(producerName, type, internationalID, minimalTrackLength, DateTime.Now, 0);
            Planes.Add(plane);
            ArrivedPlanes.Add(plane);
            PlaneArrivals.Add("plane " + plane.GetInternationalID() + " arrived " + plane.GetArrivalTime());
        }

        public bool NotifyTrackRequirement(String internationalID, int priority)
        {
            Plane planeToSearch = new Plane(internationalID);
            TreeNode<Plane> planeFound = ArrivedPlanes.Find(planeToSearch);
            if (planeFound != null)
            {
                planeFound.Data.SetTrackRequirementTime(DateTime.Now);
                ArrivedPlanes.Delete(planeFound.Data);
                planeFound.Data.SetPriority(priority);
                WaitingPlanes.Add(planeFound.Data);

                TrackType typeToSearch = new TrackType(planeFound.Data.GetMinimalTrackLength());
                TreeNode<TrackType> typeFound = TrackTypes.Find(typeToSearch);
                if(typeFound == null)
                {
                    return false;
                }
                else
                {
                    typeFound.Data.WaitingPlanes.Add(planeFound.Data);
                    typeFound.Data.WaitingPlanesForSearch.Add(planeFound.Data);
                }
                return true;
            }
            return false;
        }

        public List<String> OutputWaitingPlanes()
        {
            return WaitingPlanes.TraverseInOrderAsStringList();
        }

    }
}
