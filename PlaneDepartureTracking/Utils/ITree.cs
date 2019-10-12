using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    interface ITree<T>
    where T : IComparable<T>

    {
        void Add(T el);

        void Delete(T el);

        TreeNode<T> Find(T el);

        bool Contains(T el);
    }
}
