namespace Flare.Battleship.Handlers.Contracts;

internal interface IQueryHandler<in TQuery, out TResult>
{
    TResult Handle(TQuery query);
}