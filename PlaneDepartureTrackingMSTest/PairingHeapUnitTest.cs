using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PlaneDepartureTracking.Model;
using PlaneDepartureTracking.Utils;
using System.Collections.Generic;
using System.IO;
namespace PlaneDepartureTrackingMSTest
{
    [TestClass]
    public class PairingHeapUnitTest
    {
        [TestMethod]
        public void TestAllOperationsOnRandomNumbers()
        {
            int count = 100000; //MIN IS 100
            int idCounter = 1;
            List<Plane> planes = new List<Plane>(count);
            List<Plane> removed = new List<Plane>(count);
            PairingHeap<Plane, int> queue = new PairingHeap<Plane, int>();
            Random random = new Random();
            int addCount = 0, succAddCount = 0,
                changePriorityCount = 0, succChangePriorityCount = 0,
                deleteCount = 0, succDeleteCount = 0,
                compareCheck = 0;


            for (int i = 0; i < count; i++)
            {
                int number = random.Next(0, 100 * count);
                if (number % 3 == 0)
                {
                    addCount++;
                    Plane newPlane = new Plane();
                    newPlane.SetInternationalID("id" + idCounter);
                    idCounter++;
                    newPlane.SetPriority(random.Next(11));
                    if (queue.Add(newPlane))
                    {
                        succAddCount++;
                        planes.Add(newPlane);
                    }
                }
                else
                    if (number % 3 == 1)
                {
                    changePriorityCount++;
                    if (planes.Count > 0)
                    {
                        Assert.IsTrue(queue.ChangePriority(planes[random.Next(0, planes.Count)],
                            random.Next(11)));
                        succChangePriorityCount++;
                    }
                    if (removed.Count > 0)
                    {
                        Assert.IsFalse(queue.ChangePriority(removed[random.Next(0, removed.Count)],
                            random.Next(11)));
                    }

                }
                else
                    if (number % 3 == 2)
                {
                    deleteCount++;
                    if (planes.Count > 0)
                    {
                        Plane toDelete = planes[random.Next(0, planes.Count)];
                        queue.Delete(toDelete);
                        succDeleteCount++;
                        planes.Remove(toDelete);
                        removed.Add(toDelete);
                    }
                    if (removed.Count > 0)
                    {
                        queue.Delete(removed[random.Next(0, removed.Count)]); // SHOULD DO NOTHING
                    }
                }/*
                if (number % (count / 100) == 0)
                {
                    compareCheck++;
                    foreach (Plane plane in planes)
                    {
                        Assert.IsTrue(queue.Contains(num));
                    }
                    foreach (int num in removed)
                    {
                        Assert.IsFalse(tree.Contains(num));
                    }
                    Assert.IsTrue(tree.CheckTreeStructure());
                }*/
            }


            String treeTraversalLevelOrder = queue.TraverseLevelOrder();
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\pairingheapTestLevelOrder.txt",
                treeTraversalLevelOrder);
            String treeTraversalInOrder = queue.TraverseInOrder();
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\pairingheapTestInOrder.txt",
                treeTraversalInOrder);
            String checkInfo = "ADD checks: " + addCount + "\r\nSuccessfully added elements: " + succAddCount +
                "\r\nCHANGE PRIORITY checks: " + changePriorityCount + "\r\nSuccessfully changed priorities: " + succChangePriorityCount +
                "\r\nDELETE checks: " + deleteCount + "\r\nSuccessfully deleted elements: " + succDeleteCount +
                "\r\nTree-List comparisons: " + compareCheck;
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\pairingheapTestStats.txt",
                checkInfo);
        }

    }
}
