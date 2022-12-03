using Flare.Battleship.Domain.Board;

namespace Flare.Battleship.Application.Query;

public class AttackResultQuery
{
    public Cell AttackPosition { get; set; }
}