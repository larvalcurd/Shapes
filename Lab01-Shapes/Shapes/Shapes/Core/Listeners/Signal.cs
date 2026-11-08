using System.Runtime.InteropServices;

namespace Shapes.Core.Listeners;

public class Signal<TArgs> : IDisposable
{
    private readonly List<(Action<TArgs> Handler, long Id)> _handlers = [];
    private long _nextId;
    private bool _disposed;

    public Subscription Subscribe(Action<TArgs> handler)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        Add(handler);

        return new Subscription(() => Remove(handler));
    }

    public void Add(Action<TArgs> handler)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        ArgumentNullException.ThrowIfNull(handler);

        if (_handlers.Any(entry => ReferenceEquals(entry.Handler, handler)))
        {
            return;
        }

        long id =_nextId;
        _nextId++;

        _handlers.Add((handler, id));
    }

    public void Remove(Action<TArgs> handler)
    {
        if (_disposed) return;

        if (handler is null) return;

        for (int i = 0; i < _handlers.Count; i++)
        {
            if (ReferenceEquals(_handlers[i].Handler, handler))
            {
                _handlers.RemoveAt(i);
                return;
            }
        }
    }

    public void Notify(TArgs args)
    {
        if (_disposed) return;

        var snapshot =_handlers.ToList();

        foreach ((Action<TArgs> Handler, long Id) in snapshot)
        {
            if (_disposed) return;

            bool subscriptionExists = _handlers.Any(current =>
                ReferenceEquals(current.Handler, Handler) && current.Id == Id);

            if (!subscriptionExists) continue;

            Handler(args);
        }
    }
    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
        _handlers.Clear();
    }
}
