namespace Metro.Graph;

// Алиас матрицы смежности
using Matrix = List<List<int>>;

// Класс взвешенного неориентированного графа (T - тип значения вершины)
public class Graph<T> where T : notnull
{
    // Список вершин
    public List<Vertex<T>> Vertices { get; private set; } = new();

    // Пары [<вершина> - <прилежащие к ней ребра>]
    public Dictionary<Vertex<T>, List<Edge<T>>> LinkedEdges { get; private set; } = new();

    public Graph() { }

    // Добавить новое ребро в граф
    public void AddEdge(T firstVertexValue, T secondVertexValue, int weight)
    {
        // Новое ребро
        Edge<T> edge =
            new(
                this.GetVertex(firstVertexValue, true),
                this.GetVertex(secondVertexValue, true),
                weight
            );

        // Пройтись по вершинам ребра
        for (int i = 0; i < 2; i++)
        {
            // Взять текущую
            Vertex<T> vertex = edge.Vertices[i];

            // Если текущей вершины нет в списке пар [<вершина> - <прилежащие к ней ребра>], то добавить
            if (!this.LinkedEdges.Keys.Contains(vertex))
            {
                this.LinkedEdges.Add(vertex, new());
            }

            // Если нового ребра еще нет в списке прилежащих к текущей вершине, то добавить
            if (
                !this._IsEdgeContained(vertex, edge)
                && !this._IsEdgeContained(vertex, edge.GetReverseEdge())
            )
            {
                this.LinkedEdges[vertex].Add(edge);
            }
        }
    }

    // Получение вершины, лежащей в списке вершин
    public Vertex<T> GetVertex(T value, bool addIfNotExist = false)
    {
        try
        {
            // Пройтись по вершинам
            foreach (Vertex<T> vertex in this.Vertices)
            {
                // Если значения искомой и текущей совпадают, то вернуть текущую
                if (vertex.Value.Equals(value))
                {
                    return vertex;
                }
            }

            // Если необходимо добавить новую вершину при отсутствии в списке вершины с заданным значением
            if (addIfNotExist)
            {
                // Создать вершину
                Vertex<T> newVertex = new(value);

                // Положить в список вершин и в список пар [<вершина> - <прилежащие к ней ребра>]
                this.Vertices.Add(newVertex);
                this.LinkedEdges.Add(newVertex, new());

                // Вернуть только что созданную вершину
                return newVertex;
            }
            // Иначе выбросить ошибку
            else
            {
                throw new ArgumentException(
                    $"Вершина со значением \"{value.ToString()}\" не найдена"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");

            // При наличии ошибки вернуть пустую вершину
            return new Vertex<T>();
        }
    }

    // Получить вершину из списка по индексу
    public Vertex<T> GetVertexByIndex(int index)
    {
        return this.Vertices[index];
    }

    // Получить индекс вершины из списка
    public int GetVertexIndex(Vertex<T> vertex)
    {
        return this.Vertices.IndexOf(vertex);
    }

    // Проверка на существование вершины
    public bool HasVertex(T value)
    {
        foreach (Vertex<T> vertex in this.Vertices)
        {
            if (vertex.Value.Equals(value))
            {
                return true;
            }
        }
        return false;
    }

    // Преобразование графа в матрицу смежности, значениями которой являются веса ребер,
    // а строки и столбцы - индексы вершин из списка
    public Matrix ToMatrix()
    {
        Matrix matrix = new();

        for (int i = 0; i < this.Vertices.Count; i++)
        {
            matrix.Add(new());

            for (int j = 0; j < this.Vertices.Count; j++)
            {
                matrix[i].Add(this.GetStraightWay(this.Vertices[i], this.Vertices[j]));
            }
        }

        return matrix;
    }

    // Получение прямого пути от одной вершины до другой, т.е. расстояние между вершинами,
    // лежащими на одном ребре
    public int GetStraightWay(Vertex<T> firstVertex, Vertex<T> secondVertex)
    {
        // Если вершины совпадают, то вернуть максимально возможное целое число ("бесконечность")
        if (firstVertex == secondVertex)
        {
            return Int32.MaxValue;
        }

        // Получить список прилежащих ребер
        List<Edge<T>> linkedEdges = this.LinkedEdges[firstVertex];

        // Пройтись по каждому
        foreach (Edge<T> edge in linkedEdges)
        {
            // Если расстояние между заданными вершинами и вершинами, лежащими на текущем ребре, равно
            if (
                (edge.Vertices[0] == firstVertex && edge.Vertices[1] == secondVertex)
                || (edge.Vertices[1] == firstVertex && edge.Vertices[0] == secondVertex)
            )
            {
                // Вернуть это расстояние
                return edge.Weight;
            }
        }

        // Если не найдено таких ребер, расстояние между вершинами которого, равно расстоянию между заданными
        // вершинами, то вернуть "бесконечность"
        return Int32.MaxValue;
    }

    // Проверка на содержание ребра в спике прилежащих к заданной вершине
    private bool _IsEdgeContained(Vertex<T> vertex, Edge<T> edge)
    {
        foreach (Edge<T> otherEdge in this.LinkedEdges[vertex])
        {
            if (otherEdge == edge)
                return true;
        }
        return false;
    }
}
