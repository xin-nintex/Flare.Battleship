
using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Domain.Test;

public partial class ShipPlacementTests
{
    [TestCaseSource(nameof(IntersectSource))]
    public void ShipPlacement_ShouldIntersect_Cell(ShipPlacement a, ShipPlacement b, bool result)
    {
        Assert.That(a.IsIntersecting(b), Is.EqualTo(result));
    }

    [TestCaseSource(nameof(CellAtSources))]
    public void ShipPlacement_ShouldBe_CoveringCell(ShipPlacement shipPlacement, IEnumerable<Cell> coveredCells, Cell randomUncoveredCell)
    {
        foreach (var cell in coveredCells)
        {
            Assert.That(shipPlacement.IsAtCell(cell), Is.True);
        }
        Assert.That(shipPlacement.IsAtCell(randomUncoveredCell), Is.False);
    }

    private static readonly object[] CellAtSources =
    {
        new object[]
        {
            new ShipPlacement(Ship.Battleship, new Cell(BoardColumn.A, BoardRow.One), Orientation.Horizontal, Swing.Right),
            new ShipPlacement(Ship.Battleship, new Cell(BoardColumn.A, BoardRow.One), Orientation.Horizontal, Swing.Right)
                .ToArray(),
            new Cell(BoardColumn.B, BoardRow.Four)
        }
    };

    private static readonly object[] IntersectSource =
    {
        new object[]
        {
            new ShipPlacement(Ship.Battleship, new Cell(BoardColumn.A, BoardRow.One), Orientation.Horizontal, Swing.Right),
            new ShipPlacement(Ship.Carrier, new Cell(BoardColumn.A, BoardRow.One), Orientation.Vertical, Swing.Down),
            true
        },
        new object[]
        {
            new ShipPlacement(Ship.Battleship, new Cell(BoardColumn.A, BoardRow.One), Orientation.Horizontal, Swing.Right),
            new ShipPlacement(Ship.Carrier, new Cell(BoardColumn.A, BoardRow.Two), Orientation.Horizontal, Swing.Right),
            false
        },
        new object[]
        {
            new ShipPlacement(Ship.Battleship, new Cell(BoardColumn.E, BoardRow.Three), Orientation.Horizontal, Swing.Right),
            new ShipPlacement(Ship.Carrier, new Cell(BoardColumn.F, BoardRow.Two), Orientation.Vertical, Swing.Down),
            true
        },
        new object[]
        {
            new ShipPlacement(Ship.Battleship, new Cell(BoardColumn.E, BoardRow.Three), Orientation.Horizontal, Swing.Right),
            new ShipPlacement(Ship.Carrier, new Cell(BoardColumn.D, BoardRow.Two), Orientation.Vertical, Swing.Down),
            false
        },
    };
}