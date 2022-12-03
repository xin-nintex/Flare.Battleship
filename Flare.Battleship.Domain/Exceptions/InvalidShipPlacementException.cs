namespace Flare.Battleship.Domain.Exceptions;

public class InvalidShipPlacementException : Exception
{
    public InvalidShipPlacementException(string s) : base(s)
    {
    }
}