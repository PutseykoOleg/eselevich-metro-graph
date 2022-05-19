using Metro.Graph;
using Metro.Structs;

namespace Metro.Algorithms;

// Алиас матрицы смежности
using Matrix = List<List<int>>;

// Класс, описывающий методы работы с алгоритмом Флойда-Уоршелла (T - тип значения вершины графа)
public class FloydWarshallAlgorithm<T> where T : notnull
{
    // Граф
    public Graph<T> Graph { get; private set; }

    // Матрица смежности (используется для хранения уже вычисленных значений)
    private Matrix? _matrix { get; set; } = null;

    // Матрица, хранящая историю посещения вершин, т.е. индексы следующих в пути вершин (используется для хранения уже вычисленных значений)
    private Matrix? _nextVertexIndexMatrix { get; set; } = null;

    // Конструктор
    public FloydWarshallAlgorithm(Graph<T> graph)
    {
        this.Graph = graph;
    }

    // Метод получения кратчайщего пути
    public Pair<List<T>, int> GetShortestPath(T startVertexValue, T endVertexValue)
    {
        // Получение вершин из графа
        Vertex<T> startVertex = this.Graph.GetVertex(startVertexValue);
        Vertex<T> endVertex = this.Graph.GetVertex(endVertexValue);

        // Получение их индексов в списке вершин графа
        int startIndex = this.Graph.GetVertexIndex(startVertex);
        int endIndex = this.Graph.GetVertexIndex(endVertex);

        // Если все уже было посчитано раньше, то вернуть результат из сохраненных значений матриц
        if (this._matrix != null && this._nextVertexIndexMatrix != null)
        {
            return new Pair<List<T>, int>(this._GetPath(this._nextVertexIndexMatrix, startIndex, endIndex), this._matrix[startIndex][endIndex]);
        }

        // Конвертация графа в матрицу
        this._matrix = new(this.Graph.ToMatrix());
        // Матрица, хранящая историю посещения вершин, т.е. индексы следующих в пути вершин
        this._nextVertexIndexMatrix = new();

        // Размер строки матрицы (длина списка вершин)
        int lineLength = this.Graph.Vertices.Count;

        // Начальные значения матрицы истории посещения вершин
        for (int i = 0; i < lineLength; i++)
        {
            this._nextVertexIndexMatrix.Add(new());
            for (int j = 0; j < lineLength; j++)
            {
                // - Если индексы вершин совпадают => петля => в вершину пришли из нее же => установить индекс j

                // - Если не совпадают => если на пересечении - не "бесконечность" => ребро =>
                //   => из вершины i попадем в j => ставим индекс j

                // - Если не совпадают => если на пересечении - "бесконечность" => нет прямого пути из i в j =>
                //   => оставляем "бесконечность"
                this._nextVertexIndexMatrix[i].Add(i == j ? j : this._matrix[i][j] != Int32.MaxValue ? j : Int32.MaxValue);
            }
        }

        // Для каждой вершины
        for (int k = 0; k < lineLength; k++)
        {
            // Для каждой строки матрицы
            for (int i = 0; i < lineLength; i++)
            {
                // Для каждого столбца матрицы
                for (int j = 0; j < lineLength; j++)
                {
                    // Если индексы строки и столбца не совпадают, т.е. не петля и при этом существует путь от i до j, проходящий
                    // через текущую вершину (k)
                    if (i != j && this._matrix[i][k] != Int32.MaxValue && this._matrix[k][j] != Int32.MaxValue)
                    {
                        // Если такой путь меньше уже проложенного между i и j
                        if (this._matrix[i][k] + this._matrix[k][j] < this._matrix[i][j])
                        {
                            // Задать следующую вершину в пути
                            this._nextVertexIndexMatrix[i][j] = this._nextVertexIndexMatrix[i][k];
                            // Установить новый кратчайший путь
                            this._matrix[i][j] = this._matrix[i][k] + this._matrix[k][j];
                        }
                    }
                }
            }
        }

        // Вернуть пару <список значений вершин, т.е. путь> - <длина пути>
        return new Pair<List<T>, int>(this._GetPath(this._nextVertexIndexMatrix, startIndex, endIndex), this._matrix[startIndex][endIndex]);
    }

    // Восстановление пути между заданными вершинами
    private List<T> _GetPath(Matrix nextVertexIndexMatrix, int startVertexIndex, int endVertexIndex)
    {
        // Если пути между ними не существует
        if (nextVertexIndexMatrix[startVertexIndex][endVertexIndex] == Int32.MaxValue)
        {
            // Вернуть пустой путь
            return new();
        }

        // Путь с первым значением - начальная вершина
        List<T> path = new() { this.Graph.GetVertexByIndex(startVertexIndex).Value };

        // Пока не дошли до конечной вершины
        while(startVertexIndex != endVertexIndex)
        {
            // Инициализация следующим в пути индексом
            startVertexIndex = nextVertexIndexMatrix[startVertexIndex][endVertexIndex];
            // Добавить в путь значение текущей вершины
            path.Add(this.Graph.GetVertexByIndex(startVertexIndex).Value);
        }

        // Вернуть путь
        return path;
    }
}

