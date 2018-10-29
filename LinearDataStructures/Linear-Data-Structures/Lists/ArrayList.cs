using System;

public class ArrayList<T>
{
    private const int ResizeCapacityCoefficient = 2;
    private const int InitialCapacity = 4;

    private T[] elements = new T[InitialCapacity];

    public int Capacity { get; private set; } = InitialCapacity;

    public int Count { get; private set; } = 0;
    private int CurrentIndex => this.Count - 1;
    private int NextIndex => this.Count;

    public T this[int index]
    {
        get
        {
            this.CheckIsOutOfRange(index);
            return this.elements[index];
        }

        set
        {
            this.CheckIsOutOfRange(index);
            this.elements[index] = value;
        }
    }

    public T RemoveAt(int index)
    {
        this.CheckIsOutOfRange(index);

        var element = this.elements[index];

        for (int i = index; i < this.NextIndex - 1; i++)
        {
            this.elements[i] = this.elements[i + 1];
        }
        this.Count--;

        if (this.IsCapacityForDecreasing())
        {
            this.DecreaseCapacity();
        }

        return element;
    }
    private void CheckIsOutOfRange(int index)
    {
        if (index > this.CurrentIndex)
        {
            throw new ArgumentOutOfRangeException($"{index} is bigger than maximum indexed element in the collection.");
        }
    }
    private bool IsCapacityForDecreasing()
    {
        return this.Count <= (this.Capacity / 4);
    }
    private void DecreaseCapacity()
    {
        this.Capacity = this.Capacity / ResizeCapacityCoefficient;
        T[] decreased = new T[this.Capacity];
        Array.Copy(this.elements, decreased, this.NextIndex);
        this.elements = decreased;
    }

    public void Add(T item)
    {
        if (this.IsCapacityFull())
        {
            this.IncreaseCapacity();
        }

        this.elements[this.NextIndex] = item;
        this.Count++;
    }
    private bool IsCapacityFull ()
    {
        return this.Capacity == this.Count;
    }
    private void IncreaseCapacity()
    {
        this.Capacity = this.Capacity * ResizeCapacityCoefficient;
        T[] increased = new T[this.Capacity];
        Array.Copy(this.elements, increased, this.NextIndex);
        this.elements = increased;
    }
}
