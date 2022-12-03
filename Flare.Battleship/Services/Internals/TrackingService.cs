namespace Flare.Battleship.Services.Internals;

internal class TrackingService<T> : ITrackingService<T> where T : IEquatable<T>
{
    protected readonly Stack<T> TrackingStack;

    public TrackingService()
    {
        TrackingStack = new Stack<T>();
    }

    public virtual bool IsPlaced(T item)
    {
        return TrackingStack.Any(x => x.Equals(item));
    }

    public virtual void Track(T item)
    {
        TrackingStack.Push(item);
    }
}