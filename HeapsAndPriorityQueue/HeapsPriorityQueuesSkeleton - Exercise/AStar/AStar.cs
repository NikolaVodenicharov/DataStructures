using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] map;
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

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        this.goal = goal;
        this.queue = queue = new PriorityQueue<Node>();

        start.H = GetH(start, goal);
        this.queue.Enqueue(start);

        Node current = null;

        while (true)
        {
            if (this.queue.Count == 0)
            {
                throw new InvalidOperationException("There is no path from start position to goal.");
            }

            current = this.queue.Dequeue();
            if (current.Equals(goal))
            {
                break;
            }

            this.ExpandCellAllDirecdion(current);
        }

        var path = new List<Node>();
        while (current != null)
        {
            path.Add(current);
            current = current.PreviousNode;
        }

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

        if (previousRow >= 0 &&
            (map[previousRow, parent.Col] == '-' ||
             map[previousRow, parent.Col] == '*'))
        {
            this.AddNewNode(parent, previousRow, parent.Col);

            this.SetCellToBusy(previousRow, parent.Col);
        }
    }
    private void ExpandCellRight(Node parent)
    {
        var nextColumn = parent.Col + 1;

        if (nextColumn < map.GetLength(1) &&
            (map[parent.Row, nextColumn] == '-' ||
             map[parent.Row, nextColumn] == '*'))
        {
            this.AddNewNode(parent, parent.Row, nextColumn);
            this.SetCellToBusy(parent.Row, nextColumn);
        }
    }
    private void ExpandCellDown(Node parent)
    {
        var nextRow = parent.Row + 1;

        if (nextRow < map.GetLength(0) &&
            (map[nextRow, parent.Col] == '-' ||
             map[nextRow, parent.Col] == '*'))
        {
            this.AddNewNode(parent, nextRow, parent.Col);
            this.SetCellToBusy(nextRow, parent.Col);
        }
    }
    private void ExpandCellLeft(Node parent)
    {
        var previousColumn = parent.Col - 1;

        if (previousColumn >= 0 &&
            (map[parent.Row, previousColumn] == '-' ||
             map[parent.Row, previousColumn] == '*'))
        {
            this.AddNewNode(parent, parent.Row, previousColumn);
            this.SetCellToBusy(parent.Row, previousColumn);
        }
    }
    private void AddNewNode(Node parent, int row, int col)
    {
        var child = new Node(row, col);

        child.G = parent.G + 1;
        child.H = GetH(child, this.goal);
        child.PreviousNode = parent;

        queue.Enqueue(child);
    }
    private void SetCellToBusy(int row, int col)
    {
        map[row, col] = 'N';
    }
}

