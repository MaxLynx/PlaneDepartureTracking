using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    public interface IPriority<T, V>
        where T : IComparable<T>
        where V : IComparable<V>
    {
        void SetPriority(T priority);
        T GetPriority();
        T GetMaxPriority();

        TreeNode<V> GetHeapNode();
        void SetHeapNode(TreeNode<V> node);

    }
}
