using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PlaneDepartureTracking;
using PlaneDepartureTracking.Utils;

namespace PlaneDepartureTrackingMSTest
{
    [TestClass]
    public class SplayTreeUnitTest
    {
        [TestMethod]
        public void TestAdd()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            SplayTree<int> tree = new SplayTree<int>();

            foreach(int number in numbers) {
                tree.Add(number);
            }

            foreach (int number in numbers)
            {
                Assert.IsTrue(tree.Contains(number));
            }
        }
    }
}
