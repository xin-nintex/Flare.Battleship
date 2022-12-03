namespace Flare.Battleship.Domain.Exceptions;

public class ShipAlreadyPlaceException : Exception
{
    public ShipAlreadyPlaceException(string s) : base(s)
    {
    }
}