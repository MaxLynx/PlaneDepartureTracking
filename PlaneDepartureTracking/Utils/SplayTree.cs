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

        public void Add(T el)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>();
                Root.Data = el;
            }
            else {
                TreeNode<T> parent = Root;
                TreeNode<T> child = Root;
                while (child != null)
                {
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
                }
                else
                {
                    parent.Right = new TreeNode<T>();
                    parent.Right.Data = el;
                }
            }
        }

        public void Delete(T el)
        {
            if(Root != null)
            {
                TreeNode<T> root = Root;
                while(root != null)
                {
                    if (root.CompareTo(el) < 0)
                    {
                        root = root.Left;
                    }
                    else
                    if (root.CompareTo(el) > 0)
                    {
                        root = root.Right;
                    }
                    else
                    {
                        root = null;
                    }
                }
            }
        }

        public TreeNode<T> Find(T el)
        {
                TreeNode<T> root = Root;
                while (root != null)
                {
                    if (root.CompareTo(el) < 0)
                    {
                        root = root.Left;
                    }
                    else
                    if (root.CompareTo(el) > 0)
                    {
                        root = root.Right;
                    }
                    else
                    {
                        return root;
                    }
                }
                return null;
            
        }

        public bool Contains(T el)
        {
            if (Find(el) != null)
                return true;
            return false;
        }

        
    }
}
