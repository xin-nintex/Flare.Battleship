namespace Flare.Battleship.Application.Contracts;

public interface ICommandHandler<in T>
{
    void Handle(T item);
}