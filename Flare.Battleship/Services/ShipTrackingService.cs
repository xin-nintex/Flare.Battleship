using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Services.Internals;

namespace Flare.Battleship.Services;

internal sealed class ShipTrackingService : BoardService<ShipPlacement>
{
    public override bool IsPlaced(ShipPlacement item)
    {
        return TrackingStack.Any(s => s.Ship == item.Ship);
    }
}