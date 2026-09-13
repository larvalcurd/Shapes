namespace Shapes.Commands.Handlers;

public class ListCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var shapes = ctx.Picture.GetShapesInOrder();
        for (var i = 0; i < shapes.Count; i++)
        {
            var s = shapes[i];
            Console.WriteLine($"{i+1} {s.Geometry.TypeName} {s.Id} {s.Color} {s.Geometry.GetParamsString()}");
        }
    }
}