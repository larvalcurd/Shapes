namespace ShapesApp;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: Shapes <output.svg>");
            return;
        }
        
        var (dispatcher, canvas) = Bootstrap.Create();
        
        while (Console.ReadLine() is { } line)
        {
            dispatcher.Dispatch(line);
        }
        
        canvas.Save(args[0]);
        Console.WriteLine($"Saved to {args[0]}");
    }
}