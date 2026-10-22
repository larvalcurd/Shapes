namespace Shapes.Core;

public class Subscription : IDisposable
{
    private Action? _unsubscribe;

    internal Subscription(Action unsubscribe)
    {
        _unsubscribe = unsubscribe;
    }

    public void Dispose()
    {
        if (_unsubscribe == null) return;
        _unsubscribe();
        _unsubscribe = null;
    }

    public Subscription Take()
    {
        if (_unsubscribe == null)
        {
            throw new InvalidOperationException("Subscription has already been disposed or taken.");
        }
        
        var subscription = new Subscription(_unsubscribe);
        _unsubscribe = null;
        return subscription;
    }
}