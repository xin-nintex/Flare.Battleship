using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Domain.Test;

public partial class ShipPlacementTests
{
    [TestCase(3, 9, false)]
    [TestCase(4, 8, false)]
    [TestCase(5, 7, false)]
    [TestCase(5, 5, true)]
    [TestCase(2, 1, true)]
    [TestCase(10, 1, true)]
    [TestCase(100, 1, false)]
    public void Ship_Cannot_BePlaced_OutSide_TheBoard_Horizontal_SwingRight(int shipLength, BoardColumn startColumn, bool isValid)
    {
        var ship = new Ship(Guid.NewGuid(), shipLength, "");
        var placement = new ShipPlacement(ship, new Cell(startColumn, BoardRow.Eight),
            Orientation.Horizontal, Swing.Right);
        Assert.That(placement.IsValid, Is.EqualTo(isValid));
    }
    [TestCase(2, 9, true)]
    [TestCase(3, 8, true)]
    [TestCase(4, 7, true)]
    [TestCase(5, 5, true)]
    [TestCase(2, 1, false)]
    [TestCase(10, 1, false)]
    public void Ship_Cannot_BePlaced_OutSide_TheBoard_Horizontal_SwingLeft(int shipLength, BoardColumn startColumn, bool isValid)
    {
        var ship = new Ship(Guid.NewGuid(), shipLength, "");
        var placement = new ShipPlacement(ship, new Cell(startColumn, BoardRow.Eight),
            Orientation.Horizontal, Swing.Left);
        Assert.That(placement.IsValid, Is.EqualTo(isValid));
    }
    [TestCase(2, 9, true)]
    [TestCase(3, 8, true)]
    [TestCase(4, 7, true)]
    [TestCase(5, 5, true)]
    [TestCase(2, 1, false)]
    [TestCase(10, 1, false)]
    [TestCase(4, 2, false)]
    public void Ship_Cannot_BePlaced_OutSide_TheBoard_Vertical_SwingUp(int shipLength, BoardRow startRow, bool isValid)
    {
        var ship = new Ship(Guid.NewGuid(), shipLength, "");
        var placement = new ShipPlacement(ship, new Cell(BoardColumn.A, startRow),
            Orientation.Vertical, Swing.Up);
        Assert.That(placement.IsValid, Is.EqualTo(isValid));
    }
    [TestCase(2, 9, true)]
    [TestCase(3, 8, true)]
    [TestCase(4, 7, true)]
    [TestCase(5, 5, true)]
    [TestCase(2, 10, false)]
    [TestCase(10, 2, false)]
    [TestCase(4, 8, false)]
    public void Ship_Cannot_BePlaced_OutSide_TheBoard_Vertical_SwingDown(int shipLength, BoardRow startRow, bool isValid)
    {
        var ship = new Ship(Guid.NewGuid(), shipLength, "");
        var placement = new ShipPlacement(ship, new Cell(BoardColumn.A, startRow),
            Orientation.Vertical, Swing.Down);
        Assert.That(placement.IsValid, Is.EqualTo(isValid));
    }
}