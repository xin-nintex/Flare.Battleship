using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;

namespace Flare.Battleship.Domain.Gameplay;

public readonly struct ShipPlacement : IEquatable<ShipPlacement>
{
    public Ship Ship { get; }
    public Cell StartCell { get; }
    public Orientation Orientation { get; }
    public Swing Swing { get; }

    public ShipPlacement(Ship ship,  Cell startCell, Orientation orientation, Swing swing)
    {
        Ship = ship;
        StartCell = startCell;
        Orientation = orientation;
        Swing = swing;
        CheckValidPlacement();
    }

    private void CheckValidPlacement()
    {
        var columnIndex = (int)StartCell.Column;
        var offset = (int)Ship;
        var rowIndex = (int)StartCell.Row;
        switch (Orientation)
        {
            case Orientation.Horizontal when Swing == Swing.Left:
                if (columnIndex - offset < (int)BoardColumn.LeftEdge)
                    goto default;
                break;
            case Orientation.Horizontal when Swing == Swing.Right:
                if (columnIndex + offset > (int)BoardColumn.RightEdge)
                    goto default;
                break;
            case Orientation.Vertical when Swing == Swing.Up:
                if (rowIndex - offset < (int)BoardRow.TopEdge)
                    goto default;
                break;
            case Orientation.Vertical when Swing == Swing.Down:
                if (rowIndex + offset > (int)BoardRow.BottomEdge)
                    goto default;
                break;
            default:
                throw new InvalidShipPlacementException($"{Ship} cannot be placed");
        }
    }

    public bool Equals(ShipPlacement other)
    {
        return Ship == other.Ship && StartCell.Equals(other.StartCell) && Orientation == other.Orientation && Swing == other.Swing;
    }

    public override bool Equals(object? obj)
    {
        return obj is ShipPlacement other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Ship, StartCell, (int)Orientation, (int)Swing);
    }

    public static bool operator ==(ShipPlacement left, ShipPlacement right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ShipPlacement left, ShipPlacement right)
    {
        return !(left == right);
    }
}