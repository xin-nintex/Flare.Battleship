using Flare.Battleship.Domain.Battleships;

namespace Flare.Battleship.Domain.Gameplay;

public class ShipStatus
{
    public Ship Ship { get; }
    public bool IsSunk { get; }

    public ShipStatus(Ship ship, bool isSunk)
    {
        Ship = ship;
        IsSunk = isSunk;
    }
}