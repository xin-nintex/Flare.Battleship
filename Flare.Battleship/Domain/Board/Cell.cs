namespace Flare.Battleship.Domain.Board;

public readonly struct Cell : IEquatable<Cell>
{
    public BoardColumn Column { get; }
    public BoardRow Row { get; }

    public Cell(BoardColumn column, BoardRow row)
    {
        Column = column;
        Row = row;
    }

    public bool Equals(Cell other)
    {
        return Column == other.Column && Row == other.Row;
    }

    public override bool Equals(object? obj)
    {
        return obj is Cell other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Column, (int)Row);
    }

    public static bool operator ==(Cell left, Cell right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Cell left, Cell right)
    {
        return !(left == right);
    }
}