namespace First_Last_List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // using Wintellect.PowerCollections;

    public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
    {
        private IList<Node<T>> nodes;
        private Node<T> root;

        public FirstLastList()
        {
            this.nodes = new List<Node<T>>();
        }

        public int Count
        {
            get
            {
                return this.nodes.Count;
            }
        }

        public void Add(T element)
        {
            var node = new Node<T>(nodes.Count, element);
            this.nodes.Add(node);

            this.root = this.InsertRecursively(this.root, node);
        }
        private Node<T> InsertRecursively(Node<T> parent, Node<T> node)
        {
            if (parent == null)
            {
                return node;
            }

            var compared = parent.CompareTo(node);
            if (compared > 0)
            {
                parent.Left = this.InsertRecursively(parent.Left, node);
            }
            else if (compared < 0)
            {
                parent.Right = this.InsertRecursively(parent.Right, node);
            }

            parent = this.Balance(parent);
            this.SetHeight(parent);

            return parent;
        }

        private Node<T> Balance (Node<T> node)
        {
            var balance = this.GetHeight(node.Left) - this.GetHeight(node.Right);

            if (balance < -1)
            {
                if (this.GetHeight(node.Right.Left) > this.GetHeight(node.Right.Right))
                {
                    node.Right = this.RotateRight(node.Right);
                }

                node = RotateLeft(node);
            }
            else if (balance > 1)
            {
                if (this.GetHeight(node.Left.Left) < this.GetHeight(node.Left.Right))
                {
                    node.Left = this.RotateLeft(node.Left);
                }

                node = RotateRight(node);
            }

            return node;
        }
        private Node<T> RotateLeft(Node<T> node)
        {
            var subTreeRoot = node.Right;
            node.Right = subTreeRoot.Left;
            subTreeRoot.Left = node;

            this.SetHeight(node);

            return subTreeRoot;
        }
        private Node<T> RotateRight(Node<T> node)
        {
            var subTreeRoot = node.Left;
            node.Left = subTreeRoot.Right;
            subTreeRoot.Right = node;

            this.SetHeight(node);

            return subTreeRoot;
        }

        private void SetHeight(Node<T> node)
        {
            node.Height = 1 + Math.Max(
                this.GetHeight(node.Left), 
                this.GetHeight(node.Right)); 
        }
        private int GetHeight(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        public void Clear()
        {
            this.nodes.Clear();
            this.root = null;
        }

        public IEnumerable<T> First(int count)
        {
            if (count > this.nodes.Count)
            {
                throw new ArgumentOutOfRangeException("Given count is bigger than the elements count");
            }

            return this.nodes
                .Take(count)
                .Select(n => n.Value)
                .ToList();
        }
        public IEnumerable<T> Last(int count)
        {
            if (count > this.nodes.Count)
            {
                throw new ArgumentOutOfRangeException("Given count is bigger than the elements count");
            }

            var elements = this.nodes
                .Skip(this.nodes.Count - count)
                .Take(count)
                .Select(n => n.Value)
                .ToList();

            elements.Reverse();

            return elements;
        }

        public IEnumerable<T> Max(int count)
        {
            if (count > this.nodes.Count)
            {
                throw new ArgumentOutOfRangeException("Given count is bigger than the elements count");
            }

            var maxElements = new List<T>();
            //this.MaxRecursively(this.root, maxElements, count);
            //return maxElements;

            this.EachInOrderRecursively(this.root, maxElements.Add);
            maxElements.Reverse();

            return maxElements.Take(count);
        }
        //private void MaxRecursively(Node<T> node, IList<T> list, int count)
        //{
        //    if (node == null || list.Count == count)
        //    {
        //        return;
        //    }

        //    this.MaxRecursively(node.Right, list, count);
        //    list.Add(node.Value);
        //    this.MaxRecursively(node.Left, list, count);
        //}

        public IEnumerable<T> Min(int count)
        {
            if (count > this.nodes.Count)
            {
                throw new ArgumentOutOfRangeException("Given count is bigger than the elements count");
            }

            var minElements = new List<T>();
            this.EachInOrderRecursively(this.root, minElements.Add);
            return minElements.Take(count);

            //this.MinRecursively(this.root, minElements, count);
            //return minElements;
        }
        //private void MinRecursively(Node<T> node, IList<T> list, int count)
        //{
        //    if (node == null || list.Count == count)
        //    {
        //        return;
        //    }

        //    this.MinRecursively(node.Left, list, count);
        //    list.Add(node.Value);
        //    this.MinRecursively(node.Right, list, count);
        //}

        public int RemoveAll(T element)
        {
            this.DeleteRecursively(this.root, element);

            return 1;
            // throw new NotImplementedException();
        }
        private Node<T> DeleteRecursively(Node<T> node, T element)
        {
            if (node == null)
            {
                return null;
            }

            var compared = node.Value.CompareTo(element);
            if (compared > 0)
            {
                node.Left = this.DeleteRecursively(node.Left, element);
            }
            else if (compared < 0)
            {
                node.Right = this.DeleteRecursively(node.Right, element);
            }
            else
            {
                node = DeleteNode(node);
            }

            return node;
        }

        private Node<T> DeleteNode(Node<T> node)
        {
            var hasLeftChild = node.Left != null;
            var hasRightChild = node.Right != null;

            if (hasLeftChild && hasRightChild)
            {
                var minNode = this.FindMinElement(node.Right);
                node.Key = minNode.Key;
                node.Value = minNode.Value;

                node.Right = this.DeleteRecursively(node.Right, minNode.Value);
            }
            else if (!hasRightChild && !hasRightChild)
            {
                node = null;
            }
            else if (hasLeftChild)
            {
                node = node.Left;
            }
            else
            {
                node = node.Right;
            }

            if (node != null)
            {
                node = this.Balance(node);
                this.SetHeight(node);
            }

            return node;
        }

        private Node<T> FindMinElement(Node<T> root)
        {
            var current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
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



        //public int Count
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public void Add(T element)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Clear()
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<T> First(int count)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<T> Last(int count)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<T> Max(int count)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<T> Min(int count)
        //{
        //    throw new NotImplementedException();
        //}

        //public int RemoveAll(T element)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
