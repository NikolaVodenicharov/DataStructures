using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] map;

    private bool[,] walkableCells;
    private PriorityQueue<Node> queue;
    private Node goal;

    public AStar(char[,] map)
    {
        this.map = map;
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaColumn = Math.Abs(current.Col - goal.Col);
        var deltaRow = Math.Abs(current.Row - goal.Row);
        var H = deltaColumn + deltaRow;

        return H;
    }

    public IEnumerable<Node> GetPath(Node start, Node goalPosition)
    {
        InitializeHelperFields(start, goalPosition);
        var goalNode = FindGoalNode(goalPosition);
        var path = RenderPath(goalNode);

        return path;
    }

    private void InitializeHelperFields(Node start, Node goalPosition)
    {
        this.walkableCells = InitializeWalkableCells(start);
        this.goal = goalPosition;
        this.queue = new PriorityQueue<Node>();

        start.H = GetH(start, goalPosition);
        this.queue.Enqueue(start);
    }
    private bool[,] InitializeWalkableCells(Node start)
    {
        var matrix = new bool[map.GetLength(0), map.GetLength(1)];

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                var isWalkable = map[row, col] != 'W';
                matrix[row, col] = isWalkable;
            }
        }

        matrix[start.Row, start.Col] = false;

        return matrix;
    }

    private Node FindGoalNode(Node goal)
    {
        Node current = null;

        while (true)
        {
            if (this.queue.Count == 0)
            {
                break;
            }

            current = this.queue.Dequeue();
            if (current.Equals(goal))
            {
                break;
            }

            this.ExpandCellAllDirecdion(current);
        }

        return current;
    }
    private static List<Node> RenderPath(Node current)
    {
        var path = new List<Node>();

        while (current != null)
        {
            path.Add(current);
            current = current.PreviousNode;
        }

        path.Reverse();

        return path;
    }

    private void ExpandCellAllDirecdion(Node node)
    {
        this.ExpandCellUp(node);
        this.ExpandCellRight(node);
        this.ExpandCellDown(node);
        this.ExpandCellLeft(node);
    }
    private void ExpandCellUp(Node parent)
    {
        var previousRow = parent.Row - 1;       
        if (previousRow >= 0)
        {
            this.AddNewNode(parent, previousRow, parent.Col);
        }
    }
    private void ExpandCellRight(Node parent)
    {
        var nextColumn = parent.Col + 1;
        if (nextColumn < map.GetLength(1))
        {
            this.AddNewNode(parent, parent.Row, nextColumn);
        }
    }
    private void ExpandCellDown(Node parent)
    {
        var nextRow = parent.Row + 1;
        if (nextRow < map.GetLength(0))
        {
            this.AddNewNode(parent, nextRow, parent.Col);
        }
    }
    private void ExpandCellLeft(Node parent)
    {
        var previousColumn = parent.Col - 1;
        if (previousColumn >= 0)
        {
            this.AddNewNode(parent, parent.Row, previousColumn);
        }
    }
    private void AddNewNode(Node parent, int row, int col)
    {
        var isWalkable = this.walkableCells[row, col];
        if (!isWalkable)
        {
            return;
        }

        this.walkableCells[row, col] = false;

        var child = new Node(row, col)
        {
            G = parent.G + 1,
            PreviousNode = parent
        };
        child.H = GetH(child, this.goal);

        queue.Enqueue(child);
    }
}

