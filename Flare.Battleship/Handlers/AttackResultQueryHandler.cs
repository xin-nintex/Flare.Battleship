using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Handlers.Contracts;

namespace Flare.Battleship.Handlers;

public class AttackResultQueryHandler : IQueryHandler<AttackResultQuery, AttackResult?>
{
    private readonly GameStateContext _gameStateContext;

    public AttackResultQueryHandler(GameStateContext gameStateContext)
    {
        _gameStateContext = gameStateContext;
    }

    public AttackResult? Handle(AttackResultQuery query)
    {
        var hasBeenAttacked = _gameStateContext.AttackPlacements.Any(
            a => a.Cell == query.AttackPosition
        );
        if (!hasBeenAttacked)
            return null;
        return _gameStateContext.AttackPlacements.First(a => a.Cell == query.AttackPosition).Type switch
        {
            OccupationType.HitPlacement => new AttackResult(true),
            OccupationType.MissPlacement => new AttackResult(false),
        };
    }
}
