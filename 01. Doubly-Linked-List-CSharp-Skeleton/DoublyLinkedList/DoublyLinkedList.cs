using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private ListNode<T> head;
    private ListNode<T> tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var inserted = new ListNode<T>(element);
            inserted.NextNode = this.head;
            this.head.PrevNode = inserted;
            this.head = inserted;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var inserted = new ListNode<T>(element);
            inserted.PrevNode = this.tail;
            this.tail.NextNode = inserted;
            this.tail = inserted;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty");
        }

        var value = this.head.Value;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head = this.head.NextNode;
            this.head.PrevNode = null;
        }

        this.Count--;
        return value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List is empty");
        }

        var value = this.tail.Value;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.tail = this.tail.PrevNode;
            this.tail.NextNode = null;
        }

        this.Count--;
        return value;
    }

    public void ForEach(Action<T> action)
    {
        var current = this.head;

        for (int i = 0; i < this.Count; i++)
        {
            action(current.Value);

            current = current.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.head;

        for (int i = 0; i < this.Count; i++)
        {
            yield return current.Value;

            current = current.NextNode;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        var elements = new T[this.Count];

        var current = this.head;
        for (int i = 0; i < this.Count; i++)
        {
            elements[i] = current.Value;

            current = current.NextNode;
        }

        return elements;
    }


    private class ListNode<T>
    {
        public ListNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }
        public ListNode<T> PrevNode { get; set; }
        public ListNode<T> NextNode { get; set; }
    }
}
