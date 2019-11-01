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
        public List<String> TrackAllocations { get; set; }

        SplayTree<Plane> ArrivedPlanes { get; set; }

        public List<String> PlaneArrivals { get; set; }

        public List<String> PlaneDepartures { get; set; }

        SplayTree<TrackType> TrackTypes { get; set; }
        SplayTree<Track> Tracks { get; set; }
        public List<String> TrackNames { get; set; }
        SplayTree<Plane> WaitingPlanes { get; set; }

        
        public Airport()
        {
            SystemTime = DateTime.Now;
            Planes = new SplayTree<Plane>();
            TrackAllocations = new List<String>();
            ArrivedPlanes = new SplayTree<Plane>();
            PlaneArrivals = new List<String>();
            PlaneDepartures = new List<String>();
            TrackTypes = new SplayTree<TrackType>();
            WaitingPlanes = new SplayTree<Plane>();
            Tracks = new SplayTree<Track>();
            TrackNames = new List<String>();

            TrackType type1 = GenerateTrackType(1500);
            AddTrack("track 1", type1);        
            AddTrack("track 2", type1);
            
            TrackType type2 = GenerateTrackType(2000);
            AddTrack("track 3", type2);
            AddTrack("track 4", type2);
            AddTrack("track 9", type2);

            TrackType type3 = GenerateTrackType(2500);
            AddTrack("track 5A", type3);
            AddTrack("track 5B", type3);
            AddTrack("track 5C", type3);

            TrackType type4 = GenerateTrackType(3000);
            AddTrack("track 6", type4);

            TrackType type5 = GenerateTrackType(3500);
            AddTrack("track 7", type5);
            AddTrack("track 8", type5);
            AddTrack("track X", type5);
        }

        private TrackType GenerateTrackType(double length)
        {
            TrackType trackType = new TrackType(length);
            TrackTypes.Add(trackType);
            return trackType;
        }

        private void AddTrack(String name, TrackType trackType)
        {
            Track track = new Track(name, trackType);
            trackType.Tracks.Add(track);
            Tracks.Add(track);
            TrackNames.Add(name);
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
                            TrackAllocations.Add("plane ID" + planeFound.Data.GetInternationalID() + " to " + track.GetName());
                            break;
                        }
                    }
                    
                        typeFound.Data.WaitingPlanes.Add(planeFound.Data);
                        typeFound.Data.WaitingPlanesForSearch.Add(planeFound.Data);
                    
                }
                return true;
            }
            return false;
        }

        public bool NotifyDeparture(String internationalID)
        {
            Plane planeToSearch = new Plane(internationalID);
            TreeNode<Plane> planeFound = WaitingPlanes.Find(planeToSearch);
            if (planeFound != null)
            {
                /*
                planeFound.Data.SetDepartureTime(SystemTime);
                WaitingPlanes.Delete(planeFound.Data);

                TrackType typeToSearch = new TrackType(planeFound.Data.GetMinimalTrackLength());
                TreeNode<TrackType> typeFound = TrackTypes.Find(typeToSearch);
                if (typeFound == null)
                {
                    return false;
                }
                else
                {
                    foreach (Track track in typeFound.Data.Tracks)
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
                */
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

        public String[] FindWaitingPlane(String code, String track)
        {
            String[] result = new string[8];
            Track trackFound = Tracks.Find(new Track(track)).Data;

            if (trackFound == null)
            {
                return null;
            }
            else
            {
                TreeNode<Plane> plane = trackFound.GetLengthType().WaitingPlanesForSearch.Find(new Plane(code));
                if (plane != null)
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
                    return result;
                }
            }
            
                result[0] = result[1] = result[3] = result[4] = result[5] = result[6] = result[7] = "";
                result[2] = "NOT FOUND";
            
            return result;
        }

        public String ChangePlanePriority(String id, String priority)
        {
            throw new NotImplementedException();
        }

        public bool RemovePlaneFromWaiting(String id)
        {
            TreeNode<Plane> plane = WaitingPlanes.Find(new Plane(id));
            if (plane != null)
            {
                ArrivedPlanes.Add(plane.Data);
                plane.Data.SetPriority(0);
                WaitingPlanes.Delete(plane.Data);
                if(plane.Data.Track != null)
                {

                    plane.Data.Track.GetLengthType().WaitingPlanes.Delete(plane.Data);
                    plane.Data.Track.GetLengthType().WaitingPlanesForSearch.Delete(plane.Data);

                    plane.Data.Track.SetPlane(null);
                    plane.Data.Track = null;
                    TrackAllocations.Add("plane ID" + plane.Data.GetInternationalID() + " removed from waiting queue");
                                
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<String> OutputWaitingPlanes(String track)
        {
            Track trackFound = Tracks.Find(new Track(track)).Data;
                
            if (trackFound == null)
            {
                return null;
            }
            else
            {
                return trackFound.GetLengthType().WaitingPlanesForSearch.TraverseInOrderAsStringList();
            }
        }

    }
}
