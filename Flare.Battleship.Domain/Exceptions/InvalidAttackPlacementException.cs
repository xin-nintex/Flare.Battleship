using System.Diagnostics.CodeAnalysis;

namespace Flare.Battleship.Domain.Exceptions;
[ExcludeFromCodeCoverage]
public class InvalidAttackPlacementException : Exception
{
    public InvalidAttackPlacementException(string s) : base(s)
    {
    }
}