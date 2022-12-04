namespace Flare.Battleship.Internals.Contracts;

internal interface IQueryHandler<in TQuery, out TResult>
{
    TResult Handle(TQuery query);
}