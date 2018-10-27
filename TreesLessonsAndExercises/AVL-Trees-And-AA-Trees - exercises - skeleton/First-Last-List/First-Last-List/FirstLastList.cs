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
            var node = new Node<T>(element);
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

            this.Balance(parent);
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

            var sorted = new List<T>();
            this.EachInOrderRecursively(this.root, sorted.Add);
            sorted.Reverse();

            return sorted.Take(count);
        }
        public IEnumerable<T> Min(int count)
        {
            if (count > this.nodes.Count)
            {
                throw new ArgumentOutOfRangeException("Given count is bigger than the elements count");
            }

            var sorted = new List<T>();
            this.EachInOrderRecursively(this.root, sorted.Add);

            return sorted.Take(count);
        }

        public int RemoveAll(T element)
        {
            throw new NotImplementedException();
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
