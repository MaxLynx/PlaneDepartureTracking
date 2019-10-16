using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    public class TreeNode<T>: IComparable<T>
        where T : IComparable<T>
    {
        public T Data { set; get; }
        public TreeNode<T> Left { set; get; }
        public TreeNode<T> Right { set; get; }

        public TreeNode<T> Parent { set; get; }

        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }
    }
}
