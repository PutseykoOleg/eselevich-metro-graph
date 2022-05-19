namespace Metro.Graph;

// Класс ребра взвешенного неориентированного графа (T - тип значения вершин)
public struct Edge<T> where T : notnull
{
    // Пара вершин
    public List<Vertex<T>> Vertices { get; private set; }

    // Вес ребра
    public int Weight { get; private set; }

    // Конструктор
    public Edge(Vertex<T> firstVertex, Vertex<T> secondVertex, int weight)
    {
        this.Vertices = new() { firstVertex, secondVertex };
        this.Weight = weight;
    }

    // Получение обратного ребра
    public Edge<T> GetReverseEdge()
    {
        return new(this.Vertices[1], this.Vertices[0], this.Weight);
    }

    // Проверка двух ребер на неравенство
    public static bool operator !=(Edge<T> firstEdge, Edge<T> secondEdge) =>
        !(firstEdge == secondEdge);

    // Проверка двух ребер на равенство
    public static bool operator ==(Edge<T> firstEdge, Edge<T> secondEdge)
    {
        // Значения обоих ребер в удобном виде
        List<List<T>> values = new()
        {
            new()
            {
                firstEdge.Vertices[0].Value,
                firstEdge.Vertices[1].Value
            },
            new()
            {
                secondEdge.Vertices[0].Value,
                secondEdge.Vertices[1].Value
            }
        };

        // Т.к. рассматривается неориентированный граф, то ребра вида [a, b] и [a, b] будут равны
        bool areValuesEqual = values[0][0].Equals(values[1][0]) && values[0][1].Equals(values[1][1]);
        // и ребра вида [a, b] и [b, a] тоже будут равны
        bool areValuesReverseEqual = values[0][0].Equals(values[1][1]) && values[0][1].Equals(values[1][0]);

        // Если оба равенства справедливы, и вес ребер совпадает, то они равны
        return (areValuesEqual || areValuesReverseEqual) && firstEdge.Weight == secondEdge.Weight;
    }


}
