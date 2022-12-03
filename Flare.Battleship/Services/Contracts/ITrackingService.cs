namespace Flare.Battleship.Services.Internals;

public interface ITrackingService<in T>
{
    bool IsPlaced(T item);
    void Track(T item);
}