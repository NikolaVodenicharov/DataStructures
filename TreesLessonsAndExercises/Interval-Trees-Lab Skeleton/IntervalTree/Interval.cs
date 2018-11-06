using System;

public class Interval
{
    public double Lo { get; set; }
    public double Hi { get; set; }

    public Interval(double lo, double hi)
    {
        ValidateInterval(lo, hi);
        this.Lo = lo;
        this.Hi = hi;
    }

    public bool Intersects(double lo, double hi)
    {
        ValidateInterval(lo, hi);
        return this.Lo < hi && this.Hi > lo;
    }

    public override bool Equals(object obj)
    {
        if (obj == this)
        {
            return true;
        }
        else if (obj == null)
        {
            return false;
        }
        else if (obj.GetType() != this.GetType())
        {
            return false;
        }

        var other = (Interval)obj;
        return this.Lo == other.Lo && this.Hi == other.Hi;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", this.Lo, this.Hi);
    }

    private static void ValidateInterval(double lo, double hi)
    {
        if (hi < lo)
        {
            throw new ArgumentException($"Lower border of the interval ({lo}) is bigger than Higher border ({hi}).");
        }
    }
}
