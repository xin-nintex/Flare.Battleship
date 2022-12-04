using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Domain.Test;

public class AttackPlacementTests
{
    [Test]
    public void AttackPlacement_Is_EqualBy_Value()
    {
        var placement = new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement);
        Assert.That(placement, Is.Not.Null);
        var otherPlacement = new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement);
        Assert.That(placement, Is.EqualTo(otherPlacement));
    }
}