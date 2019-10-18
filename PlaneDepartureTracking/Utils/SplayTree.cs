using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    public class SplayTree<T> : ITree<T>
          where T : IComparable<T>
    {
        public TreeNode<T> Root { set; get; }

        public Boolean SplayDisabled { set; get; }
                
        public bool Add(T el)
        {
            
            if (Root == null)
            {
                Root = new TreeNode<T>();
                Root.Data = el;
                return true;
            }
            else
            {
                TreeNode<T> parent = Root;
                TreeNode<T> child = Root;
                while (child != null)
                {
                    if (child.CompareTo(el) > 0)
                    {
                        parent = child;
                        child = child.Left;
                    }
                    else
                    if (child.CompareTo(el) < 0)
                    {
                        parent = child;
                        child = child.Right;
                    }
                    else
                    {
                        return false;
                    }

                }
                // empty place for insertion found
                if (parent.CompareTo(el) > 0)
                {
                    parent.Left = new TreeNode<T>();
                    parent.Left.Data = el;
                    parent.Left.Parent = parent;
                    Splay(parent.Left);
                }
                else
                {
                    parent.Right = new TreeNode<T>();
                    parent.Right.Data = el;
                    parent.Right.Parent = parent;
                    Splay(parent.Right);
                }
                return true;
            }
        }

        public void Delete(T el) 
        {
            TreeNode<T> currentNode = Root;
            while (currentNode != null)
            {
                if (currentNode.CompareTo(el) > 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                if (currentNode.CompareTo(el) < 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    TreeNode<T> parent = currentNode.Parent;
                    if(currentNode.Left == null && currentNode.Right == null)
                    {
                        if (parent != null)
                        {
                            if (parent.Left != null && parent.Left.Equals(currentNode))
                            {
                                parent.Left = null;
                            }
                            else
                            {
                                parent.Right = null;
                            }
                            currentNode.Parent = null;
                            Splay(parent);
                        }
                        else
                        {
                            Root = null;
                        }
                    }
                    else
                        if (currentNode.Left != null && currentNode.Right != null)
                    {
                        TreeNode<T> successor = FindMin(currentNode.Right);
                        currentNode.Data = successor.Data;
                        if(successor.Right == null)
                        {
                            if (successor.Parent.Left != null && successor.Parent.Left.Equals(successor))
                            {
                                successor.Parent.Left = null;
                            }
                            else // ONLY IF currentNode.Right IS MINIMAL ITSELF
                            {
                                successor.Parent.Right = null;
                            }
                            successor.Parent = null;
                        }
                        else
                        {
                            if (successor.Parent.Left != null && successor.Parent.Left.Equals(successor))
                            {
                                successor.Parent.Left = successor.Right;
                                successor.Right.Parent = successor.Parent;
                            }
                            else
                            {
                                successor.Parent.Right = successor.Right;
                                successor.Right.Parent = successor.Parent;
                            }
                            successor.Parent = null;
                        }
                        Splay(parent);
                    }
                    else
                        if (currentNode.Left != null)
                    {
                        if (parent != null)
                        {
                            if (parent.Left != null && parent.Left.Equals(currentNode))
                            {
                                parent.Left = currentNode.Left;
                                currentNode.Left.Parent = parent;
                            }
                            else
                            {
                                parent.Right = currentNode.Left;
                                currentNode.Left.Parent = parent;
                            }
                            currentNode.Parent = null;
                            Splay(parent);
                        }
                        else
                        {
                            currentNode.Left.Parent = null;
                            Root = currentNode.Left;
                        }
                    }
                    else // currentNode.Right != null
                    {
                        if (parent != null)
                        {
                            if (parent.Left != null && parent.Left.Equals(currentNode))
                            {
                                parent.Left = currentNode.Right;
                                currentNode.Right.Parent = parent;
                            }
                            else
                            {
                                parent.Right = currentNode.Right;
                                currentNode.Right.Parent = parent;
                            }
                            currentNode.Parent = null;
                            Splay(parent);
                        }
                        else
                        {
                            currentNode.Right.Parent = null;
                            Root = currentNode.Right;
                        }
                    }
                    break;
                }
            }

        }

        public TreeNode<T> Find(T el)
        {
            TreeNode<T> currentNode = Root;
            TreeNode<T> savedNode = Root;
            while (currentNode != null)
            {
                savedNode = currentNode;
                if (currentNode.CompareTo(el) > 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                if (currentNode.CompareTo(el) < 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    Splay(currentNode);
                    return currentNode;
                }
            }
            Splay(savedNode);
            return null;

        }

        public bool Contains(T el)
        {
            if (Find(el) != null)
                return true;
            return false;
        }


        public T FindMin()
        {
            return FindMin(Root).Data;
        }

        public T FindMax()
        {
            return FindMax(Root).Data;
        }

        TreeNode<T> FindMin(TreeNode<T> root)
        {
            if (root == null)
            {
                return null;
            }
            while (root.Left != null)
            {
                root = root.Left;
            }
            return root;
        }

        TreeNode<T> FindMax(TreeNode<T> root)
        {
            if (root == null)
            {
                return null;
            }
            while (root.Right != null)
            {
                root = root.Right;
            }
            return root;
        }

        private void Splay(TreeNode<T> node)
        {
            if (!SplayDisabled)
            {
                //
            }
        }

        public String TraverseInOrder()
        {
            String result = "";
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            TreeNode<T> currentNode = Root;

            while(currentNode != null || stack.Count > 0)
            {
                while(currentNode != null)
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
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            TreeNode<T> currentNode = Root;

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

        public bool CheckTreeStructure()
        {
            List<T> list = new List<T>();
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            TreeNode<T> currentNode = Root;

            while (currentNode != null || stack.Count > 0)
            {
                while (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.Left;
                }

                currentNode = stack.Pop();
                list.Append(currentNode.Data);
                currentNode = currentNode.Right;
            }

            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) >= 0)
                    return false;
            }

            return true;
        }


    }
}
