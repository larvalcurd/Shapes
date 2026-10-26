using Shapes.Gfx;

namespace Shapes.Core;

public class Shape : IDisposable
{
    private readonly ListenerList<IShapeListener> _listeners;

    private Color _color;

    // todo: провести аналогию с ptr в c++, разобраться, какой тип владения геометрией
    // это управляемая ссылка, этим занимается GC, аналог в с++ - это unique_ptr, который владеет объектом и уничтожает его при удалении
    private IShapeGeometry _geometry;

    public Shape(string id, Color color, IShapeGeometry geometry)
    {
        Id = id;
        _color = color;
        _geometry = geometry;
        _listeners = new ListenerList<IShapeListener>(listener => listener.OnShapeChanged(this)
        );
    }

    public string Id { get; }

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            _listeners.Notify();
        }
    }

    public void Dispose() => _listeners.Dispose();

    public void Move(double dx, double dy)
    {
        _geometry.Move(dx, dy);
        _listeners.Notify();
    }

    public void Draw(ICanvas canvas)
    {
        canvas.SetColor(Color); 
        _geometry.Draw(canvas);
    }

    public void ReplaceGeometry(IShapeGeometry newGeometry)
    {
        _geometry = newGeometry;
        _listeners.Notify();
    }

    public string GetTypeName() => _geometry.TypeName;

    public string GetParamsString() => _geometry.GetParamsString();

    // TODO: паттерн "прототип"
    // Используется для создания копии объекта (клонирования) на основе уже существующего экземпляра.
    // В данном случае реализуется "глубокое копирование" (Deep Copy), так как сложный внутренний 
    // объект '_geometry' тоже клонируется, а не просто копируется по ссылке. Это предотвращает 
    // ситуацию, когда изменение геометрии у клона меняет геометрию у оригинала.
    public Shape Clone(string newId) => new(newId, Color, _geometry.Clone());

    public void AddListener(IShapeListener listener) => _listeners.Add(listener);

    public void RemoveListener(IShapeListener listener) => _listeners.Remove(listener);

    public Subscription Subscribe(IShapeListener listener) => _listeners.Subscribe(listener);
}
