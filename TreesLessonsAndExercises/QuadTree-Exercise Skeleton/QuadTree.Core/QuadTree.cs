using System;
using System.Collections.Generic;
using System.Linq;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;
    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }
    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfsRecursively(this.root, action);
    }
    private void ForEachDfsRecursively(
        Node<T> node, 
        Action<List<T>, int, int> action, 
        int depth = 1, 
        int quadrant = 0)
    {
        if (node == null)
        {
            return;
        }

        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                ForEachDfsRecursively(node.Children[i], action, depth + 1, i);
            }
        }
    }

    public bool Insert(T item)
    {
        if (!item.Bounds.IsInside(this.Bounds))
        {
            return false;
        }

        var depth = 1;
        var current = this.root;
        while (current.Children != null)
        {
            var quadrant = this.GetQuadrant(current, item.Bounds);
            if (quadrant == -1)
            {
                break;
            }

            current = current.Children[quadrant];
            depth++;
        }

        current.Items.Add(item);
        this.TrySplit(current, depth);
        this.Count++;

        return true;
    }
    private int GetQuadrant(Node<T> node, Rectangle bounds)
    {
        var isTop = bounds.Y2 <= node.Bounds.MidY;
        var isBottom = bounds.Y1 >= node.Bounds.MidY;
        var isLeft = bounds.X2 <= node.Bounds.MidX;
        var isRight = bounds.X1 >= node.Bounds.MidY;

        var result = -1;

        if (isTop)
        {
            if (isRight)
            {
                result = 0;
            }
            else if (isLeft)
            {
                result = 1;
            }
        }
        else if (isBottom)
        {
            if (isLeft)
            {
                result = 2;
            }
            else if (isRight)
            {
                result = 3;
            }
        }

        return result;
    }
    private void TrySplit(Node<T> node, int depth)
    {
        if (depth >= this.MaxDepth)
        {
            return;
        }

        if (!node.ShouldSplit)
        {
            return;
        }

        InitializeNodeChildren(node);
        MoveItemsToChildren(node);
        TrySplitChildren(node, depth);
    }
    private void InitializeNodeChildren(Node<T> node)
    {
        var halfWidth = node.Bounds.Width / 2;
        var halfHeight = node.Bounds.Height / 2;

        var topRightQuadrant = new Node<T>(
            node.Bounds.MidX,
            node.Bounds.Y1,
            halfWidth,
            halfHeight);

        var topLeftQuadrant = new Node<T>(
            node.Bounds.X1,
            node.Bounds.Y1,
            halfWidth,
            halfHeight);

        var bottomLeftQuadrant = new Node<T>(
            node.Bounds.X1,
            node.Bounds.MidY,
            halfWidth,
            halfHeight);

        var bottomRightQuadrant = new Node<T>(
            node.Bounds.MidX,
            node.Bounds.MidY,
            halfWidth,
            halfHeight);

        node.Children = new Node<T>[]
        {
            topRightQuadrant,
            topLeftQuadrant,
            bottomLeftQuadrant,
            bottomRightQuadrant
        };
    }
    private void MoveItemsToChildren(Node<T> node)
    {
        for (int i = node.Items.Count - 1; i >= 0; i--)
        {
            var item = node.Items[i];
            var quadrant = this.GetQuadrant(node, item.Bounds);

            if (quadrant == -1)
            {
                continue;
            }

            node.Children[quadrant].Items.Add(item);
            node.Items.RemoveAt(i);
        }
    }
    private void TrySplitChildren(Node<T> node, int depth)
    {
        foreach (var child in node.Children)
        {
            this.TrySplit(child, depth + 1);
        }
    }

    public List<T> Report(Rectangle bounds)
    {
        var collisionCandidates = new List<T>();
        this.GetCollisionCandidates(this.root, bounds, collisionCandidates);
        return collisionCandidates;
    }
    private void GetCollisionCandidates(Node<T> node, Rectangle bounds, List<T> results)
    {
        var quadrant = this.GetQuadrant(node, bounds);

        if (quadrant == -1)
        {
            this.GetSubtreeContent(node, bounds, results);
        }
        else
        {
            if (node.Children != null)
            {
                this.GetCollisionCandidates(node.Children[quadrant], bounds, results);
            }

            results.AddRange(node.Items);
        }
    }
    private void GetSubtreeContent(Node<T> node, Rectangle bounds, List<T> results)
    {
        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                if (child.Bounds.Intersects(bounds))
                {
                    this.GetSubtreeContent(child, bounds, results);
                }
            }
        }

        results.AddRange(node.Items);
    }

}
