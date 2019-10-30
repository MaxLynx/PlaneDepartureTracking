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
          where V : IPriority<T, V>, IComparable<V>
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
            if(heap1Root.Data.GetPriority().CompareTo(heap2Root.Data.GetPriority()) > 0)
            {
                TreeNode<V> rightChild = heap1Root.Right;
                if (heap2Root.Left != null)
                {
                    heap2Root.Left.Parent = heap1Root;
                    heap1Root.Right = heap2Root.Left;
                    
                    if (rightChild != null)
                    {
                        TreeNode<V> iterNode = heap1Root.Right;
                        while (iterNode.Right != null)
                        {
                            iterNode = iterNode.Right;
                        }
                        iterNode.Right = rightChild;
                        rightChild.Parent = iterNode;
                    }
                }
                heap2Root.Left = heap1Root;
                heap1Root.Parent = heap2Root;
                
                return heap2Root;
            }
            else
            {
                TreeNode<V> rightChild = heap2Root.Right;
                if (heap1Root.Left != null)
                {
                    heap2Root.Right = heap1Root.Left;
                    heap1Root.Left.Parent = heap2Root;
                    
                    if (rightChild != null)
                    {
                        TreeNode<V> iterNode = heap2Root.Right;
                        while (iterNode.Right != null)
                        {
                            iterNode = iterNode.Right;
                        }
                        iterNode.Right = rightChild;
                        rightChild.Parent = iterNode;
                    }
                }
                heap1Root.Left = heap2Root;
                heap2Root.Parent = heap1Root;
                
                return heap1Root;
            }
        }
        public bool ChangePriority(V el, T newPriority)
        {

            
            
            TreeNode<V> currentNode = el.GetHeapNode();
            if (currentNode != null)
            {
                if (el.GetPriority().CompareTo(newPriority) == 0)
                {
                    return true;
                }
                else
                if (el.GetPriority().CompareTo(newPriority) > 0)
                {
                    IncreasePriority(currentNode, newPriority);
                }
                else
                {
                    DecreasePriority(currentNode, newPriority);
                }
                return true;
                
            }
                
            return false;
        }

        private void IncreasePriority(TreeNode<V> currentNode, T newPriority)
        {
            TreeNode<V> oldCurrentNode = currentNode;
            currentNode.Data.SetPriority(newPriority);

            if (currentNode.Parent != null && currentNode.Parent.Data.GetPriority().CompareTo(newPriority) > 0)
            {
                TreeNode<V> rightChild = currentNode.Right;

                    if (currentNode.Parent.Left != null && currentNode.Parent.Left.Equals(oldCurrentNode))
                    {
                        currentNode.Parent.Left = rightChild;
                    }
                    else
                    {
                        currentNode.Parent.Right = rightChild;
                    }

                if (rightChild != null)
                {
                    rightChild.Parent = currentNode.Parent;
                }
                currentNode.Parent = null;
                currentNode.Right = null;
                
                Root = Pair(currentNode, Root);
                
            }
        }

        private void DecreasePriority(TreeNode<V> currentNode, T newPriority)
        {
            TreeNode<V> oldCurrentNode = currentNode;
            currentNode.Data.SetPriority(newPriority);

            if (currentNode.Left != null && currentNode.Left.Data.GetPriority().CompareTo(newPriority) < 0)
            {
                TreeNode<V> parent = currentNode.Parent;
                TreeNode<V> leftChild = currentNode.Left;
                currentNode.Left = null;
                leftChild.Parent = null;
                TreeNode<V> rightChild = currentNode.Right;
                currentNode.Right = null;
                if (rightChild != null)
                {
                    rightChild.Parent = null;
                }
                TreeNode<V> newSubtreeRoot = Pair(Pair(currentNode, leftChild), rightChild);
                newSubtreeRoot.Parent = parent;
                if (parent == null)
                {
                    Root = newSubtreeRoot;
                }
                else
                if (parent.Left != null && parent.Left.Equals(oldCurrentNode))
                {
                    parent.Left = newSubtreeRoot;
                }
                else
                {
                    parent.Right = newSubtreeRoot;
                }
            }
        }

        public bool Add(V el)
        {
            TreeNode<V> newNode = new TreeNode<V>();
            newNode.Data = el;
            el.SetHeapNode(newNode);
            Root = Pair(newNode, Root);
            return true;

        }

       
        public bool Contains(V el)
        {
            if(el.GetHeapNode() != null)
            {
                return true;
            }
            return false;
        }

        public V Delete(V el)
        {
            if (ChangePriority(el, el.GetMaxPriority()))
            {
                return DeleteMin();
            }
            else
            {
                return default;
            }
        }

        public V DeleteMin()
        {
            if(Root == null)
            {
                return default;  // default for V if V could not be null
            }
            V rootData = Root.Data;
            rootData.SetHeapNode(null);
            Queue<TreeNode<V>> queue = new Queue<TreeNode<V>>();
            TreeNode<V> currentNode = Pair(Root.Left, Root.Right);
            if (currentNode != null)
            {
                while (currentNode != null)
                {
                    currentNode.Parent = null;
                    TreeNode<V> nextNode = currentNode.Right;
                    currentNode.Right = null;
                    queue.Enqueue(currentNode);
                    currentNode = nextNode;
                }
                while (queue.Count > 1)
                {
                    queue.Enqueue(Pair(queue.Dequeue(), queue.Dequeue()));
                }
                Root = queue.Dequeue();
            }
            else
            {
                Root = null;
            }

            return rootData;
        }

        
        public TreeNode<V> Find(V el)
        {
            return el.GetHeapNode();
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

                result += currentNode.Data.GetPriority() + " < ";

                currentNode = currentNode.Right;
            }

            return result;
        }

        public String TraverseLevelOrder()
        {
            String result = "";
            if (Root != null)
            {
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
                        result += currentNode.Data.GetPriority() + " ";
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
            }
            return result;
        }
    }
}
