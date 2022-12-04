using Flare.Battleship.Application.Command;
using Flare.Battleship.Domain.Gameplay;
using Flare.Battleship.Handlers.Contracts;

namespace Flare.Battleship.Handlers;

public class AddBattleshipCommandHandler : ICommandHandler<AddBattleshipCommand>
{
    private readonly GameStateContext _gameStateContext;

    public AddBattleshipCommandHandler(GameStateContext gameStateContext)
    {
        _gameStateContext = gameStateContext;
    }
    public void Handle(AddBattleshipCommand item)
    {
        var placement = new ShipPlacement(item.Ship, item.StartCell, item.Orientation, item.Swing);
        _gameStateContext.Save(placement);
    }
}