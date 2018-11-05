namespace Hash_Table
{
    using System;

    public class AVLTree<T> where T : IComparable
    {
        public Node<T> Root { get; private set; }
        
        public int Count { get; private set; }
       
        public void Insert(T value)
        {
            this.Root = this.InsertRecursively(this.Root, value);
        }
        private Node<T> InsertRecursively(Node<T> node, T value)
        {
            if (node == null)
            {
                this.Count++;
                return new Node<T>(value);
            }

            var compared = node.Value.CompareTo(value);
            if (compared > 0)
            {
                node.Left = this.InsertRecursively(node.Left, value);
            }
            else if (compared < 0)
            {
                node.Right = this.InsertRecursively(node.Right, value);
            }

            node = this.Balance(node);
            this.UpdateHeight(node);

            return node;
        }

        public bool Contains (T value)
        {
            return this.SearchRecursively(this.Root, value) != null;
        }
        private Node<T> SearchRecursively (Node<T> node, T value)
        {
            if (node == null)
            {
                return null;
            }

            var compared = node.Value.CompareTo(value);
            if (compared > 0)
            {
                return this.SearchRecursively(node.Left, value);
            }
            else if (compared < 0)
            {
                return this.SearchRecursively(node.Right, value);
            }
            else
            {
                return node;
            }
        }

        public void EachInOrder (Action<T> action)
        {
            this.EachInOrderRecursively(this.Root, action);
        }
        private void EachInOrderRecursively(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrderRecursively(node.Left, action);
            action(node.Value);
            this.EachInOrderRecursively(node.Right, action);
        }

        public void Remove(T value)
        {
            this.Root = this.RemoveRecursively(this.Root, value);
        }
        private Node<T> RemoveRecursively(Node<T> node, T value)
        {
            if (node == null)
            {
                return null;
            }

            var compared = node.Value.CompareTo(value);
            if (compared > 0)
            {
                node = this.RemoveRecursively(node.Left, value);
            }
            else if (compared < 0)
            {
                node = this.RemoveRecursively(node.Right, value);
            }
            else
            {
                if (node.Left == null)
                {
                    node = node.Right;
                }
                else if (node.Right == null)
                {
                    node = node.Left;
                }
                else
                {
                    var minNode = this.FindMinNode(node.Right);
                    node.Value = minNode.Value;
                    node.Right = this.RemoveRecursively(node.Right, minNode.Value);

                    this.Balance(node);
                    this.UpdateHeight(node);
                }

                this.Count--;
            }

            return node;
        }

        private Node<T> FindMinNode(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            var current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        private Node<T> Balance(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            var heightDifferent = this.CompareNodesHeight(node.Left, node.Right);
            if (heightDifferent > 1)
            {
                if (this.CompareNodesHeight(node.Left.Left, node.Left.Right) < 0)
                {
                    node.Left = this.RotateLeft(node.Left);
                }

                node = this.RotateRight(node);
            }
            else if (heightDifferent < -1)
            {
                if (this.CompareNodesHeight(node.Right.Left, node.Right.Right) > 0)
                {
                    node.Right = this.RotateRight(node.Right);
                }

                node = this.RotateLeft(node);
            }

            return node;
        }
        private Node<T> RotateLeft(Node<T> node)
        {
            var subTreeRoot = node.Right;
            node.Right = subTreeRoot.Left;
            subTreeRoot.Left = node;

            this.UpdateHeight(node);
            return subTreeRoot;
        }
        private Node<T> RotateRight(Node<T> node)
        {
            var subTreeRoot = node.Left;
            node.Left = subTreeRoot.Right;
            subTreeRoot.Right = node;

            this.UpdateHeight(node);
            return subTreeRoot;
        }

        private void UpdateHeight(Node<T> node)
        {
            node.Height = 1 + this.GetBiggerHeight(node.Left, node.Right);
        }
        private int GetBiggerHeight(Node<T> node1, Node<T> node2)
        {
            return Math.Max(this.GetHeight(node1), this.GetHeight(node2));
        }
        private int CompareNodesHeight(Node<T> a, Node<T> b)
        {
            return this.GetHeight(a) - this.GetHeight(b);
        }
        private int GetHeight(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }
    }
}
