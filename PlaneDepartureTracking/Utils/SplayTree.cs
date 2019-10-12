using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneDepartureTracking.Utils
{
    class SplayTree<T> : ITree<T>
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
                        throw new ArgumentException("Duplicate entries are not allowed");
                    }

                }
                root = new TreeNode<T>();
                root.Data = el;
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

        
    }
}
