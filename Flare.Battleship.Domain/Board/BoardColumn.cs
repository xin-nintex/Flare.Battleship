namespace Flare.Battleship.Domain.Board;

public enum BoardColumn
{
    A = 1,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
}

public enum BoardColumnEdge
{
    LeftEdge = BoardColumn.A,
    RightEdge = BoardColumn.J
}