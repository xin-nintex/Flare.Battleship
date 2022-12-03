using Flare.Battleship.Application.Query;

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
        throw new NotImplementedException();
    }
}

public class GameResult
{
    public bool IsLost { get; }
}

internal interface IQuery<TResult>
{
    
}

internal interface IQueryHandler<in TQuery, out TResult>
{
    TResult Handle(TQuery query);
}