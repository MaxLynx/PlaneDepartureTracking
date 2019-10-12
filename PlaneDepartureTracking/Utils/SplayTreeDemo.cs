using PlaneDepartureTracking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    class SplayTreeDemo
    {
        private static SplayTree<int> SplayTree { set; get; }
        private static Random Random { set; get; }

        public static void Test()
        {
            Random = new Random();
            SplayTree = new SplayTree<int>();

            for(int i = 0; i < 10000; i++)
            {
                SplayTree.Add(Random.Next(0, 100000000));
            }

            System.Diagnostics.Debug.WriteLine("SplayTreeDemo Main method done");
        }
    }
}
