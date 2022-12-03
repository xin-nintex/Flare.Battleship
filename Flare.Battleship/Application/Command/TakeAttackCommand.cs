using Flare.Battleship.Domain.Board;

namespace Flare.Battleship.Application.Command;

public class TakeAttackCommand
{
    public Cell AttackPosition { get; set; }
}