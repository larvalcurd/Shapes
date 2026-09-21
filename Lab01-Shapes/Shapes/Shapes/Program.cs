using Shapes.Gfx;
using Shapes.Commands;
using Shapes.Commands.Handlers;
using Shapes.Formatting;
using Shapes.Shapes;
using Shapes.Shapes.ShapeFactory;

if (args.Length < 1)
{
    Console.WriteLine("Usage: Shapes <output.svg>");
    return;
}

var outputPath = args[0];

var factory = DefaultFactory.CreateDefault();
var picture = new Picture();
var canvas = new SvgCanvas();

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

var ctx = new CommandContext(picture, canvas, factory);

while (Console.ReadLine() is { } line)
{
    dispatcher.Dispatch(line, ctx);
}

canvas.Save(outputPath);
Console.WriteLine($"Saved to {outputPath}");