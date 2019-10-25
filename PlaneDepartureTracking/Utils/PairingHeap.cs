using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    /**
     * T - key (priority), V - data 
     **/
    public class PairingHeap<V, T>
          where V : IPriority<T>, IComparable<V>
          where T : IComparable<T>
    {
        public TreeNode<V> Root { set; get; }
        private TreeNode<V> Pair(TreeNode<V> heap1Root, TreeNode<V> heap2Root)
        {
            if(heap1Root == null)
            {
                return heap2Root;
            }
            else
            if (heap2Root == null)
            {
                return heap1Root;
            }
            else
            if(heap1Root.Data.GetPriority().CompareTo(heap2Root.Data.GetPriority()) < 0)
            {
                heap1Root.Right = heap2Root.Left;
                if (heap2Root.Left != null)
                {
                    heap2Root.Left.Parent = heap1Root.Right;
                }
                heap2Root.Left = heap1Root;
                heap1Root.Parent = heap2Root;
                return heap2Root;
            }
            else
            {
                heap2Root.Right = heap1Root.Left;
                if (heap2Root.Left != null)
                {
                    heap1Root.Left.Parent = heap2Root.Right;
                }
                heap1Root.Left = heap2Root;
                heap2Root.Parent = heap1Root;
                return heap1Root;
            }
        }
        public bool ChangePriority(V el, T newPriority)
        {
            if (el.GetPriority().CompareTo(newPriority) == 0)
                return true;
            TreeNode<V> currentNode = Root;
            while (currentNode != null)
            {
                if (currentNode.Data.GetPriority().CompareTo(el.GetPriority()) < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                if (currentNode.Data.GetPriority().CompareTo(el.GetPriority()) > 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    if (currentNode.Data.Equals(el))
                    {
                        if (el.GetPriority().CompareTo(newPriority) < 0)
                        {
                            currentNode.Parent = null;
                            Root = Pair(currentNode, Root);
                        }
                        else
                        {
                            TreeNode<V> parent = currentNode.Parent;
                            TreeNode<V> newSubtreeRoot = Pair(Pair(currentNode, currentNode.Left), currentNode.Right);
                            newSubtreeRoot.Parent = parent;
                            if (parent.Left != null && parent.Left.Equals(currentNode))
                            {
                                parent.Left = newSubtreeRoot;
                            }
                            else
                            {
                                parent.Right = newSubtreeRoot;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        currentNode = currentNode.Right;
                    }
                }
            }
            return false;
        }
        public bool Add(V el)
        {
            TreeNode<V> newNode = new TreeNode<V>();
            newNode.Data = el;
            Root = Pair(newNode, Root);
            return true;

        }

        /*
         * Not implemented
         */
        public bool Contains(V el)
        {
            throw new NotImplementedException();
        }

        public void Delete(V el)
        {
            ChangePriority(el, el.GetMaxPriority());
            DeleteMin();
        }

        public V DeleteMin()
        {
            V rootData = Root.Data;
            Root.Left.Parent = null;
            Root.Right.Parent = null;
            Root = Pair(Root.Left, Root.Right);
            return rootData;
        }

        /*
         * Not implemented
         */
        public TreeNode<V> Find(V el)
        {
            throw new NotImplementedException();
        }

        public String TraverseInOrder()
        {
            String result = "";
            Stack<TreeNode<V>> stack = new Stack<TreeNode<V>>();
            TreeNode<V> currentNode = Root;

            while (currentNode != null || stack.Count > 0)
            {
                while (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.Left;
                }

                currentNode = stack.Pop();

                result += currentNode.Data + " < ";

                currentNode = currentNode.Right;
            }

            return result;
        }

        public String TraverseLevelOrder()
        {
            String result = "";
            Queue<TreeNode<V>> queue = new Queue<TreeNode<V>>();
            TreeNode<V> currentNode = Root;

            int currentLevelNodesCount = 1;
            int nextLevelNodesCount = 0;

            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                for (int i = 0; i < currentLevelNodesCount; i++)
                {
                    currentNode = queue.Dequeue();
                    result += currentNode.Data + " ";
                    if (currentNode.Left != null && currentNode.Right != null)
                    {
                        nextLevelNodesCount += 2;
                        queue.Enqueue(currentNode.Left);
                        queue.Enqueue(currentNode.Right);
                    }
                    else
                        if (currentNode.Left != null)
                    {
                        nextLevelNodesCount += 1;
                        queue.Enqueue(currentNode.Left);
                    }
                    else
                        if (currentNode.Right != null)
                    {
                        nextLevelNodesCount += 1;
                        queue.Enqueue(currentNode.Right);
                    }


                }

                result += "\r\n";
                currentLevelNodesCount = nextLevelNodesCount;
                nextLevelNodesCount = 0;
            }

            return result;
        }
    }
}
