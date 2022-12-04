namespace Flare.Battleship.Domain.Battleships;

public abstract record Ship(ShipType Type, int Length)
{
    private record Destroyer() : Ship(ShipType.Destroyer, 2);

    private record Submarine() : Ship(ShipType.Submarine, 3);

    private record Cruiser() : Ship(ShipType.Cruiser, 3);

    private record Battleship() : Ship(ShipType.Battleship, 4);

    private record Carrier() : Ship(ShipType.Carrier, 5);

    public static IEnumerable<Ship> AllAvailableShips
    {
        get
        {
            yield return new Destroyer();
            yield return new Submarine();
            yield return new Carrier();
            yield return new Battleship();
            yield return new Cruiser();
        }
    }
}
