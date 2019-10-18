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
        public void TestAllOperations()
        {
            int count = 1000; //MIN IS 100
            List<int> numbers = new List<int>(count);
            List<int> removed = new List<int>(count);
            SplayTree<int> tree = new SplayTree<int>();
            Random random = new Random();
            int addCount = 0, succAddCount = 0,
                findCount = 0, succFindCount = 0,
                deleteCount = 0, succDeleteCount = 0,
                compareCheck = 0;


            for (int i = 0; i < count; i++)
            {
                int number = random.Next(0, 100 * count);
                if (number % 3 == 0)
                {
                    addCount++;
                    if (tree.Add(number))
                    {
                        succAddCount++;
                        numbers.Add(number);
                        if (removed.Contains(number))
                        {
                            removed.Remove(number);
                        }
                    }
                }
                else
                    if (number % 3 == 1)
                {
                    findCount++;
                    if (numbers.Count > 0)
                    {
                        Assert.IsTrue(tree.Contains(numbers[random.Next(0, numbers.Count)]));
                        succFindCount++;
                    }
                    if (removed.Count > 0)
                    {
                        Assert.IsFalse(tree.Contains(removed[random.Next(0, removed.Count)]));
                    }
                    
                }
                else
                    if (number % 3 == 2)
                {
                    deleteCount++;
                    if (numbers.Count > 0)
                    {
                        int toDelete = numbers[random.Next(0, numbers.Count)];
                        tree.Delete(toDelete);
                        succDeleteCount++;
                        numbers.Remove(toDelete);
                        removed.Add(toDelete);
                    }
                    if (removed.Count > 0)
                    {
                        tree.Delete(removed[random.Next(0, removed.Count)]); // SHOULD DO NOTHING
                    }
                }
                if(number % (count / 100) == 0)
                {
                    compareCheck++;
                    foreach(int num in numbers)
                    {
                        Assert.IsTrue(tree.Contains(num));
                    }
                    foreach (int num in removed)
                    {
                        Assert.IsFalse(tree.Contains(num));
                    }
                    Assert.IsTrue(tree.CheckTreeStructure());
                }
            }


            String treeTraversalLevelOrder = tree.TraverseLevelOrder();
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\splaytreeTestLevelOrder.txt", 
                treeTraversalLevelOrder);
            String treeTraversalInOrder = tree.TraverseInOrder();
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\splaytreeTestInOrder.txt",
                treeTraversalInOrder);
            String checkInfo = "ADD checks: " + addCount + "\r\nSuccessfully added elements: " + succAddCount +
                "\r\nFIND checks: " + findCount + "\r\nSuccessfully found elements: " + succFindCount +
                "\r\nDELETE checks: " + deleteCount + "\r\nSuccessfully deleted elements: " + succDeleteCount +
                "\r\nTree-List comparisons: " + compareCheck;
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\testStats.txt",
                checkInfo);
        }
    }
}
