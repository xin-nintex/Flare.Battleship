using Flare.Battleship.Application.Command;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Handlers.Contracts;

namespace Flare.Battleship.Handlers;

public class TakeAttackCommandHandler : ICommandHandler<TakeAttackCommand>
{
    private readonly GameStateContext _gameStateContext;

    public TakeAttackCommandHandler(GameStateContext gameStateContext)
    {
        _gameStateContext = gameStateContext;
    }

    public void Handle(TakeAttackCommand item)
    {
        var attackPlacement = _gameStateContext.ShipPlacements.Any(
            s => s.IsAtCell(item.AttackPosition)
        ) switch
        {
            true => new AttackPlacement(item.AttackPosition, OccupationType.HitPlacement),
            false => new AttackPlacement(item.AttackPosition, OccupationType.MissPlacement)
        };
        if (attackPlacement.Type == OccupationType.HitPlacement)
        {
            var ship = _gameStateContext.ShipPlacements.First(s => s.IsAtCell(item.AttackPosition)).Ship;
            _gameStateContext.Save(attackPlacement, ship);
            return;
        }
        _gameStateContext.Save(attackPlacement, null);
    }
}
