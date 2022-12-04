using Flare.Battleship.Application.Command;
using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;
using Flare.Battleship.Domain.Exceptions;
using Flare.Battleship.Handlers;
using Flare.Battleship.Services;

namespace Flare.Battleship.UI.Console;

/// <summary>
///  Simulates a Dependency Injection Container
/// </summary>
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

    private void CreateBoard(CreateBoardCommand command)
    {
        new CreateBoardCommandHandler(_gameContext).Handle(command);
    }

    private AttackResult? TakeAttack(TakeAttackCommand command)
    {
        new TakeAttackCommandHandler(_gameContext).Handle(command);
        return new AttackResultQueryHandler(_gameContext).Handle(
            new AttackResultQuery() { AttackPosition = command.AttackPosition }
        );
    }

    private GameResult QueryGameResult(GameResultQuery query)
    {
        return new GameResultQueryHandler(_gameContext).Handle(query);
    }

    private void PlaceShip(AddBattleshipCommand command)
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
        //TODO: place more ships if like
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
                System.Console.WriteLine($"Taking attack at {attackPosition}");
                var result = TakeAttack(new TakeAttackCommand() { AttackPosition = attackPosition });
                if (result?.IsHit ?? false)
                {
                    System.Console.WriteLine("It's Hit");
                }
                else
                {
                    System.Console.WriteLine("It' miss");
                }

                var game = QueryGameResult(new GameResultQuery());
                System.Console.WriteLine(
                    game.IsLost ? "you have lost" : $"you have {game.LiveShips.Count()} ships left"
                );
                if (game.IsLost)
                    break;
            }
            catch (InvalidAttackPlacementException)
            {
                System.Console.WriteLine("random attack is placed before, continuing...");
            }

            await Task.Delay(1000, token);
        }
    }
}
