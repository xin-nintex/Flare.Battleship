using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Services.Internals;

internal sealed class AttackTrackingService : TrackingService<AttackPlacement>
{
    public override bool IsPlaced(AttackPlacement item)
    {
        return TrackingStack.Any(x => x.Cell == item.Cell);
    }
}