using System;
using System.Collections;
using System.Collections.Generic;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;
    private const float ResizeFactor = 2;
    private const float LoadFactor = 0.75f;

    private LinkedList<KeyValue<TKey, TValue>>[] slots;
    private int maxElements;

    public int Count { get; private set; }
    public int Capacity => this.slots.Length;

    public HashTable(int capacity = DefaultCapacity)
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.maxElements = (int)(capacity * LoadFactor);
    }

    public void Add(TKey key, TValue value)
    {
        GrowIfNeeded();

        int slotIndex = this.FindSlotIndex(key);

        if (this.slots[slotIndex] == null)
        {
            this.slots[slotIndex] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var kvp in this.slots[slotIndex])
        {
            if (kvp.Key.Equals(key))
            {
                throw new ArgumentException("Element is already in the colelction.");
            }
        }

        this.slots[slotIndex].AddLast(new KeyValue<TKey, TValue>(key, value));

        this.Count++;
    }
    public bool AddOrReplace(TKey key, TValue value)
    {
        GrowIfNeeded();

        int slotIndex = this.FindSlotIndex(key);

        if (this.slots[slotIndex] == null)
        {
            this.slots[slotIndex] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var kvp in this.slots[slotIndex])
        {
            if (kvp.Key.Equals(key))
            {
                kvp.Value = value;
                return false;
            }
        }

        this.slots[slotIndex].AddLast(new KeyValue<TKey, TValue>(key, value));
        this.Count++;

        return true;
    }

    private void GrowIfNeeded()
    {
        if (this.IsGrowNeeded())
        {
            this.Grow();
        }
    }
    private bool IsGrowNeeded()
    {
        return (float)(this.Count + 1) / this.slots.Length > LoadFactor;
    }
    private void Grow()
    {
        var resized = new HashTable<TKey, TValue>((int)(this.slots.Length * ResizeFactor));
        foreach (var kvp in this)
        {
            resized.Add(kvp.Key, kvp.Value);
        }

        this.slots = resized.slots;
        this.Count = resized.Count;
    }

    private int FindSlotIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % this.slots.Length;
    }

    public TValue Get(TKey key)
    {
        var kvp = this.Find(key);
        if (kvp == null)
        {
            throw new KeyNotFoundException();
        }

        return kvp.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return this.Get(key);
        }
        set
        {
            this.AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var kvp = this.Find(key);
        if (kvp != null)
        {
            value = kvp.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        var slotIndex = this.FindSlotIndex(key);
        var linkedList = this.slots[slotIndex];
        if (linkedList != null)
        {
            foreach (var kvp in linkedList)
            {
                if (kvp.Key.Equals(key))
                {
                    return kvp;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        return this.Find(key) != null;
    }

    public bool Remove(TKey key)
    {
        var slotIndex = this.FindSlotIndex(key);
        var linkedList = this.slots[slotIndex];
        if (linkedList != null)
        {
            foreach (var kvp in linkedList)
            {
                if (kvp.Key.Equals(key))
                {
                    linkedList.Remove(kvp);
                    this.Count--;
                    return true;
                }
            }
        }

        return false;
    }

    public void Clear()
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[this.slots.Length];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            foreach (var linkedList in this.slots)
            {
                if (linkedList != null)
                {
                    foreach (var kvp in linkedList)
                    {
                        yield return kvp.Key;
                    }
                }
            }
        }
    }
    public IEnumerable<TValue> Values
    {
        get
        {
            foreach (var linkedList in this.slots)
            {
                if (linkedList != null)
                {
                    foreach (var kvp in linkedList)
                    {
                        yield return kvp.Value;
                    }
                }
            }
        }
    }
    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var linkedList in this.slots)
        {
            if (linkedList != null)
            {
                foreach (var kvp in linkedList)
                {
                    yield return kvp;
                }
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
