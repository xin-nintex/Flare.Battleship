using Flare.Battleship.Domain.Board;

namespace Flare.Battleship.Domain.Gameplay;

public record AttackPlacement(Cell Cell, OccupationType Type);