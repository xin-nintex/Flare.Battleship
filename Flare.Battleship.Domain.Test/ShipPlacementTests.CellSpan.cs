using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;
using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Domain.Test;

public partial class ShipPlacementTests
{
    [Test]
    public void ShipPlacement_IsAn_EnumerationOfCells()
    {
        var ship = new Ship(Guid.NewGuid(), 2, "");
        var placement = new ShipPlacement(
            ship,
            new Cell(BoardColumn.A, BoardRow.One),
            Orientation.Horizontal,
            Swing.Right
        );
        Assert.That(
            placement,
            Is.EquivalentTo(
                new[]
                {
                    new Cell(BoardColumn.A, BoardRow.One),
                    new Cell(BoardColumn.B, BoardRow.One)
                }
            )
        );
    }

    [Test]
    public void ShipPlacement_Throws_WhenEnumeratingInvalid()
    {
        Assert.Throws<InvalidShipPlacementException>(
            ()=>new ShipPlacement(
                Ship.Battleship,
                new Cell(BoardColumn.C, BoardRow.Four),
                Orientation.Horizontal,
                Swing.Up
            ).ToArray()
        );
    }

    [TestCaseSource(nameof(TransformCases))]
    public void ShipPlacement_CanTransformTo_ListOfCells(
        int shipLength,
        Cell startCell,
        Orientation orientation,
        Swing swing,
        Cell[] equivalentList
    )
    {
        var ship = new Ship(Guid.NewGuid(), shipLength, "");
        var placement = new ShipPlacement(ship, startCell, orientation, swing);
        Assert.That(placement, Is.EquivalentTo(equivalentList));
    }

    private static readonly object[] TransformCases =
    {
        new object[]
        {
            2,
            new Cell(BoardColumn.A, BoardRow.One),
            Orientation.Horizontal,
            Swing.Right,
            new Cell[] { new(BoardColumn.A, BoardRow.One), new(BoardColumn.B, BoardRow.One) }
        },
        new object[]
        {
            3,
            new Cell(BoardColumn.D, BoardRow.Four),
            Orientation.Horizontal,
            Swing.Left,
            new Cell[]
            {
                new(BoardColumn.D, BoardRow.Four),
                new(BoardColumn.C, BoardRow.Four),
                new(BoardColumn.B, BoardRow.Four),
            }
        },
        new object[]
        {
            4,
            new Cell(BoardColumn.E, BoardRow.Four),
            Orientation.Horizontal,
            Swing.Left,
            new Cell[]
            {
                new(BoardColumn.D, BoardRow.Four),
                new(BoardColumn.C, BoardRow.Four),
                new(BoardColumn.B, BoardRow.Four),
                new(BoardColumn.E, BoardRow.Four)
            }
        },
        new object[]
        {
            4,
            new Cell(BoardColumn.E, BoardRow.Four),
            Orientation.Vertical,
            Swing.Up,
            new Cell[]
            {
                new(BoardColumn.E, BoardRow.Three),
                new(BoardColumn.E, BoardRow.Two),
                new(BoardColumn.E, BoardRow.Four),
                new(BoardColumn.E, BoardRow.One)
            }
        },
        new object[]
        {
            5,
            new Cell(BoardColumn.E, BoardRow.Ten),
            Orientation.Vertical,
            Swing.Up,
            new Cell[]
            {
                new(BoardColumn.E, BoardRow.Ten),
                new(BoardColumn.E, BoardRow.Nine),
                new(BoardColumn.E, BoardRow.Seven),
                new(BoardColumn.E, BoardRow.Eight),
                new(BoardColumn.E, BoardRow.Six)
            }
        },
        new object[]
        {
            5,
            new Cell(BoardColumn.E, BoardRow.Five),
            Orientation.Vertical,
            Swing.Down,
            new Cell[]
            {
                new(BoardColumn.E, BoardRow.Five),
                new(BoardColumn.E, BoardRow.Nine),
                new(BoardColumn.E, BoardRow.Seven),
                new(BoardColumn.E, BoardRow.Eight),
                new(BoardColumn.E, BoardRow.Six)
            }
        },
        new object[]
        {
            2,
            new Cell(BoardColumn.E, BoardRow.Five),
            Orientation.Vertical,
            Swing.Down,
            new Cell[] { new(BoardColumn.E, BoardRow.Five), new(BoardColumn.E, BoardRow.Six), }
        },
    };
}
