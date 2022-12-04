using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Domain.Test;

public class ShipStatusTests
{
    
    [Test]
    public void ShipStatus_Is_EqualBy_Value()
    {
        var shipStatus = new ShipStatus(Ship.Battleship, true);
        Assert.That(shipStatus, Is.Not.Null);
        var expected = new ShipStatus(Ship.Battleship, true);
        Assert.That(shipStatus, Is.EqualTo(expected));
    }
}