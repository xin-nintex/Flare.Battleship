namespace Flare.Battleship.Domain.Services;

public interface IBoardService<T> : IEnumerable<T>
{
    bool IsPlaced(T item);
    void Place(T item);
    void ClearBoard();
}