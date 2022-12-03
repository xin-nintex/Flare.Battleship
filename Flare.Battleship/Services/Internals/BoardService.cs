using System.Collections;
using Flare.Battleship.Domain.Services;

namespace Flare.Battleship.Services.Internals;

internal class BoardService<T> : IBoardService<T> where T : IEquatable<T>
{
    protected readonly Stack<T> TrackingStack;

    protected BoardService()
    {
        TrackingStack = new Stack<T>();
    }

    public virtual bool IsPlaced(T item)
    {
        return TrackingStack.Any(x => x.Equals(item));
    }

    public virtual void Record(T item)
    {
        TrackingStack.Push(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return TrackingStack.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}