using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;

namespace Flare.Battleship.Domain.Gameplay;

public partial record ShipPlacement(Ship Ship, Cell StartCell, Orientation Orientation, Swing Swing)
{
    public bool IsValid => CheckValidPlacement();

    public bool IsIntersecting(ShipPlacement other)
    {
        return this.Intersect(other).Any();
    }

    public bool IsAtCell(Cell cell)
    {
        return this.Contains(cell);
    }

    private bool CheckValidPlacement()
    {
        var columnIndex = (int)StartCell.Column;
        var offset = Ship.Length - 1;
        var rowIndex = (int)StartCell.Row;
        switch (Orientation)
        {
            case Orientation.Horizontal when Swing == Swing.Left:
                if (columnIndex - offset < (int)BoardColumnEdge.LeftEdge)
                    goto default;
                break;
            case Orientation.Horizontal when Swing == Swing.Right:
                if (columnIndex + offset > (int)BoardColumnEdge.RightEdge)
                    goto default;
                break;
            case Orientation.Vertical when Swing == Swing.Up:
                if (rowIndex - offset < (int)BoardRowEdge.TopEdge)
                    goto default;
                break;
            case Orientation.Vertical when Swing == Swing.Down:
                if (rowIndex + offset > (int)BoardRowEdge.BottomEdge)
                    goto default;
                break;
            default:
                return false;
        }

        return true;
    }
}
