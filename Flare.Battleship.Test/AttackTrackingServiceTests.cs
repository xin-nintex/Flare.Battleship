using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Services;

namespace Flare.Battleship.Test;

public class AttackTrackingServiceTests
{
    [Test]
    public void AttackTrackingService_ShouldKnow_PlacedAttackPlacement()
    {
        var service = new AttackTrackingService();
        service.Place(new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement));

        var test = new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement);
        
        Assert.That(service.IsPlaced(test), Is.True);
    }

    [Test]
    public void AttackTrackingService_ShouldClear_WhenReset()
    {
        var service = new AttackTrackingService();
        service.Place(new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement));

        var test = new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement);
        
        Assert.That(service.IsPlaced(test), Is.True);
        service.ClearBoard();
        
        Assert.That(service.IsPlaced(test), Is.False);
    }
    
    [Test]
    public void AttackTrackingService_Is_AListOfTrackedObjects()
    {
        var service = new AttackTrackingService();
        service.Place(new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement));

        var test = new AttackPlacement(new Cell(BoardColumn.A, BoardRow.Eight), OccupationType.HitPlacement);
        
        Assert.That(service.IsPlaced(test), Is.True);
        Assert.That(service, Is.EquivalentTo(new [] {test}));
    }
}