using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Internals.Contracts;

namespace Flare.Battleship.Handlers;

public class AttackResultQueryHandler : IQueryHandler<AttackResultQuery, AttackResult?>
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
