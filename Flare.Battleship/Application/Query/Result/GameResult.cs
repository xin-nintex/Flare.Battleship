using Flare.Battleship.Domain.Battleships;

namespace Flare.Battleship.Application.Query.Result;

public class GameResult
{
    public bool IsLost { get; }
    public IEnumerable<Ship> LiveShips { get; }

    public GameResult(bool isLost, IEnumerable<Ship> liveShips)
    {
        IsLost = isLost;
        LiveShips = liveShips;
    }
}