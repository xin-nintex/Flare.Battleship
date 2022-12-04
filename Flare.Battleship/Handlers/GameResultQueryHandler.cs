using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;
using Flare.Battleship.Handlers.Contracts;

namespace Flare.Battleship.Handlers;

public class GameResultQueryHandler : IQueryHandler<GameResultQuery, GameResult>
{
    private readonly GameStateContext _gameStateContext;

    public GameResultQueryHandler(GameStateContext gameStateContext)
    {
        _gameStateContext = gameStateContext;
    }

    public GameResult Handle(GameResultQuery query)
    {
        var isLost = _gameStateContext.ShipStatus.All(s => s.IsSunk.GetValueOrDefault());
        var livShips = _gameStateContext.ShipStatus.Where(s => !s.IsSunk.GetValueOrDefault()).Select(s => s.Ship);
        return new GameResult(isLost, livShips.ToArray());
    }
}
