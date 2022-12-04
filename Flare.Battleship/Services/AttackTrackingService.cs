using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Services.Internals;

namespace Flare.Battleship.Services;

public sealed class AttackTrackingService : BoardService<AttackPlacement>
{
    public override bool IsPlaced(AttackPlacement item)
    {
        return TrackingStack.Any(x => x.Cell == item.Cell);
    }
}