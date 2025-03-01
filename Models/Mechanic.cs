namespace AvtoTrans.Models;

public class Mechanic : IEquatable<Mechanic?>
{
    public FIO FIO { get; set; }
    public List<Request> Requests { get; set; } = new();


    public Mechanic(FIO fIO) => FIO = fIO;

    public Mechanic(string fIO):this(new FIO(fIO))
    {

    }


    public bool Equals(Mechanic? other)
    {
        return other is not null &&
               EqualityComparer<FIO>.Default.Equals(FIO, other.FIO);
    }

    public override string ToString()
    {
        return FIO.ToString()!;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FIO);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Mechanic);
    }


    public static bool operator ==(Mechanic? left, Mechanic? right)
    {
        return EqualityComparer<Mechanic>.Default.Equals(left, right);
    }

    public static bool operator !=(Mechanic? left, Mechanic? right)
    {
        return !(left == right);
    }
}