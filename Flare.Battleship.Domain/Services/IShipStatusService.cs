using Flare.Battleship.Domain.Battleships;

namespace Flare.Battleship.Domain.Services;

public interface IShipStatusService
{
    bool? IsSunk(Ship ship);
    void RecordShipHit(Ship ship);
    void ResetStatuses();
}