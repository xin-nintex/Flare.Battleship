namespace Flare.Battleship.Domain.Battleships;

public readonly struct Ship : IEquatable<Ship>
{
    public ShipType ShipType { get; }
    public int Length { get; }

    private Ship(ShipType shipType, int length)
    {
        ShipType = shipType;
        Length = length;
    }

    public static Ship Destroyer => new(ShipType.Destroyer, 2);
    public static Ship Submarine => new(ShipType.Submarine, 3);
    public static Ship Cruiser => new(ShipType.Cruiser, 3);
    public static Ship Battleship => new(ShipType.Battleship, 4);
    public static Ship Carrier => new(ShipType.Carrier, 5);

    public static IEnumerable<Ship> AllAvailableShips
    {
        get
        {
            yield return Destroyer;
            yield return Submarine;
            yield return Carrier;
            yield return Battleship;
            yield return Cruiser;
        }
    }

    public bool Equals(Ship other)
    {
        return ShipType == other.ShipType && Length == other.Length;
    }

    public override bool Equals(object? obj)
    {
        return obj is Ship other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)ShipType, Length);
    }

    public static bool operator ==(Ship left, Ship right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Ship left, Ship right)
    {
        return !(left == right);
    }
}
