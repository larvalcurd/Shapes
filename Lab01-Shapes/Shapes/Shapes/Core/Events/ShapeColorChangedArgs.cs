using Shapes.Gfx;

namespace Shapes.Core.Events;

// новое значение обработчик всегда может прочитать из args.Shape.Color
public record ShapeColorChangedArgs(Shape Shape, Color OldColor);
