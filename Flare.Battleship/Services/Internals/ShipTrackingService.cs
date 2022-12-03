using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Services.Internals;

internal sealed class ShipTrackingService : BoardService<ShipPlacement>
{
    public override bool IsPlaced(ShipPlacement item)
    {
        return TrackingStack.Any(s => s.Ship == item.Ship);
    }
}