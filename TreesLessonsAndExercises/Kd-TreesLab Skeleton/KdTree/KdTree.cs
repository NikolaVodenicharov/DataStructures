using System;

public class KdTree
{
    private Node root;

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        return this.SearchRecursively(this.root, point) != null;
    }
    private Node SearchRecursively(Node node, Point2D point, int depth = 0)
    {
        if (node == null)
        {
            return null;
        }

        var compared = this.Compare(node.Point, point, depth);
        if (compared > 0)
        {
            return this.SearchRecursively(node.Left, point, depth + 1);
        }
        else if (compared < 0)
        {
            return this.SearchRecursively(node.Right, point, depth + 1);
        }

        return node;
    }

    public void Insert(Point2D point)
    {
        this.root = this.InsertRecursively(this.root, point);
    }
    private Node InsertRecursively(Node node, Point2D point, int depth = 0)
    {
        if (node == null)
        {
            return new Node(point);
        }

        var compared = this.Compare(node.Point, point, depth);
        if (compared > 0)
        {
            node.Left = this.InsertRecursively(node.Left, point, depth + 1);
        }
        else if (compared < 0)
        {
            node.Right = this.InsertRecursively(node.Right, point, depth + 1);
        }

        return node;
    }
    private int Compare(Point2D point1, Point2D point2, int depth)
    {
        var compared = 0;

        if (depth % 2 == 0)
        {
            compared = point1.X.CompareTo(point2.X);
            if (compared == 0)
            {
                compared = point1.Y.CompareTo(point2.Y);
            }
        }
        else
        {
            compared = point1.Y.CompareTo(point2.Y);
            if (compared == 0)
            {
                compared = point1.X.CompareTo(point2.X);
            }
        }

        return compared;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }
    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}
