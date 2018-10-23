using System;
using System.Collections.Generic;

public class Parent<T> where T : IComparable<T>
{
    private List<T> heap;

    public Parent()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }
    private void HeapifyUp(int index)
    {
        while (index > 0 && IsLess(this.FindParentIndex(index), index))
        {
            this.Swap(this.FindParentIndex(index), index);
            index = this.FindParentIndex(index);
        }
    }
    private int FindParentIndex(int childIndex)
    {
        return (childIndex - 1) / 2;
    }
    private bool IsLess(int index1, int index2)
    {
        return this.heap[index1].CompareTo(this.heap[index2]) < 0;
    }
    private void Swap(int index1, int index2)
    {
        var temp = this.heap[index1];
        this.heap[index1] = this.heap[index2];
        this.heap[index2] = temp;
    }

    public T Peek()
    {
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.heap.Count <= 0)
        {
            throw new InvalidOperationException("The collection is empty.");
        }

        var element = this.heap[0];
        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);

        return element;
    }
    private void HeapifyDown(int parentIndex)
    {
        while (true)
        {
            if (!this.IsParent(parentIndex))
            {
                break;
            }

            int biggerChildIndex = this.FindBiggerChildIndex(parentIndex);

            if (this.IsLess(biggerChildIndex, parentIndex))
            {
                break;
            }

            this.Swap(parentIndex, biggerChildIndex);
            parentIndex = biggerChildIndex;
        }
    }
    private bool IsParent(int index)
    {
        int lastParentIndex = this.heap.Count / 2 - 1;
        return index <= lastParentIndex;
    }
    private int FindBiggerChildIndex(int parentIndex)
    {
        var leftChildIndex = this.FindLeftChildIndex(parentIndex);

        if (!this.IsIndexInRange(leftChildIndex))
        {
            throw new InvalidOperationException("The element don not have any child.");
        }

        var rightChildIndex = this.FindRightChildIndex(parentIndex);

        if (this.IsIndexInRange(rightChildIndex) &&
            this.IsLess(leftChildIndex, rightChildIndex))
        {
            return rightChildIndex;
        }

        return leftChildIndex;
    }
    private int FindLeftChildIndex(int parentIndex)
    {
        return 2 * parentIndex + 1;
    }
    private int FindRightChildIndex(int parentIndex)
    {
        return 2 * parentIndex + 1;
    }
    private bool IsIndexInRange(int index)
    {
        return index < this.heap.Count;
    }
}
