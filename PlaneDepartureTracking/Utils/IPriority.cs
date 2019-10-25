using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    public interface IPriority<T>
        where T : IComparable<T>
    {
        void SetPriority(T priority);
        T GetPriority();
        T GetMaxPriority();
    }
}
