using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Model
{
    public class PlaneShortInfo
    {
        public String ID { set; get; }
        public DateTime DepartureTime { set; get; }

        public PlaneShortInfo(String id, DateTime departureTime)
        {
            ID = id;
            DepartureTime = departureTime;
        }
    }
}
