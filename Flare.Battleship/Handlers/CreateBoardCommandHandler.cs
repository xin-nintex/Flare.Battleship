using Flare.Battleship.Application.Command;
using Flare.Battleship.Handlers.Contracts;

namespace Flare.Battleship.Handlers;

public class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand>
{
    private readonly GameContext _gameContext;

    public CreateBoardCommandHandler(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public void Handle(CreateBoardCommand item)
    {
        _gameContext.Initialize();
    }
}