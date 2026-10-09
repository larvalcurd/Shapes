using Shapes.Commands;
using Shapes.Commands.Handlers;
using Shapes.Gfx;
using Shapes.Shapes;
using Shapes.Shapes.Geometries;

namespace ShapesApp;

public static class Bootstrap
{
    public static (CommandDispatcher Dispatcher, SvgCanvas Canvas) Create()
    {
        var picture = new Picture();

        var canvas = new SvgCanvas(1220, 1200);
        
        var registry = new Registry();
        registry.Register("circle", CircleGeometry.Parse);
        registry.Register("rectangle", RectangleGeometry.Parse);
        registry.Register("triangle", TriangleGeometry.Parse);
        registry.Register("line", LineGeometry.Parse);
        registry.Register("text", TextGeometry.Parse);

        var dispatcher = new CommandDispatcher(Console.Out);
        
        dispatcher.Register("AddShape", new AddShapeCommand(picture, registry));
        dispatcher.Register("MoveShape", new MoveShapeCommand(picture));
        dispatcher.Register("MovePicture", new MovePictureCommand(picture));
        dispatcher.Register("DeleteShape", new DeleteShapeCommand(picture));
        dispatcher.Register("CloneShape", new CloneShapeCommand(picture));
        
        dispatcher.Register("List", new ListCommand(picture, Console.Out));
        
        dispatcher.Register("ChangeColor", new ChangeColorCommand(picture));
        dispatcher.Register("ChangeShape", new ChangeShapeCommand(picture, registry));
        
        dispatcher.Register("DrawShape", new DrawShapeCommand(picture, canvas));
        dispatcher.Register("DrawPicture", new DrawPictureCommand(picture, canvas));
        
        return (dispatcher, canvas);
    }
}