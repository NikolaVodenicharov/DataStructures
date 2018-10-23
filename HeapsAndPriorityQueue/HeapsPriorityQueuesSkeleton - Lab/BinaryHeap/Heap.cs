using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        RearrangeArrayToHeap(arr);
        OrderAscending(arr);
    }

    private static void RearrangeArrayToHeap(T[] arr)
    {
        var length = arr.Length;
        var lastParentIndex = length / 2 - 1;

        for (int parentIndex = lastParentIndex; parentIndex >= 0; parentIndex--)
        {
            Heapify(arr, length, parentIndex);
        }
    }
    private static void Heapify(T[] arr, int length, int parentIndex)
    {
        var largestElementIndex = parentIndex;
        var leftChildIndex = FindLeftChildIndex(parentIndex);
        var rightChildIndex = FindRightChildIndex(parentIndex);

        if (IsIndexInRange(leftChildIndex, length) &&
            IsLess(largestElementIndex, leftChildIndex, arr))
        {
            largestElementIndex = leftChildIndex;
        }

        if (IsIndexInRange(rightChildIndex, length) &&
            IsLess(largestElementIndex, rightChildIndex, arr))
        {
            largestElementIndex = rightChildIndex;
        }

        if (largestElementIndex != parentIndex)
        {
            SwapElements(arr, parentIndex, largestElementIndex);
            Heapify(arr, length, largestElementIndex);
        }
    }
    private static int FindLeftChildIndex(int parentIndex)
    {
        return 2 * parentIndex + 1;
    }
    private static int FindRightChildIndex(int parentIndex)
    {
        return 2 * parentIndex + 2;
    }
    private static bool IsIndexInRange(int index, int length)
    {
        return index < length;
    }
    private static bool IsLess(int index1, int index2, T[] arr)
    {
        return arr[index1].CompareTo(arr[index2]) < 0;
    }
    private static void SwapElements(T[] arr, int index1, int index2)
    {
        var temporary = arr[index1];
        arr[index1] = arr[index2];
        arr[index2] = temporary;
    }

    private static void OrderAscending(T[] arr)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            SwapElements(arr, 0, i);
            Heapify(arr, i, 0);
        }
    }
}
