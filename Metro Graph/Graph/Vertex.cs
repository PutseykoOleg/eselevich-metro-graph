namespace Metro.Graph;

// Интерфейс вершины (T - тип значения вершины)
public interface IVertex<T> where T : notnull
{
    // Значение
    public T Value { get; set; }
}

// Класс вершины (T - тип значения вершины)
public class Vertex<T> : IVertex<T> where T : notnull
{
    // Значение
    public T Value { get; set; } = default;

    // Классы
    public Vertex() { }

    public Vertex(T value)
    {
        this.Value = value;
    }

    // Сравнение на неравенство
    public static bool operator !=(Vertex<T> firstVertex, Vertex<T> secondVertex)
    {
        return !(firstVertex == secondVertex);
    }

    // Сравнение на равенство
    public static bool operator ==(Vertex<T> firstVertex, Vertex<T> secondVertex)
    {
        return firstVertex.Value.Equals(secondVertex.Value);
    }
}
