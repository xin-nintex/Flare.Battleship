using Flare.Battleship.Domain.Battleships;
using Flare.Battleship.Domain.Board;

namespace Flare.Battleship.Application.Command;

public class AddBattleshipCommand
{
    public Ship Ship { get; set; }
    public Cell StartCell { get; set; }
    public Orientation Orientation { get; set; }
    public Swing Swing { get; set; }
}