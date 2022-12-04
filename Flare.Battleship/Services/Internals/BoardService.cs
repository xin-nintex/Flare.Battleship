using System.Collections;
using Flare.Battleship.Domain.Services;

namespace Flare.Battleship.Services.Internals;

public abstract class BoardService<T> : IBoardService<T> where T : IEquatable<T>
{
    protected readonly Stack<T> TrackingStack;

    protected BoardService()
    {
        TrackingStack = new Stack<T>();
    }

    public abstract bool IsPlaced(T item);

    public virtual void Place(T item)
    {
        TrackingStack.Push(item);
    }

    public void ClearBoard()
    {
        TrackingStack.Clear();
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