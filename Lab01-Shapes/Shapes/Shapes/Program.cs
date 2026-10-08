using Shapes.Gfx;
using Shapes.Commands;
using Shapes.Commands.Handlers;
using Shapes.Shapes;

if (args.Length < 1)
{
    Console.WriteLine("Usage: Shapes <output.svg>");
    return;
}

var outputPath = args[0];

var picture = new Picture();
var canvas = new SvgCanvas(1200, 1200);

var dispatcher = new CommandDispatcher();
dispatcher.Register("AddShape", new AddShapeCommand());
dispatcher.Register("MoveShape", new MoveShapeCommand());
dispatcher.Register("MovePicture", new MovePictureCommand());
dispatcher.Register("DeleteShape", new DeleteShapeCommand());
dispatcher.Register("CloneShape", new CloneShapeCommand());
dispatcher.Register("List", new ListCommand());
dispatcher.Register("ChangeColor", new ChangeColorCommand());
dispatcher.Register("ChangeShape", new ChangeShapeCommand());
dispatcher.Register("DrawShape", new DrawShapeCommand());
dispatcher.Register("DrawPicture", new DrawPictureCommand());

var dependencies = new CommandDependencies(picture, canvas, factory);

while (Console.ReadLine() is { } line)
{
    dispatcher.Dispatch(line, dependencies);
}

canvas.Save(outputPath);
Console.WriteLine($"Saved to {outputPath}");
