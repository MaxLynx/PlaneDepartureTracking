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

        public byte ObservedLevelCount { set; get; }

        public void Add(T el)
        {
            ObservedLevelCount = 0;
            if (Root == null)
            {
                Root = new TreeNode<T>();
                Root.Data = el;
            }
            else
            {
                TreeNode<T> parent = Root;
                TreeNode<T> child = Root;
                while (child != null)
                {
                    ObservedLevelCount++;
                    if (child.CompareTo(el) < 0)
                    {
                        parent = child;
                        child = child.Left;
                    }
                    else
                    if (child.CompareTo(el) > 0)
                    {
                        parent = child;
                        child = child.Right;
                    }
                    else
                    {
                        throw new ArgumentException("Duplicate entries are not allowed");
                    }

                }
                if (parent.CompareTo(el) < 0)
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
            }
        }

        public void Delete(T el) //Implement ROOT deletion
        {
            TreeNode<T> currentNode = Root;
            while (currentNode != null)
            {
                if (currentNode.CompareTo(el) < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                if (currentNode.CompareTo(el) > 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    TreeNode<T> parent = currentNode.Parent;
                    if(currentNode.Left == null && currentNode.Right == null)
                    {

                        currentNode = null;
                        Splay(parent);
                    }
                    else
                        if (currentNode.Left != null && currentNode.Right != null)
                    {
                        //TODO
                    }
                    else
                        if(currentNode.Left != null)
                    {
                        if (parent.Left.Equals(currentNode))
                        {
                            parent.Left = currentNode.Left;
                            currentNode.Left.Parent = parent;
                        }
                        else
                        {
                            parent.Right = currentNode.Left;
                            currentNode.Left.Parent = parent;
                        }
                        currentNode = null;
                        Splay(parent);
                    }
                    else // currentNode.Right != null
                    {
                        if (parent.Left.Equals(currentNode))
                        {
                            parent.Left = currentNode.Right;
                            currentNode.Right.Parent = parent;
                        }
                        else
                        {
                            parent.Right = currentNode.Right;
                            currentNode.Right.Parent = parent;
                        }
                        currentNode = null;
                        Splay(parent);
                    }
                }
            }

        }

        public TreeNode<T> Find(T el)
        {
            ObservedLevelCount = 0;
            TreeNode<T> currentNode = Root;
            TreeNode<T> savedNode = Root;
            while (currentNode != null)
            {
                savedNode = currentNode;
                ObservedLevelCount++;
                if (currentNode.CompareTo(el) < 0)
                {
                    currentNode = currentNode.Left;
                }
                else
                if (currentNode.CompareTo(el) > 0)
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

        TreeNode<T> FindMin(TreeNode<T> root)
        {
            ObservedLevelCount = 0;
            if (root == null)
            {
                return null;
            }
            while (root.Left != null)
            {
                ObservedLevelCount++;
                root = root.Left;
            }
            return root;
        }

        private void Splay(TreeNode<T> node)
        {
            //TODO
        }

    }
}
