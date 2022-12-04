using Flare.Battleship.Domain.Battleships;

namespace Flare.Battleship.Domain.Services;

public interface IShipStatusService
{
    int GetHitCount(Ship ship);
    bool IsSunk(Ship ship);
    void RecordShipHit(Ship ship);
    void ResetStatuses();
}