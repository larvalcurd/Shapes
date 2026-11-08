namespace Shapes.Core.Events;

public record ShapeGeometryChangedArgs(Shape Shape, IShapeGeometry OldGeometry);
