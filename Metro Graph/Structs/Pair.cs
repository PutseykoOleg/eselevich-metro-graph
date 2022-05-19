namespace Metro.Structs;

public struct Pair<F, S>
{
    public F First { get; set; }
    public S Second { get; set; }

    public Pair(F first, S second)
    {
        this.First = first;
        this.Second = second;
    }
}

