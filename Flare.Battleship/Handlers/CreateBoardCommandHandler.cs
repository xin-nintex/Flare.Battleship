using Flare.Battleship.Application.Command;
using Flare.Battleship.Handlers.Contracts;

namespace Flare.Battleship.Handlers;

public class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand>
{
    private readonly GameStateContext _gameStateContext;

    public CreateBoardCommandHandler(GameStateContext gameStateContext)
    {
        _gameStateContext = gameStateContext;
    }

    public void Handle(CreateBoardCommand item)
    {
        _gameStateContext.Initialize();
    }
}