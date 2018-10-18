using System;
using System.Collections.Generic;

public class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>(children);
    }

    public T Value { get; private set; }
    public IList<Tree<T>> Children { get; private set; }

    public void Print(int indent = 0)
    {
        Console.Write(new string(' ', 2 * indent));
        Console.WriteLine(this.Value);
        foreach (var child in this.Children)
        {
            child.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);

        foreach (var child in this.Children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        var elements = new List<T>();
        this.DPS(this, elements);

        return elements;
    }
    private void DPS (Tree<T> tree, List<T> elements)
    {
        foreach (var child in tree.Children)
        {
            this.DPS(child, elements);
        }

        elements.Add(tree.Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        var elements = new List<T>();
        var trees = new Queue<Tree<T>>();

        trees.Enqueue(this);

        while (trees.Count > 0)
        {
            var tree = trees.Dequeue();
            elements.Add(tree.Value);

            foreach (var child in tree.Children)
            {
                trees.Enqueue(child);
            }
        }

        return elements;
    }
}
