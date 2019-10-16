using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PlaneDepartureTracking;
using PlaneDepartureTracking.Utils;
using System.Collections.Generic;
using System.IO;

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

            foreach (int number in numbers)
            {
                Assert.IsTrue(tree.Contains(number));
            }
        }

        [TestMethod]
        public void TestAddOnTreeStructure()
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

            Assert.IsTrue(tree.ObservedLevelCount <= 2 * Math.Log2(numbers.Count));

            String nodesAsText = tree.TraverseInorder();

            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\splaytree.txt", nodesAsText);
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
            Assert.AreEqual(tree.FindMin(), numbers[0]);

        }


        [TestMethod]
        public void TestFindAndDelete()
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
            }

            
            for (int i = 0; i < 10000; i++)
            {
                int number = random.Next(0, 1500000);
                if (numbers.Contains(number)) //not calling tree method to not invoke Splay 
                {
                    tree.Delete(number);
                    Assert.IsNull(tree.Find(number));
                }
            }


        }
    }
}
