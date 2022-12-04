using System.Diagnostics.CodeAnalysis;

namespace Flare.Battleship.Domain.Exceptions;
[ExcludeFromCodeCoverage]
public class InvalidShipPlacementException : Exception
{
    public InvalidShipPlacementException(string s) : base(s)
    {
    }
}