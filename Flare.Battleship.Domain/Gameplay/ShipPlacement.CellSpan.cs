using System.Collections;
using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;

namespace Flare.Battleship.Domain.Gameplay;

public partial record ShipPlacement : IEnumerable<Cell>
{
    private IEnumerable<Cell> AsCellSpan()
    {
        switch (Orientation)
        {
            case Orientation.Horizontal or Orientation.Vertical when !IsValid:
                goto default;
            case Orientation.Horizontal when Swing == Swing.Left:
                foreach (var cell in EnumerateCellHorizontal((int)StartCell.Column - Ship.Length + 1))
                    yield return cell;
                break;
            case Orientation.Horizontal when Swing == Swing.Right:
                foreach (var cell in EnumerateCellHorizontal((int)StartCell.Column))
                    yield return cell;
                break;
            case Orientation.Vertical when Swing == Swing.Up:
                foreach (var cell in EnumerateCellVertical((int)StartCell.Row - Ship.Length + 1))
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
        for (var i = start; i < start + Ship.Length; i++)
        {
            yield return StartCell with { Column = (BoardColumn)i };
        }
    }

    private IEnumerable<Cell> EnumerateCellVertical(int start)
    {
        for (var i = start; i < start + Ship.Length; i++)
        {
            yield return StartCell with { Row = (BoardRow)i };
        }
    }

    public IEnumerator<Cell> GetEnumerator()
    {
        return AsCellSpan().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}