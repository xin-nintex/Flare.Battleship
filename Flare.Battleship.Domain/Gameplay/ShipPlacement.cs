using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;

namespace Flare.Battleship.Domain.Gameplay;

public readonly struct ShipPlacement : IEquatable<ShipPlacement>
{
    public Ship Ship { get; }
    private Cell StartCell { get; }
    private Orientation Orientation { get; }
    private Swing Swing { get; }

    public ShipPlacement(Ship ship, Cell startCell, Orientation orientation, Swing swing)
    {
        Ship = ship;
        StartCell = startCell;
        Orientation = orientation;
        Swing = swing;
        CheckValidPlacement();
    }

    public bool IsIntersecting(ShipPlacement other)
    {
        return AsCellSpan().Intersect(other.AsCellSpan()).Any();
    }

    public bool IsAtCell(Cell cell)
    {
        return AsCellSpan().Contains(cell);
    }

    private void CheckValidPlacement()
    {
        var columnIndex = (int)StartCell.Column;
        var offset = Ship.Length;
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

    private IEnumerable<Cell> AsCellSpan()
    {
        switch (Orientation)
        {
            case Orientation.Horizontal when Swing == Swing.Left:
                foreach (var cell in EnumerateCellHorizontal((int)StartCell.Column - Ship.Length))
                    yield return cell;
                break;
            case Orientation.Horizontal when Swing == Swing.Right:
                foreach (var cell in EnumerateCellHorizontal((int)StartCell.Column))
                    yield return cell;
                break;
            case Orientation.Vertical when Swing == Swing.Up:
                foreach (var cell in EnumerateCellVertical((int)StartCell.Row - Ship.Length))
                    yield return cell;
                break;
            case Orientation.Vertical when Swing == Swing.Down:
                foreach (var cell in EnumerateCellVertical((int)StartCell.Row))
                    yield return cell;
                break;
            default:
                throw new InvalidShipPlacementException($"{Ship} placement is invalid");
        }
    }

    private IEnumerable<Cell> EnumerateCellHorizontal(int start)
    {
        for (var i = start; i < Ship.Length; i++)
        {
            yield return new Cell((BoardColumn)i, StartCell.Row);
        }
    }

    private IEnumerable<Cell> EnumerateCellVertical(int start)
    {
        for (var i = start; i < Ship.Length; i++)
        {
            yield return new Cell(StartCell.Column, (BoardRow)i);
        }
    }

    #region Equality
    public bool Equals(ShipPlacement other)
    {
        return Ship == other.Ship
            && StartCell.Equals(other.StartCell)
            && Orientation == other.Orientation
            && Swing == other.Swing;
    }

    public override bool Equals(object? obj)
    {
        return obj is ShipPlacement other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Ship, StartCell, (int)Orientation, (int)Swing);
    }

    public static bool operator ==(ShipPlacement left, ShipPlacement right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ShipPlacement left, ShipPlacement right)
    {
        return !(left == right);
    }
    #endregion
}
