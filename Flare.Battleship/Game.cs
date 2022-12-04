using Flare.Battleship.Application.Command;
using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;
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
        return new AttackResultQueryHandler(_gameContext).Handle(
            new AttackResultQuery() { AttackPosition = command.AttackPosition }
        );
    }

    public GameResult QueryGameResult(GameResultQuery query)
    {
        return new GameResultQueryHandler(_gameContext).Handle(query);
    }

    public void PlaceShip(AddBattleshipCommand command)
    {
        new AddBattleshipCommandHandler(_gameContext).Handle(command);
    }

    public async Task GameLoop(CancellationToken token)
    {
        CreateBoard(new CreateBoardCommand());
        PlaceShip(
            new AddBattleshipCommand()
            {
                Orientation = Orientation.Horizontal,
                Ship = Ship.Battleship,
                Swing = Swing.Right,
                StartCell = new Cell(BoardColumn.A, BoardRow.Four)
            }
        );
        PlaceShip(
            new AddBattleshipCommand()
            {
                Orientation = Orientation.Horizontal,
                Ship = Ship.Carrier,
                Swing = Swing.Right,
                StartCell = new Cell(BoardColumn.A, BoardRow.Five)
            }
        );
        var rand = new Random();
        while (!token.IsCancellationRequested)
        {
            try
            {
                var randCol = rand.Next((int)BoardColumnEdge.RightEdge);
                var randRow = rand.Next((int)BoardRowEdge.BottomEdge);
                if (randCol == 0 || randRow == 0)
                    continue;
                var attackPosition = new Cell((BoardColumn)randCol, (BoardRow)randRow);
                Console.WriteLine($"Taking attack at {attackPosition}");
                var result = TakeAttack(new TakeAttackCommand() { AttackPosition = attackPosition });
                if (result?.IsHit ?? false)
                {
                    Console.WriteLine("It's Hit");
                }
                else
                {
                    Console.WriteLine("It' miss");
                }

                var game = QueryGameResult(new GameResultQuery());
                Console.WriteLine(
                    game.IsLost ? "you have lost" : $"you have {game.LiveShips.Count()} ships left"
                );
                if (game.IsLost)
                    break;
            }
            catch (InvalidAttackPlacementException)
            {
                Console.WriteLine("random attack is placed before, continuing...");
            }

            await Task.Delay(1000);
        }
    }
}
