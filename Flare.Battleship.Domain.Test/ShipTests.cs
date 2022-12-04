using Flare.Battleship.Domain.Battleships;

namespace Flare.Battleship.Domain.Test;

public class ShipTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Ship_Is_EqualBy_Value()
    {
        var id = Guid.NewGuid();
        var ship = new Ship(id, 5, "Test");
        Assert.That(ship, Is.Not.Null);
        var sameShip = new Ship(id, 5, "Test");
        Assert.That(ship, Is.EqualTo(sameShip));
    }
}