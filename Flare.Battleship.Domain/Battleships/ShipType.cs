namespace Flare.Battleship.Domain.Battleships;

public enum ShipType
{
    Destroyer = 2,
    Submarine = 3,
    Cruiser = Submarine,
    Battleship = 4,
    Carrier = 5,
}