using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PlaneDepartureTracking;
using PlaneDepartureTracking.Utils;
using System.Collections.Generic;

namespace PlaneDepartureTrackingMSTest
{
    [TestClass]
    public class SplayTreeUnitTest
    {
        [TestMethod]
        public void TestAddWithNoDuplicates()
        {
            List<int> numbers = new List<int>(10000);

            Random random = new Random();

            for (int i = 0; i < 10000; i++)
            {
                int number = random.Next(0, 1500000);
                if (!numbers.Contains(number))
                    numbers.Add(number);
            }

            SplayTree<int> tree = new SplayTree<int>();

            foreach (int number in numbers)
            {
                tree.Add(number);
            }

            //Assert.IsTrue(tree.ObservedLevelCount < 500);

            foreach (int number in numbers)
            {
                Assert.IsTrue(tree.Contains(number));
            }
        }

        [TestMethod]
        public void TestFindMin()
        {
            List<int> numbers = new List<int>(10000);

            Random random = new Random();

            for (int i = 0; i < 10000; i++)
            {
                int number = random.Next(0, 1500000);
                if (!numbers.Contains(number))
                    numbers.Add(number);
            }

            SplayTree<int> tree = new SplayTree<int>();

            foreach (int number in numbers)
            {
                tree.Add(number);
            }

            numbers.Sort();
            Assert.AreEqual(tree.FindMin(), numbers[numbers.Count - 1]);

        }

        [TestMethod]
        public void TestFind()
        {
            List<int> numbers = new List<int>(10000);

            Random random = new Random();

            for (int i = 0; i < 10000; i++)
            {
                int number = random.Next(0, 1500000);
                if (!numbers.Contains(number))
                    numbers.Add(number);
            }

            SplayTree<int> tree = new SplayTree<int>();

            foreach (int number in numbers)
            {
                tree.Add(number);
                TreeNode<int> node = tree.Find(number);
                Assert.IsTrue(node.Data.Equals(number));
                //tree.Delete(number);
                //Assert.IsNull(tree.Find(number));
            }


        }

    }
}
