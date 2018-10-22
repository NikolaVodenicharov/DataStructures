using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T:IComparable
{
    private const string EmptyCollectionMessage = "The collection is empty.";
    private int nodeCount;
    private Node root;

    public BinarySearchTree()
    {
    }
    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }
    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }
  
    public int Count()
    {
        return this.nodeCount;
    }

    public void Insert(T element)
    {
        this.root = this.InsertRecursively(element, this.root);
    }
    private Node InsertRecursively(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
            this.nodeCount++;
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.InsertRecursively(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.InsertRecursively(element, node.Right);
        }

        return node;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(EmptyCollectionMessage);
        }

        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }

        this.nodeCount--;
    }
    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(EmptyCollectionMessage);
        }

        Node parent = null;
        var current = this.root;
        while (current.Right != null)
        {
            parent = current;
            current = current.Right;
        }

        if (parent != null)
        {
            parent.Right = current.Left;
        }
        else
        {
            this.root = this.root.Left;
        }

        this.nodeCount--;
    }
    public void Delete(T element)
    {
        this.root = DeleteRecursively(element, this.root);
        this.nodeCount--;
    }
    private Node DeleteRecursively(T element, Node node)
    {
        if (node == null)
        {
            return null;
        }

        var comparedValue = node.Value.CompareTo(element);

        if (comparedValue > 0)
        {
            node.Left = this.DeleteRecursively(element, node.Left);
        }
        else if (comparedValue < 0)
        {
            node.Right = this.DeleteRecursively(element, node.Right);   
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            node.Value = FindMinValue(node.Right);
            node.Right = this.DeleteRecursively(node.Value, node.Right);
        }

        return node;
    }
    private T FindMinValue(Node node)
    {
        var current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }

        return current.Value;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }
    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    public int Rank(T element)
    {
        return this.RankRecursively(element, this.root);
    }
    private int RankRecursively(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        var compared = element.CompareTo(node.Value);

        if (compared < 0)
        {
            return this.RankRecursively(element, node.Left);
        }

        if (compared > 0)
        {
            return 1 + this.RankRecursively(element, node.Left) + this.RankRecursively(element, node.Right);
        }

        return 0; // something different from 0 ?
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }
    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }
    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }
    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    public T Select(int rank)
    {
        throw new NotImplementedException();
    }

    public T Ceiling(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(EmptyCollectionMessage);
        }

        var current = this.root;
        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                return current.Value;
            }
            else
            {
                current = current.Right;
            }
        }

        throw new InvalidOperationException("There is no element bigger that given.");
    }
    public T Floor(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException(EmptyCollectionMessage);
        }

        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) < 0)
            {
                return current.Value;
            }
            else
            {
                current = current.Left;
            }
        }

        throw new InvalidOperationException("There is no element smaller that given.");
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

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        bst.EachInOrder(Console.WriteLine);
        
    }
}