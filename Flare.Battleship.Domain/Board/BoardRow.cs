namespace Flare.Battleship.Domain.Board;

public enum BoardRow
{
    One = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
}

public enum BoardRowEdge
{
    TopEdge = BoardRow.One,
    BottomEdge = BoardRow.Ten
}