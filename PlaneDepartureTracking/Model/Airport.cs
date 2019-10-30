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
        public DateTime SystemTime { get; set; }
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
            SystemTime = DateTime.Now;
            Planes = new SplayTree<Plane>();
            TrackAllocations = new SplayTree<String>();
            ArrivedPlanes = new SplayTree<Plane>();
            PlaneArrivals = new SplayTree<String>();
            PlaneDepartures = new SplayTree<Plane>();
            TrackTypes = new SplayTree<TrackType>();
            WaitingPlanes = new SplayTree<Plane>();


            TrackType type1 = new TrackType(1500);
            type1.Tracks.Add(new Track("track 1", type1));
            type1.Tracks.Add(new Track("track 2", type1));
            TrackType type2 = new TrackType(2000);
            type2.Tracks.Add(new Track("track 3", type2));
            type2.Tracks.Add(new Track("track 4", type2));
            TrackType type3 = new TrackType(2500);
            type3.Tracks.Add(new Track("track 5", type3));

            TrackTypes.Add(type1);
            TrackTypes.Add(type2);
            TrackTypes.Add(type3);
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
                PlaneArrivals.Add("plane " + planeFound.Data.GetInternationalID() + " arrived " + planeFound.Data.GetArrivalTime());
                return true;
            }
        }

        public void AddNewPlane(String producerName, String type, String internationalID, double minimalTrackLength)
        {
            Plane plane = new Plane(producerName, type, internationalID, minimalTrackLength, SystemTime, 0);
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
                planeFound.Data.SetTrackRequirementTime(SystemTime);
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
                    foreach(Track track in typeFound.Data.Tracks)
                    {
                        if (track.GetPlane() == null)
                        {
                            planeFound.Data.Track = track;
                            track.SetPlane(planeFound.Data);
                            TrackAllocations.Add("plane ID" + planeFound.Data.GetInternationalID() + " to the track " + track.GetName());
                            break;
                        }
                    }
                    if (planeFound.Data.Track == null)
                    {
                        typeFound.Data.WaitingPlanes.Add(planeFound.Data);
                        typeFound.Data.WaitingPlanesForSearch.Add(planeFound.Data);
                    }
                }
                return true;
            }
            return false;
        }

        public List<String> OutputWaitingPlanes()
        {
            return WaitingPlanes.TraverseInOrderAsStringList();
        }

        public String[] FindWaitingPlane(String code)
        {
            String[] result = new string[8];
            TreeNode<Plane> plane = WaitingPlanes.Find(new Plane(code));
            if (plane == null)
            {
                result[0] = result[1] = result[3] = result[4] = result[5] = result[6] = result[7] = "";
                result[2] = "NOT FOUND";
            }
            else
            {
                result[0] = plane.Data.GetProducerName();
                result[1] = plane.Data.GetPlaneType();
                result[2] = code;
                result[3] = "" + plane.Data.GetMinimalTrackLength();
                result[4] = plane.Data.GetArrivalTime().ToString();
                result[5] = plane.Data.GetTrackRequirementTime().ToString();
                result[6] = "" + plane.Data.GetPriority();
                if (plane.Data.Track != null)
                {
                    result[7] = plane.Data.Track.GetName();
                }
                else
                {
                    result[7] = "";
                }
            }
            return result;
        }
    }
}
