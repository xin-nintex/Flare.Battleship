namespace Flare.Battleship.Domain.Exceptions;

public class AttackAlreadyPlacedException : Exception
{
    public AttackAlreadyPlacedException(string s) : base(s)
    {
    }
}