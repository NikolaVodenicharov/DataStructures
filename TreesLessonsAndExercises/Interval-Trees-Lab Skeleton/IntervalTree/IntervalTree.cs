using System;
using System.Collections.Generic;

public class IntervalTree
{
    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.InsertRecursively(this.root, lo, hi);
    }
    private Node InsertRecursively(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = InsertRecursively(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = InsertRecursively(node.right, lo, hi);
        }

        this.UpdateMax(node);
        return node;
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrderRecursively(this.root, action);
    }
    private void EachInOrderRecursively(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrderRecursively(node.left, action);
        action(node.interval);
        EachInOrderRecursively(node.right, action);
    }

    public Interval SearchAny(double lo, double hi)
    {
        var current = this.root;
        while (current != null &&
               !current.interval.Intersects(lo, hi))
        {
            if (current.left != null &&
                current.left.max >= lo)
            {
                current = current.left;
            }
            else
            {
                current = current.right;
            }
        }

        if (current == null)
        {
            return null;
        }

        return current.interval;
    }

    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        var results = new List<Interval>();
        this.SearchAllRecursively(this.root, lo, hi, results);
        return results;
    }
    private void SearchAllRecursively(Node node, double lo, double hi, IList<Interval> results)
    {
        if (node == null)
        {
            return;
        }

        var isGoingLeft = node.left != null && node.left.max >= lo;
        var isGoingRight = node.right != null && node.right.interval.Lo <= hi;

        if (isGoingLeft)
        {
            this.SearchAllRecursively(node.left, lo, hi, results);
        }

        if (node.interval.Intersects(lo, hi))
        {
            results.Add(node.interval);
        }

        if (isGoingRight)
        {
            this.SearchAllRecursively(node.right, lo, hi, results);
        }
    }
    
    private void UpdateMax(Node node)
    {
        var maxChild = this.GetMax(node.left, node.right);

        node.max = this
            .GetMax(node, maxChild)
            .max;
    }
    private Node GetMax(Node a, Node b)
    {
        if (a == null)
        {
            return b;
        }
        else if (b == null)
        {
            return a;
        }
        else
        {
            return a.max >= b.max ? a : b;
        }
    }

    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }
}
