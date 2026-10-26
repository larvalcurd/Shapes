namespace Shapes.Core;

public class ListenerList<T>(Action<T> notifyAction) : IDisposable
    where T : class
{
    private readonly List<(T Listener, long Id)> _listeners = [];
    private Action<T>? _notifyAction = notifyAction ?? throw new ArgumentNullException(nameof(notifyAction));
    private long _nextId;
    private bool _disposed;

    public Subscription Subscribe(T listener)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        Add(listener);

        return new Subscription(() => Remove(listener));
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _notifyAction = null;
        _listeners.Clear();
    }

    public void Add(T listener)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        ArgumentNullException.ThrowIfNull(listener, nameof(listener));


        if (_listeners.Any(entry => ReferenceEquals(entry.Listener, listener)))
        {
            return;
        }

        long id = _nextId;
        _nextId++;

        _listeners.Add((listener, id));
    }

    public void Remove(T listener)
    {
        if (_disposed) return;

        int indexToRemove = -1;

        for (int i = 0; i < _listeners.Count; i++)
        {
            if (ReferenceEquals(_listeners[i].Listener, listener))
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove >= 0)
        {
            _listeners.RemoveAt(indexToRemove);
        }
    }

    public void Notify()
    {
        if (_disposed)
            return;

        Action<T>? action = _notifyAction;

        if (action is null)
            return;

        var snapshot = _listeners.ToList();

        foreach ((T Listener, long Id) in snapshot)
        {
            if (_disposed)
                return;

            bool subscriptionExists = _listeners.Any(current => ReferenceEquals(current.Listener, Listener) && current.Id == Id);

            if (!subscriptionExists)
                continue;

            action(Listener);
        }
    }
}
