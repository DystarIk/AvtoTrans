namespace AvtoTrans.Models;

public class FIO : IEquatable<FIO?>
{
    /// <summary>
    /// Фамилия
    /// </summary>
    public string F { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string I { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string O { get; set; }


    public FIO(string fIO)
    {
        var splitFIO = fIO.Split(' ');

        if (splitFIO.Length < 3) throw new Exception("Некорректный формат ФИО");

        F = splitFIO[0];
        I = splitFIO[1];
        O = splitFIO[2];
    }


    public bool Equals(FIO? other)
    {
        return other is not null &&
               F == other.F &&
               I == other.I &&
               O == other.O;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(F, I, O);
    }

    public override string? ToString()
    {
        return $"{F} {I} {O}";
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as FIO);
    }


    public static bool operator ==(FIO? left, FIO? right)
    {
        return EqualityComparer<FIO>.Default.Equals(left, right);
    }

    public static bool operator !=(FIO? left, FIO? right)
    {
        return !(left == right);
    }
}