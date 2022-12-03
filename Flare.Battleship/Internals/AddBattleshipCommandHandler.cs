using Flare.Battleship.Application.Command;
using Flare.Battleship.Application.Contracts;
using Flare.Battleship.Domain.Exceptions;
using Flare.Battleship.Domain.Gameplay;

namespace Flare.Battleship.Internals;

internal class AddBattleshipCommandHandler : ICommandHandler<AddBattleshipCommand>
{
    private readonly GameContext _gameContext;

    public AddBattleshipCommandHandler(GameContext gameContext)
    {
        _gameContext = gameContext;
    }
    public void Handle(AddBattleshipCommand item)
    {
        var placement = new ShipPlacement(item.Ship, item.StartCell, item.Orientation, item.Swing);
        var isIntersecting = _gameContext.ShipPlacements.Any(s => s.IsIntersecting(placement));
        if (isIntersecting)
            throw new InvalidShipPlacementException("There is an existing ship at the placement");
        _gameContext.Save(placement);
    }
}