namespace Shapes.Tests.Mocks;

public class OrderCounter
{
    private int _next;
    public int Next() => _next++;
}