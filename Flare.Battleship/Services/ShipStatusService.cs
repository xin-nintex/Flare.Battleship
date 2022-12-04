using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Services;

namespace Flare.Battleship.Services;

internal class ShipStatusService : IShipStatusService
{
    private readonly Dictionary<ShipType, int> _data;

    public ShipStatusService()
    {
        _data = new Dictionary<ShipType, int>();
    }

    public int GetHitCount(Ship ship) => _data.TryGetValue(ship.Type, out var hit) ? hit : 0;

    public bool IsSunk(Ship ship) => _data.TryGetValue(ship.Type, out var hit) && hit == ship.Length;

    public void RecordShipHit(Ship ship)
    {
        if (!_data.TryGetValue(ship.Type, out _))
            _data[ship.Type] = 1;
        else
        {
            _data[ship.Type] += 1;
        }
    }

    public void ResetStatuses()
    {
        _data.Clear();
    }
}
