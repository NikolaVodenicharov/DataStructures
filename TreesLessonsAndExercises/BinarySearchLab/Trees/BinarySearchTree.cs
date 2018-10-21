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
        if (this.root == null)
        {
            return;
        }

        Node parent = null;
        var min = this.root;

        while (min.Left != null)
        {
            parent = min;
            min = min.Left;
        }

        if (parent == null)
        {
            this.root = null;
        }
        else
        {
            parent.Left = null;
        }
        
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        var elements = new Queue<T>();
        this.RangeRecurse(this.root, elements, startRange, endRange);

        return elements;
    }

    private void RangeRecurse(Node node, Queue<T> elements, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        var nodeInLowerRange = startRange.CompareTo(node.Value);
        var nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.RangeRecurse(node.Left, elements, startRange, endRange);
        }

        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            elements.Enqueue(node.Value);
        }

        if (nodeInHigherRange > 0)
        {
            this.RangeRecurse(node.Right, elements, startRange, endRange);
        }
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
