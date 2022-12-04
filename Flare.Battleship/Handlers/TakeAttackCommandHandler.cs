using Flare.Battleship.Application.Command;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Handlers.Contracts;

namespace Flare.Battleship.Handlers;

internal class TakeAttackCommandHandler : ICommandHandler<TakeAttackCommand>
{
    private readonly GameContext _gameContext;

    public TakeAttackCommandHandler(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public void Handle(TakeAttackCommand item)
    {
        var attackPlacement = _gameContext.ShipPlacements.Any(
            s => s.IsAtCell(item.AttackPosition)
        ) switch
        {
            true => new AttackPlacement(item.AttackPosition, OccupationType.HitPlacement),
            false => new AttackPlacement(item.AttackPosition, OccupationType.MissPlacement)
        };
        if (attackPlacement.Type == OccupationType.HitPlacement)
        {
            var ship = _gameContext.ShipPlacements.First(s => s.IsAtCell(item.AttackPosition)).Ship;
            _gameContext.Save(attackPlacement, ship);
            return;
        }
        _gameContext.Save(attackPlacement, null);
    }
}
