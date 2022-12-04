using Flare.Battleship.Domain.Battleships;

namespace Flare.Battleship.Application.Query.Result;

public record GameResult(bool IsLost, Ship[] LiveShips);