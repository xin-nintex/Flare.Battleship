namespace Flare.Battleship.Application.Query.Result;

public class AttackResult
{
    public bool IsHit { get; }

    public AttackResult(bool isHit)
    {
        IsHit = isHit;
    }
}