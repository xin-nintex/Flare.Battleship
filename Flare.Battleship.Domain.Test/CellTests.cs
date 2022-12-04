using Flare.Battleship.Domain.Board;

namespace Flare.Battleship.Domain.Test;

public class CellTests
{
    [Test]
    public void Cell_Is_EqualBy_Value()
    {
        var cell = new Cell(BoardColumn.A, BoardRow.Eight);
        Assert.That(cell, Is.Not.Null);
        var sameCell = new Cell(BoardColumn.A, BoardRow.Eight);
        Assert.That(cell, Is.EqualTo(sameCell));
    }
}