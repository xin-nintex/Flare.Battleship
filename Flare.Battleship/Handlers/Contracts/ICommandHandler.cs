namespace Flare.Battleship.Handlers.Contracts;

internal interface ICommandHandler<in T>
{
    void Handle(T item);
}