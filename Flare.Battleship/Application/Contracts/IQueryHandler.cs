namespace Flare.Battleship.Application.Contracts;

public interface IQueryHandler<in TQuery, out TResult>
{
    TResult Handle(TQuery query);
}