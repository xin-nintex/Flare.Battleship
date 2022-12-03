using Flare.Battleship.Domain.Board;

namespace Flare.Battleship.Domain.Gameplay;

public readonly struct AttackPlacement : IEquatable<AttackPlacement>
{
    public Cell Cell { get; }
    public OccupationType Type { get; }
    
    public AttackPlacement(Cell cell, OccupationType type)
    {
        Cell = cell;
        Type = type;
    }

    public bool Equals(AttackPlacement other)
    {
        return Cell.Equals(other.Cell) && Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        return obj is AttackPlacement other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Cell, (int)Type);
    }

    public static bool operator ==(AttackPlacement left, AttackPlacement right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(AttackPlacement left, AttackPlacement right)
    {
        return !(left == right);
    }
}