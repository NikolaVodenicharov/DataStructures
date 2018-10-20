using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node root)
    {
        this.Copy(root);
    }
    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }
    public BinarySearchTree<T> Search(T item)
    {
        var current = root;

        while (current != null)
        {
            if (current.Value.CompareTo(item) < 0)
            {
                current = current.Right;
            }
            else if (current.Value.CompareTo(item) > 0)
            {
                current = current.Left;
            }
            else
            {
                break;
            }
        }

        return new BinarySearchTree<T>(current); ;
    }

    public void Insert(T value)
    {
        if (root == null)
        {
            root = new Node(value);
            return;
        }

        var child = new Node(value);
        var current = root;

        while (true)
        {
            if (current.Value.CompareTo(value) < 0)
            {
                if (current.Right == null)
                {
                    current.Right = child;
                    break;
                }

                current = current.Right;
            }
            else if (current.Value.CompareTo(value) > 0)
            {
                if (current.Left == null)
                {
                    current.Left = child;
                    break;
                }

                current = current.Left;
            }
            else
            {
                break;
            }
        }
    }

    public bool Contains(T value)
    {
        var current = root;

        while (current != null)
        {
            if (current.Value.CompareTo(value) < 0)
            {
                current = current.Right;
            }
            else if (current.Value.CompareTo(value) > 0)
            {
                current = current.Left;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public void DeleteMin()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        throw new NotImplementedException();
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(action, this.root);
    }
    private void EachInOrder(Action<T> action, Node node)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(action, node.Left);
        action(node.Value);
        this.EachInOrder(action, node.Right);
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}
