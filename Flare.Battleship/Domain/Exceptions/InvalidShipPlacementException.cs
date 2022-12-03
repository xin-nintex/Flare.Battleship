namespace Flare.Battleship.Domain.Exceptions;

internal class InvalidShipPlacementException : Exception
{
    public InvalidShipPlacementException(string s) : base(s)
    {
    }
}