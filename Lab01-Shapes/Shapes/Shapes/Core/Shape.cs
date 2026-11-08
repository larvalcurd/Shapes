using Shapes.Core.Events;
using Shapes.Core.Listeners;
using Shapes.Gfx;

namespace Shapes.Core;

public class Shape : IDisposable
{
    private readonly Signal<ShapeMovedArgs> _moved = new();
    private readonly Signal<ShapeColorChangedArgs> _colorChanged = new();
    private readonly Signal<ShapeGeometryChangedArgs> _geometryChanged = new();

    private Color _color;

    // TODO: провести аналогию с ptr в C++, разобраться, какой тип владения геометрией
    // В C# это управляемая ссылка (GC). Shape не управляет детерминированным
    // освобождением геометрии: он не вызывает у нее Dispose и не знает, есть ли
    // у нее другие владельцы. Но, удерживая ссылку в поле _geometry, Shape продлевает
    // жизнь объекта: пока жив Shape, геометрия не будет собрана GC.
    // Ближайший аналог в C++ по смыслу это std::shared_ptr<IShapeGeometry>
    // (разделяемое удержание: объект живет, пока на него есть ссылки), но с двумя
    // отличиями: нет счетчика ссылок и нет детерминированного разрушения —
    // освобождение делает GC, и циклы он тоже умеет собирать.
    private IShapeGeometry _geometry;

    public Shape(string id, Color color, IShapeGeometry geometry)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        _color = color;
        _geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
    }

    public string Id { get; }

    public Color Color
    {
        get => _color;
        set
        {
            Color oldColor = _color;
            _color = value;
            _colorChanged.Notify(new ShapeColorChangedArgs(this, oldColor));
        }
    }

    public void Move(double dx, double dy)
    {
        _geometry.Move(dx, dy);
        _moved.Notify(new ShapeMovedArgs(this, dx, dy));
    }

    public void ReplaceGeometry(IShapeGeometry newGeometry)
    {
        ArgumentNullException.ThrowIfNull(newGeometry);

        IShapeGeometry oldGeometry =_geometry;
        _geometry = newGeometry;
        _geometryChanged.Notify(new ShapeGeometryChangedArgs(this, oldGeometry));
    }

    public void Draw(ICanvas canvas)
    {
        canvas.SetColor(Color);
        _geometry.Draw(canvas);
    }

    public string GetTypeName() => _geometry.TypeName;
    public string GetParamsString() => _geometry.GetParamsString();

    // TODO: паттерн "прототип"
    // Используется для создания копии объекта на основе уже существующего экземпляра.
    // В данном случае реализуется глубокое копирование, так как сложный внутренний
    // объект _geometry тоже клонируется, а не просто копируется по ссылке. Это предотвращает
    // ситуацию, когда изменение геометрии у клона меняет геометрию у оригинала.
    public Shape Clone(string newId) => new(newId, Color, _geometry.Clone());

    internal Subscription OnMoved(Action<ShapeMovedArgs> handler) => _moved.Subscribe(handler);
    internal Subscription OnColorChanged(Action<ShapeColorChangedArgs> handler) => _colorChanged.Subscribe(handler);
    internal Subscription OnGeometryChanged(Action<ShapeGeometryChangedArgs> handler) => _geometryChanged.Subscribe(handler);

    public void Dispose()
    {
        _moved.Dispose();
        _colorChanged.Dispose();
        _geometryChanged.Dispose();
    }
}
