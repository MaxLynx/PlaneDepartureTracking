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
            int count = 1000; //MIN IS 100
            int idCounter = 1;
            List<Plane> planes = new List<Plane>(count);
            List<Plane> removed = new List<Plane>(count);
            PairingHeap<Plane, int> queue = new PairingHeap<Plane, int>();
            Random random = new Random();
            int addCount = 0, succAddCount = 0,
                changePriorityCount = 0, succChangePriorityCount = 0,
                deleteCount = 0, succDeleteCount = 0,
                compareCheck = 0;
            String history = "";


            for (int i = 0; i < count; i++)
            {
                int number = random.Next(0, 100 * count);
                if (number % 5 == 0 || number % 5 == 3 || number % 5 == 4)
                {
                    addCount++;
                    Plane newPlane = new Plane();
                    newPlane.SetInternationalID("id" + idCounter);
                    idCounter++;
                    newPlane.SetPriority(random.Next(11));
                    if (queue.Add(newPlane))
                    {
                        succAddCount++;
                        history += "ADDED: " + newPlane.GetInternationalID() + " with priority " + newPlane.GetPriority() + "\r\n";
                        history += "ROOT: " + queue.Root.Data.GetInternationalID() + " with priority " 
                            + queue.Root.Data.GetPriority() + "\r\n";
                        planes.Add(newPlane);
                    }
                }
                                    
                else
                    if (number % 5 == 1 || number % 5 == 2)
                {
                    /*
                    deleteCount++;
                    if (planes.Count > 0)
                    {
                        Plane toDelete = queue.DeleteMin();
                        if (toDelete != null)
                        {
                            succDeleteCount++;
                            planes.Remove(toDelete);
                            removed.Add(toDelete);
                            history += "DELETED: " + toDelete.GetInternationalID() + " with priority " + toDelete.GetPriority() + "\r\n";
                            if (queue.Root != null)
                            {
                                history += "ROOT: " + queue.Root.Data.GetInternationalID() + " with priority "
                                + queue.Root.Data.GetPriority() + "\r\n";
                            }
                            else
                            {
                                history += "EMPTY HEAP\r\n";
                            }
                        }
                    }
                    if (removed.Count > 0)
                    {
                        
                        Plane errorneousPlane = queue.Delete(removed[random.Next(0, removed.Count)]);  // SHOULD DO NOTHING
                        if(errorneousPlane != null)
                        {
                            history += "???DELETED: " + errorneousPlane.GetInternationalID() 
                                + " with priority " + errorneousPlane.GetPriority() + "\r\n";
                            if (queue.Root != null)
                            {
                                history += "ROOT: " + queue.Root.Data.GetInternationalID() + " with priority "
                                + queue.Root.Data.GetPriority() + "\r\n";
                            }
                            else
                            {
                                history += "EMPTY HEAP\r\n";
                            }
                        }
                        
                    }*/
                }
                if (count >= 100 && number % (count / 100) == 0)
                {
                    
                    changePriorityCount++;
                    
                    if (planes.Count > 0)
                    {
                        int newPriority = random.Next(11);
                        int randomIndex = random.Next(0, planes.Count);
                        if (newPriority < planes[randomIndex].GetPriority())
                        {
                            Assert.IsTrue(queue.ChangePriority(planes[randomIndex],
                                newPriority));
                            planes[randomIndex].SetPriority(newPriority);
                            succChangePriorityCount++;
                        }
                    }
                    
                    if (removed.Count > 0)
                    {
                        int newPriority = random.Next(11);
                        int randomIndex = random.Next(0, removed.Count);
                        if (newPriority != removed[randomIndex].GetPriority())
                        {
                            Assert.IsFalse(queue.ChangePriority(removed[randomIndex],
                                newPriority));
                        }
                            
                    }
                    /*
                    Plane toDelete = queue.Delete(planes[random.Next(0, planes.Count)]);
                    if (toDelete != null)
                    {
                        history += "DELETED: " + toDelete.GetInternationalID()
                            + " with priority " + toDelete.GetPriority() + "\r\n";
                        if (queue.Root != null)
                        {
                            history += "ROOT: " + queue.Root.Data.GetInternationalID() + " with priority "
                            + queue.Root.Data.GetPriority() + "\r\n";
                        }
                        else
                        {
                            history += "EMPTY HEAP\r\n";
                        }
                        planes.Remove(toDelete);
                        removed.Add(toDelete);
                    }
                    */
                    
                 
                    compareCheck++;
                    
                    foreach (Plane plane in planes)
                    {
                        Assert.IsTrue(queue.Contains(plane));
                    }
                    foreach (Plane plane in removed)
                    {
                        Assert.IsFalse(queue.Contains(plane));
                    }
                    
                }
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
                "\r\nQueue-List comparisons: " + compareCheck;
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\pairingheapTestStats.txt",
                checkInfo);
            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\pairingheapTestHistory.txt",
                history);
        }

        [TestMethod]
        public void TestIncPriority()
        {
            PairingHeap<Plane, int> queue = new PairingHeap<Plane, int>();
            int idCounter = 0;

            Plane plane1 = new Plane();
            plane1.SetInternationalID("id" + idCounter);
            idCounter++;
            plane1.SetPriority(4);
            queue.Add(plane1);

            Plane plane2 = new Plane();
            plane2.SetInternationalID("id" + idCounter);
            idCounter++;
            plane2.SetPriority(5);
            queue.Add(plane2);

            Plane plane3 = new Plane();
            plane3.SetInternationalID("id" + idCounter);
            idCounter++;
            plane3.SetPriority(7);
            queue.Add(plane3);

            queue.Delete(plane3);


            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\testIncPriority.txt",
                queue.TraverseLevelOrder());
        }


        [TestMethod]
        public void TestDecPriority()
        {
            PairingHeap<Plane, int> queue = new PairingHeap<Plane, int>();
            int idCounter = 0;

            Plane plane1 = new Plane();
            plane1.SetInternationalID("id" + idCounter);
            idCounter++;
            plane1.SetPriority(4);
            queue.Add(plane1);

            Plane plane2 = new Plane();
            plane2.SetInternationalID("id" + idCounter);
            idCounter++;
            plane2.SetPriority(5);
            queue.Add(plane2);

            Plane plane3 = new Plane();
            plane3.SetInternationalID("id" + idCounter);
            idCounter++;
            plane3.SetPriority(7);
            queue.Add(plane3);

            queue.ChangePriority(plane2, 89);


            File.WriteAllText("D:\\.NET Projects\\PlaneDepartureTracking\\PlaneDepartureTrackingMSTest\\testDecPriority.txt",
                queue.TraverseLevelOrder());
        }
    }
}
