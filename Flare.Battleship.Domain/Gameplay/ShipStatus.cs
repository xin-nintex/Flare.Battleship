using Flare.Battleship.Domain.Battleships;

namespace Flare.Battleship.Domain.Gameplay;

public record ShipStatus(Ship Ship, bool IsSunk);