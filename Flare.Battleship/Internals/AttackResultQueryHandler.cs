using Flare.Battleship.Application.Contracts;
using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Internals;

internal class AttackResultQueryHandler : IQueryHandler<AttackResultQuery, AttackResult?>
{
    private readonly GameContext _gameContext;

    public AttackResultQueryHandler(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public AttackResult? Handle(AttackResultQuery query)
    {
        var hasBeenAttacked = _gameContext.AttackPlacements.Any(
            a => a.Cell == query.AttackPosition
        );
        if (!hasBeenAttacked)
            return null;
        return _gameContext.AttackPlacements.First(a => a.Cell == query.AttackPosition).Type switch
        {
            OccupationType.HitPlacement => new AttackResult(true),
            OccupationType.MissPlacement => new AttackResult(false),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
