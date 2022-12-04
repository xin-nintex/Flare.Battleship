using Flare.Battleship.Application.Command;
using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Handlers;
using Flare.Battleship.Services;

namespace Flare.Battleship;

public class Game
{
    private readonly GameContext _gameContext;

    public Game()
    {
        _gameContext = new GameContext(
            new ShipTrackingService(),
            new AttackTrackingService(),
            new ShipStatusService()
        );
    }

    public void CreateBoard(CreateBoardCommand command)
    {
        new CreateBoardCommandHandler(_gameContext).Handle(command);
    }

    public AttackResult? TakeAttack(TakeAttackCommand command)
    {
        new TakeAttackCommandHandler(_gameContext).Handle(command);
        return new AttackResultQueryHandler(_gameContext).Handle(new AttackResultQuery()
        {
            AttackPosition = command.AttackPosition
        });
    }

    public void PlaceShip(AddBattleshipCommand command)
    {
        new AddBattleshipCommandHandler(_gameContext).Handle(command);
    }

    public async Task GameLoop(CancellationToken token)
    {
        CreateBoard(new CreateBoardCommand());
        while (!token.IsCancellationRequested)
        {
            PlaceShip(new AddBattleshipCommand()
            {
                Orientation = Enum.Parse<Orientation>(Console.ReadLine() ?? throw new InvalidOperationException())
            });
        }
    }
}
