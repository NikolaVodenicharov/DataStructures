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

    public void Delete(T element)
    {
        this.root = this.DeleteRecursively(this.root, element);
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
            var hasLeftChild = node.Left != null;
            var hasRightChild = node.Right != null;

            if (node.Left == null)
            {
                node = node.Right;
            }
            else if (node.Right == null)
            {
                node = node.Left; // if left is null and right is null, the node will be null;
            }
            else
            {
                var minNode = this.FindMinValueNode(node.Right);
                node.Value = minNode.Value;
                node.Right = this.DeleteRecursively(node.Right, minNode.Value);
            }
        }

        node = Balance(node);
        UpdateHeight(node);
        return node;
    }
    private Node<T> FindMinValueNode (Node<T> node)
    {
        var current = node;
        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }
        else if (this.root.Left == null)
        {
            this.root = this.root.Right;
            return;
        }

        var node = this.root;
        while (node.Left != null)
        {
            if (node.Left.Left == null)
            {
                break;
            }

            node = node.Left;
        }

        node.Left = node.Left.Right;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }
    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private Node<T> Balance(Node<T> node)
    {
        if (node == null)
        {
            return null;
        }

        var balance = FindHeight(node.Left) - FindHeight(node.Right);
        if (balance > 1)
        {
            var childBalance = FindHeight(node.Left.Left) - FindHeight(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = FindHeight(node.Right.Left) - FindHeight(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }
    private int FindHeight(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }
    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }
    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }

    private void UpdateHeight(Node<T> node)
    {
        if (node == null)
        {
            return;
        }

        node.Height = Math.Max(FindHeight(node.Left), FindHeight(node.Right)) + 1;
    }  
}
