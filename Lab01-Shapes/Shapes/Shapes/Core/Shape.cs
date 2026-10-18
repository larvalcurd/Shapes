using Shapes.Gfx;

namespace Shapes.Core;

public class Shape(string id, Color color, IShapeGeometry geometry)
{
    public string Id { get; } = id;

    private Color _color = color;

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            NotifyListeners();
        }
    }
    
    private readonly List<IShapeListener> _listeners = [];
    
    // todo: провести аналогию с ptr в c++, разобраться, какой тип владения геометрией
    // это управляемая ссылка, этим занимается GC, аналог в с++ - это unique_ptr, который владеет объектом и уничтожает его при удалении
    private IShapeGeometry _geometry = geometry;

    public void Move(double dx, double dy)
    {
        _geometry.Move(dx, dy);
        NotifyListeners();
    }

    public void Draw(ICanvas canvas)
    {
        canvas.SetColor(Color);
        _geometry.Draw(canvas);
    }

    public void ReplaceGeometry(IShapeGeometry newGeometry)
    {
        _geometry = newGeometry;
        NotifyListeners();
    }

    public string GetTypeName() => _geometry.TypeName;

    public string GetParamsString() => _geometry.GetParamsString();
    // TODO: паттерн "прототип"
    public Shape Clone(string newId) => new Shape(newId, Color, _geometry.Clone());

    public void AddListener(IShapeListener listener)
    {
        if (!_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void RemoveListener(IShapeListener listener)
    {
        _listeners.Remove(listener);
    }

    private void NotifyListeners()
    {
        foreach (var listener in _listeners)
        {
            listener.OnShapeChanged(this);
        }
    }
}