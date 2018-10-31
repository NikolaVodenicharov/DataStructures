using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private const string GreaterCountMessage = "The given count is greater than the elements count in the collection.";

    private LinkedList<T> insertOrder;
    private OrderedBag<LinkedListNode<T>> minValueOrder;
    private OrderedBag<LinkedListNode<T>> maxValueOrder;

    public FirstLastList()
    {
        this.insertOrder = new LinkedList<T>();
        this.minValueOrder = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.maxValueOrder = new OrderedBag<LinkedListNode<T>>((x, y) => -x.Value.CompareTo(y.Value));
    }

    public int Count
    {
        get
        {
            return this.insertOrder.Count;
        }
    }

    public void Add(T element)
    {
        var node = new LinkedListNode<T>(element);
        this.insertOrder.AddLast(node);
        this.minValueOrder.Add(node);
        this.maxValueOrder.Add(node);
    }

    public void Clear()
    {
        this.insertOrder.Clear();
        this.minValueOrder.Clear();
        this.maxValueOrder.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        this.CheckCountThrowException(count);

        return this.insertOrder.Take(count);
    }
    private void CheckCountThrowException(int count)
    {
        if (count > this.insertOrder.Count)
        {
            throw new ArgumentOutOfRangeException(GreaterCountMessage);
        }
    }

    public IEnumerable<T> Last(int count)
    {
        this.CheckCountThrowException(count);

        var lastElements = this.insertOrder
            .Skip(this.insertOrder.Count - count)
            .Take(count)
            .ToList();

        lastElements.Reverse();

        return lastElements;
    }

    public IEnumerable<T> Max(int count)
    {
        this.CheckCountThrowException(count);

        return this.maxValueOrder.Take(count).Select(n => n.Value);
    }

    public IEnumerable<T> Min(int count)
    {
        this.CheckCountThrowException(count);

        return this.minValueOrder.Take(count).Select(n => n.Value);
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);
        var range = this.minValueOrder.Range(node, true, node, true);

        foreach (var item in range)
        {
            this.insertOrder.Remove(item);
        }

        var count = this.minValueOrder.RemoveAllCopies(node);
        this.maxValueOrder.RemoveAllCopies(node);

        return count;
    }
}
