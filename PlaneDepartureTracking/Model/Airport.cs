using PlaneDepartureTracking.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<Track> TrackList { get; set; }
        SplayTree<Plane> WaitingPlanes { get; set; }

        private List<double> TrackTypesLengthList { get; set; }

        private int waitingCount = 0;

        private DateTime beginOfTimes = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);



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
            TrackList = new List<Track>();
            TrackTypesLengthList = new List<double>();

            TrackType type1 = GenerateTrackType(1500);
            AddTrack("track 1", type1);
            AddTrack("track 2", type1);
            AddTrack("track 10", type1);
            AddTrack("track 11", type1);
            AddTrack("track 12", type1);
            AddTrack("track 13", type1);
            AddTrack("track 14", type1);
            AddTrack("track 15", type1);
            AddTrack("track 16", type1);
            AddTrack("track 17", type1);
            AddTrack("track 18", type1);
            AddTrack("track 19", type1);

            TrackType type2 = GenerateTrackType(2000);
            AddTrack("track 3", type2);
            AddTrack("track 4", type2);
            AddTrack("track 9", type2);
            AddTrack("track 20", type2);
            AddTrack("track 21", type2);
            AddTrack("track 22", type2);
            AddTrack("track 23", type2);
            AddTrack("track 25", type2);
            AddTrack("track 26", type2);
            AddTrack("track 27", type2);
            AddTrack("track 28", type2);
            AddTrack("track 29", type2);


            TrackType type3 = GenerateTrackType(2500);
            AddTrack("track 5A", type3);
            AddTrack("track 5B", type3);
            AddTrack("track 5C", type3);
            AddTrack("track 50", type3);
            AddTrack("track 51", type3);
            AddTrack("track 52", type3);
            AddTrack("track 53", type3);
            AddTrack("track 55", type3);
            AddTrack("track 56", type3);
            AddTrack("track 58", type3);
            AddTrack("track 59", type3);


            TrackType type4 = GenerateTrackType(3000);
            AddTrack("track 6", type4);
            AddTrack("track 60", type4);
            

            TrackType type5 = GenerateTrackType(3500);
            AddTrack("track 7", type5);
            AddTrack("track 8", type5);
            AddTrack("track X", type5);
            AddTrack("track 70", type5);
            AddTrack("track 71", type5);
            AddTrack("track 72", type5);
            AddTrack("track 73", type5);
            AddTrack("track 74", type5);
            AddTrack("track 75", type5);
            AddTrack("track 76", type5);
            AddTrack("track 77", type5);
            AddTrack("track 78", type5);
            AddTrack("track 80", type5);
            AddTrack("track 81", type5);
            AddTrack("track 82", type5);
            AddTrack("track 83", type5);
            AddTrack("track 84", type5);
            AddTrack("track 85", type5);
            AddTrack("track 86", type5);
            AddTrack("track 87", type5);
            AddTrack("track 88", type5);

        }

        private TrackType GenerateTrackType(double length)
        {
            TrackType trackType = new TrackType(length);
            TrackTypes.Add(trackType);
            TrackTypesLengthList.Add(length);
            return trackType;
        }

        private void AddTrack(String name, TrackType trackType)
        {
            Track track = new Track(name, trackType);
            trackType.Tracks.Add(track);
            Tracks.Add(track);
            TrackList.Add(track);
        }

        public String[] GetTrackNames()
        {
            String[] names = new string[TrackList.Count];
            for(int i = 0; i < names.Length; i++)
            {
                names[i] = TrackList[i].GetName();
            }
            return names;
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
                planeFound.Data.SetArrivalTime(SystemTime);
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

        public bool NotifyTrackRequirement(String internationalID, double priority)
        {
            Plane planeToSearch = new Plane(internationalID);
            TreeNode<Plane> planeFound = ArrivedPlanes.Find(planeToSearch);
            if (planeFound != null)
            {
                planeFound.Data.SetTrackRequirementTime(SystemTime);
                ArrivedPlanes.Delete(planeFound.Data);
                planeFound.Data.SetPriority(priority + (SystemTime.ToUniversalTime() - beginOfTimes).TotalSeconds * 1.0 / 10000000000);
                WaitingPlanes.Add(planeFound.Data);
                waitingCount++;

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
                            TrackAllocations.Add("plane ID" + planeFound.Data.GetInternationalID() + " to " + track.GetName()
                                 );
                            break;
                        }
                    }
                    if(planeFound.Data.Track == null && waitingCount / TrackList.Count > 5)
                    {
                        int upperTypeIndex = TrackTypesLengthList.IndexOf(typeFound.Data.Length) + 1;
                        if (upperTypeIndex < TrackTypesLengthList.Count)
                        {
                            TrackType upperType = TrackTypes.Find(new TrackType(TrackTypesLengthList[upperTypeIndex])).Data;
                            foreach (Track track in upperType.Tracks)
                            {
                                if (track.GetPlane() == null)
                                {
                                    planeFound.Data.Track = track;
                                    track.SetPlane(planeFound.Data);
                                    TrackAllocations.Add("!!! plane ID" + planeFound.Data.GetInternationalID() + " to " + track.GetName()
                                         );
                                    break;
                                }
                            }
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

        public bool NotifyDeparture(String internationalID)
        {
            Plane planeToSearch = new Plane(internationalID);
            TreeNode<Plane> planeFound = WaitingPlanes.Find(planeToSearch);
            if (planeFound != null && planeFound.Data.Track != null)
            {

                planeFound.Data.SetDepartureTime(SystemTime);
                PlaneDepartures.Add("plane ID" + planeFound.Data.GetInternationalID() + " arrived from the track " 
                    + planeFound.Data.Track.GetName() + " " + planeFound.Data.GetDepartureTime().ToString());
                TrackType type = planeFound.Data.Track.GetLengthType();
                planeFound.Data.Track.DepartureHistory.Add(new PlaneShortInfo(planeFound.Data.GetInternationalID(),
                    planeFound.Data.GetDepartureTime()));
                planeFound.Data.Track.SetPlane(null);
                planeFound.Data.Track = null;
                WaitingPlanes.Delete(planeFound.Data);


                foreach (Track track in type.Tracks)
                {
                    if (track.GetPlane() == null && type.WaitingPlanes.Root != null)
                    {
                        Plane plane = type.WaitingPlanes.DeleteMin();
                        plane.Track = track;
                        track.SetPlane(plane);
                        TrackAllocations.Add("plane ID" + plane.GetInternationalID() + " to the track " + track.GetName());
                        type.WaitingPlanesForSearch.Delete(plane);
                    }
                    
                    if (waitingCount / TrackList.Count > 5)
                    {
                        int upperTypeIndex = TrackTypesLengthList.IndexOf(type.Length) + 1;
                        if (upperTypeIndex < TrackTypesLengthList.Count)
                        {
                            TrackType upperType = TrackTypes.Find(new TrackType(TrackTypesLengthList[upperTypeIndex])).Data;
                            foreach (Track tr in upperType.Tracks)
                            {
                                if (tr.GetPlane() == null)
                                {
                                    planeFound.Data.Track = tr;
                                    tr.SetPlane(planeFound.Data);
                                    TrackAllocations.Add("!!! plane ID" + planeFound.Data.GetInternationalID() + " to " + tr.GetName()
                                         );
                                    break;
                                }
                            }
                        }
                        
                    }
                }

                waitingCount--;
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

        public bool ChangePlanePriority(String id, double priority)
        {
            TreeNode<Plane> plane = WaitingPlanes.Find(new Plane(id));
            if (plane != null)
            {
                TrackType typeToSearch = new TrackType(plane.Data.GetMinimalTrackLength());
                TreeNode<TrackType> type = TrackTypes.Find(typeToSearch);
                type.Data.WaitingPlanes.ChangePriority(plane.Data, priority);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemovePlaneFromWaiting(String id)
        {
            TreeNode<Plane> plane = WaitingPlanes.Find(new Plane(id));
            if (plane != null)
            {
                ArrivedPlanes.Add(plane.Data);
                plane.Data.SetPriority(0);
                WaitingPlanes.Delete(plane.Data);
                if (plane.Data.Track != null)
                {

                    plane.Data.Track.SetPlane(null);
                    plane.Data.Track = null;

                }
                else
                {
                    TrackType typeToSearch = new TrackType(plane.Data.GetMinimalTrackLength());
                    TreeNode<TrackType> type = TrackTypes.Find(typeToSearch);
                    type.Data.WaitingPlanes.Delete(plane.Data);
                    type.Data.WaitingPlanesForSearch.Delete(plane.Data);
                }
                TrackAllocations.Add("plane ID" + plane.Data.GetInternationalID() + " removed from waiting queue");
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

        public List<String[]> OutputTrackDepartureHistories()
        {
            List<String[]> result = new List<String[]>();
            foreach(Track track  in TrackList)
            {
                result.Add(new string[5]
                    {
                    track.GetName(),
                                        
                    "---",
                    "---",
                    "---",
                    "---"
                    });
                foreach (PlaneShortInfo plane in track.DepartureHistory)
                {
                    Plane planeFullInfo = Planes.Find(new Plane(plane.ID)).Data;
                    result.Add(new string[5]
                    {
                    track.GetName(),
                    plane.ID,
                    planeFullInfo.GetProducerName(),
                    planeFullInfo.GetPlaneType(),
                    plane.DepartureTime.ToString()
                    });
                }
            }
            return result;
        }

        public void GenerateActiveState(int opsCount)
        {
            List<String> arrived = new List<String>();
            List<String> allocated = new List<String>();
            List<String> departured = new List<String>();
            for(int i = 0; i < opsCount; i++)
            {
                if(i % 7 == 0)
                {   // Count -1 to test upper type sub
                    Planes.Add(new Plane("Producer " + i, "Type " + i, "" + i, TrackTypesLengthList[i % TrackTypesLengthList.Count],
                     SystemTime, 0));
                    if (NotifyArrival("" + i))
                    {
                        arrived.Add("" + i);
                    }
                }
                else
                    if (i % 7 == 1 && departured.Count > 0)
                {
                    String randomID = departured[i % departured.Count];
                    if (NotifyArrival(randomID))
                    {
                        departured.Remove(randomID);
                        arrived.Add(randomID);
                    }
                }
                else
                    if ((i % 7 == 2 || i % 7 == 3 || i % 7 == 6) && allocated.Count > 0)
                {
                    String randomID = allocated[i % allocated.Count];
                    if (NotifyDeparture(randomID))
                    {
                        allocated.Remove(randomID);
                        departured.Add(randomID);
                    }
                }
                else
                    if ((i % 7 == 4 || i % 7 == 5) && arrived.Count > 0)
                {
                    String randomID = arrived[i % arrived.Count];
                    if (NotifyTrackRequirement(randomID, i % 11))
                    {
                        arrived.Remove(randomID);
                        allocated.Add(randomID);
                    }
                }
                SystemTime = SystemTime.AddMinutes(10);
            }
        }

        public void SaveToFile()
        {
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\SystemTime.txt",
                SystemTime.ToString());

            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\Planes.txt",
                Planes.TraverseLevelOrder());

            StringBuilder trackAllocations = new StringBuilder();
            foreach(String allocation in TrackAllocations)
            {
                trackAllocations.Append(allocation + ",");
            }
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\TrackAllocations.txt",
                trackAllocations.ToString());

            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\ArrivedPlanes.txt", 
                ArrivedPlanes.TraverseIDsLevelOrder());

            StringBuilder planeArrivals = new StringBuilder();
            foreach (String arrival in PlaneArrivals)
            {
                planeArrivals.Append(arrival + ",");
            }
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\PlaneArrivals.txt",
                planeArrivals.ToString());

            StringBuilder planeDepartures = new StringBuilder();
            foreach (String departure in PlaneDepartures)
            {
                planeDepartures.Append(departure + ",");
            }
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\PlaneDepartures.txt",
                planeDepartures.ToString());

            StringBuilder trackString = new StringBuilder();
            foreach (Track track in TrackList)
            {
                if (track.GetPlane() != null)
                {
                    trackString.Append(track.GetName() + ":" + track.GetPlane().GetInternationalID() + ",");
                }
            }
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\TrackPlanes.txt",
                trackString.ToString());

            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\WaitingPlanes.txt",
                            WaitingPlanes.TraverseIDsLevelOrder());
            
            foreach(double length in TrackTypesLengthList) {

                TrackType type = TrackTypes.Find(new TrackType(length)).Data;

                File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\" +
                    type.Length
                    + "WaitingPlanes.txt",
                            type.WaitingPlanes.TraverseIDsLevelOrder());

                File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\" +
                   type.Length
                   + "WaitingPlanesForSearch.txt",
                           type.WaitingPlanesForSearch.TraverseIDsLevelOrder());

                foreach (Track track in type.Tracks) {
                    StringBuilder history = new StringBuilder();
                    foreach (PlaneShortInfo info in track.DepartureHistory)
                    {
                        history.Append(info.ID + "," + info.DepartureTime + "\n");
                    }
                    File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\" +
                        track.GetName() +
                        "History.txt",
                        history.ToString());
                }
            }

            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\WaitingCount.txt",
                "" + waitingCount);

        }

        public void ReadFromFile()
        {
            SystemTime = DateTime.Parse(File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\SystemTime.txt"));

            
            String[] planes = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                "\\PlaneDepartureTrackingMSTest\\Planes.txt").Split('\n');
            int test = 0;
            for (int i = 0; i < planes.Length; i++)
            {
                String [] row = planes[i].Split(',');
                if (row.Length >= 6)
                {
                    test++;
                    Plane plane = new Plane(row[0], row[1], row[2],
                        Double.Parse(row[3]),
                        DateTime.Parse(row[4]),
                        Double.Parse(row[6].Replace('.', ',')));
                    plane.SetTrackRequirementTime(DateTime.Parse(row[5]));
                    Planes.Add(plane);
                }
            }
            System.Windows.Forms.MessageBox.Show("" + test);
            

            String[] trackAllocations = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\TrackAllocations.txt").Split(',');
            for (int i = 0;   i < trackAllocations.Length; i++)
            {
                String allocation = trackAllocations[i];
                TrackAllocations.Add(allocation);
            }

            ArrivedPlanes.SplayDisabled = true;
            String[] arrivedPlanes = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                    "\\PlaneDepartureTrackingMSTest\\ArrivedPlanes.txt").Split();
            foreach(String planeID in arrivedPlanes)
            {
                TreeNode<Plane> plane = Planes.Find(new Plane(planeID));
                if (plane != null)
                {
                    ArrivedPlanes.Add(plane.Data);
                }
            }
            ArrivedPlanes.SplayDisabled = false;

            String[] planeArrivals = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\PlaneArrivals.txt").Split(',');
            foreach (String arrival in planeArrivals)
            {
                PlaneArrivals.Add(arrival);
            }

            String[] planeDepartures = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\PlaneDepartures.txt").Split(',');
            foreach (String departure in planeDepartures)
            {
                PlaneDepartures.Add(departure);
            }
            
            String[] pairs = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                    "\\PlaneDepartureTrackingMSTest\\TrackPlanes.txt").Split(',');
            foreach (String pair in pairs)
            {
                String[] splittedPair = pair.Split(':');
                if (splittedPair.Length == 2)
                {
                    String trackName = splittedPair[0];
                    String planeID = splittedPair[1];
                    TreeNode<Plane> plane = Planes.Find(new Plane(planeID));
                    TreeNode<Track> track = Tracks.Find(new Track(trackName));
                    if (plane != null && track != null)
                    {
                        plane.Data.Track = track.Data;
                        track.Data.SetPlane(plane.Data);
                    }
                }
            }
                

            WaitingPlanes.SplayDisabled = true;
            String[] waitingPlanes = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                    "\\PlaneDepartureTrackingMSTest\\WaitingPlanes.txt").Split();
            foreach (String planeID in waitingPlanes)
            {
                TreeNode<Plane> plane = Planes.Find(new Plane(planeID));
                if (plane != null)
                {
                    WaitingPlanes.Add(plane.Data);
                }
            }
            WaitingPlanes.SplayDisabled = false;

            foreach (double length in TrackTypesLengthList)
            {

                TreeNode<TrackType> type = TrackTypes.Find(new TrackType(length));

                if (type != null)
                {

                    type.Data.WaitingPlanesForSearch.SplayDisabled = true;
                    String[] waitingPlanesForSearchTrack = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                        "\\PlaneDepartureTrackingMSTest\\" +
                        type.Data.Length
                        + "WaitingPlanesForSearch.txt").Split();
                    foreach (String planeID in waitingPlanesForSearchTrack)
                    {
                        TreeNode<Plane> plane = Planes.Find(new Plane(planeID));
                        if (plane != null)
                        {
                            type.Data.WaitingPlanesForSearch.Add(plane.Data);
                        }
                    }
                    type.Data.WaitingPlanesForSearch.SplayDisabled = false;

                    String[] waitingPlanesTrack = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                        "\\PlaneDepartureTrackingMSTest\\" +
                        type.Data.Length
                        + "WaitingPlanes.txt").Split();
                    foreach (String planeID in waitingPlanesTrack)
                    {
                        TreeNode<Plane> plane = Planes.Find(new Plane(planeID));
                        if (plane != null)
                        {
                            type.Data.WaitingPlanes.Add(plane.Data);
                        }
                    }
                    foreach(Track track in type.Data.Tracks)
                    {
                        String[] trackHistory = File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                            "\\PlaneDepartureTrackingMSTest\\" +
                        track.GetName() +
                        "History.txt").Split('\n');

                        foreach (String entry in trackHistory)
                        {
                            String[] splittedEntry = entry.Split(',');
                            if (splittedEntry.Length == 2)
                            {
                                track.DepartureHistory.Add(new PlaneShortInfo(splittedEntry[0],
                                    DateTime.Parse(splittedEntry[1])));
                            }
                        }
                    }
                }
            }

            waitingCount = Int32.Parse(File.ReadAllText("D:\\.NET Projects\\PlaneDepartureTracking" +
                    "\\PlaneDepartureTrackingMSTest\\WaitingCount.txt"));
                
        }

    }
}
