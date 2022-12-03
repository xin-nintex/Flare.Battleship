using Flare.Battleship.Application.Contracts;
using Flare.Battleship.Application.Query;
using Flare.Battleship.Application.Query.Result;

namespace Flare.Battleship.Internals;

internal class GameResultQueryHandler : IQueryHandler<GameResultQuery, GameResult>
{
    private readonly GameContext _gameContext;

    public GameResultQueryHandler(GameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public GameResult Handle(GameResultQuery query)
    {
        var isLost = _gameContext.ShipStatus.All(s => s.IsSunk);
        var livShips = _gameContext.ShipStatus.Where(s => !s.IsSunk).Select(s => s.Ship);
        return new GameResult(isLost, livShips);
    }
}
