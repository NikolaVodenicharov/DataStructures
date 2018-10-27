using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }
    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    public void Insert(T item)
    {
        this.root = this.InsertRecursively(this.root, item);
    }
    private Node<T> InsertRecursively(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int compared = item.CompareTo(node.Value);
        if (compared < 0)
        {
            node.Left = this.InsertRecursively(node.Left, item);
        }
        else if (compared > 0)
        {
            node.Right = this.InsertRecursively(node.Right, item);
        }

        node = Balance(node);
        UpdateHeight(node);

        return node;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrderRecursively(this.root, action);
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

    private static Node<T> Balance(Node<T> node)
    {
        int balance = FindHeight(node.Left) - FindHeight(node.Right);
        if (balance < -1) // right subtree is heavier
        {
            if (FindHeight(node.Right.Left) > FindHeight(node.Right.Right))
            {
                node.Right = RotateRight(node.Right);
            }

            return RotateLeft(node);
        }
        else if (balance > 1) // left child is heavier
        {
            if (FindHeight(node.Left.Left) < FindHeight(node.Left.Right))
            {
                node.Left = RotateLeft(node.Left);
            }

            return RotateRight(node);
        }

        return node;
    }
    private static Node<T> RotateLeft(Node<T> node)
    {
        var subTreeRoot = node.Right;
        node.Right = subTreeRoot.Left;
        subTreeRoot.Left = node;

        UpdateHeight(node);

        return subTreeRoot;
    }
    private static Node<T> RotateRight(Node<T> node)
    {
        var subTreeRoot = node.Left;
        node.Left = subTreeRoot.Right;
        subTreeRoot.Right = node;

        UpdateHeight(node);

        return subTreeRoot;
    }

    private static void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(FindHeight(node.Left), FindHeight(node.Right)) + 1;
    }
    private static int FindHeight(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }
}
