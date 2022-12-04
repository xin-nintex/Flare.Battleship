using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Services;

namespace Flare.Battleship.Services;

public class ShipStatusService : IShipStatusService
{
    private readonly Dictionary<Ship, int> _data;

    public ShipStatusService()
    {
        _data = new Dictionary<Ship, int>();
    }

    public int GetHitCount(Ship ship) => _data.TryGetValue(ship, out var hit) ? hit : 0;

    public bool IsSunk(Ship ship) => _data.TryGetValue(ship, out var hit) && hit == ship.Length;

    public void RecordShipHit(Ship ship)
    {
        if (!_data.TryGetValue(ship, out _))
            _data[ship] = 1;
        else
        {
            _data[ship] += 1;
        }
    }

    public void ResetStatuses()
    {
        _data.Clear();
    }
}
