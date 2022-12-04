namespace Flare.Battleship.Domain.Exceptions;

public class InvalidAttackPlacementException : Exception
{
    public InvalidAttackPlacementException(string s) : base(s)
    {
    }
}