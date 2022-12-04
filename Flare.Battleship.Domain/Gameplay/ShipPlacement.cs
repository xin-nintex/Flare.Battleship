using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;

namespace Flare.Battleship.Domain.Gameplay;

public record ShipPlacement(Ship Ship, Cell StartCell, Orientation Orientation, Swing Swing)
{
    public bool IsValid => CheckValidPlacement();

    public bool IsIntersecting(ShipPlacement other)
    {
        return AsCellSpan().Intersect(other.AsCellSpan()).Any();
    }

    public bool IsAtCell(Cell cell)
    {
        return AsCellSpan().Contains(cell);
    }

    private bool CheckValidPlacement()
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
                return false;
        }

        return true;
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
            yield return StartCell with { Column = (BoardColumn)i };
        }
    }

    private IEnumerable<Cell> EnumerateCellVertical(int start)
    {
        for (var i = start; i < Ship.Length; i++)
        {
            yield return StartCell with { Row = (BoardRow)i };
        }
    }
}
