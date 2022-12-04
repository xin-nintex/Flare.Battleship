using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Services;

namespace Flare.Battleship.Test;

public class ShipStatusServiceTests
{
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void ShipStatusService_ShouldRecord_HitCount(int hit)
    {
        var service = new ShipStatusService();
        var expect = hit;
        while (hit > 0)
        {
            service.RecordShipHit(Ship.Battleship);
            hit--;
        }
        Assert.That(service.GetHitCount(Ship.Battleship), Is.EqualTo(expect));
    }

    [Test]
    public void ShipStatusService_ShouldReturn_NegativeOne_WhenShipIsUnknown()
    {
        var service = new ShipStatusService();
        var hitCount = service.GetHitCount(Ship.Battleship);
        Assert.That(hitCount, Is.EqualTo(-1));
    }

    [Test]
    public void ShipStatusService_ShouldKnow_IfShipIsSunk()
    {
        var service = new ShipStatusService();
        service.RecordShipHit(Ship.Destroyer);
        service.RecordShipHit(Ship.Destroyer);
        Assert.That(service.IsSunk(Ship.Destroyer), Is.True);
        
        service.RecordShipHit(Ship.Carrier);
        Assert.That(service.IsSunk(Ship.Carrier), Is.False);
        
        //Unknown ship is null
        Assert.That(service.IsSunk(Ship.Submarine), Is.Null);
    }

    [Test]
    public void ShipStatusService_ShouldClear_WhenReset()
    {
        var service = new ShipStatusService();
        service.RecordShipHit(Ship.Destroyer);
        Assert.That(service.GetHitCount(Ship.Destroyer), Is.EqualTo(1));
        
        service.ResetStatuses();
        
        Assert.That(service.GetHitCount(Ship.Destroyer), Is.EqualTo(-1));
    }
}
