﻿using Metro.Algorithms;
using Metro.Graph;

/**
 * Данная программа работает с любым неориентированным взвешенным графом,
 * т.е. для работы с метро другого города необходимо изменить входные данные,
 * представленные ниже.
 * 
 * В данном случае в качестве весов ребер графа было выбрано время пути между двумя
 * соседними станциями. При необходимосит его можно заменить на расстояние между этими станциями.
 * 
 * Время пути взято с сайта https://yandex.ru/metro/saint-petersburg?scheme_id=sc60983525
 */

// Граф, у которого значения вершин - названия станций, а веса ребер - время пути между соседними станциями
Graph<string> graph = new();

// Невско-Василеостровская ветка
graph.AddEdge("Беговая", "Зенит", 4);
graph.AddEdge("Зенит", "Приморская", 4);
graph.AddEdge("Приморская", "Василеостровская", 4);
graph.AddEdge("Василеостровская", "Гостиный двор", 4);
graph.AddEdge("Гостиный двор", "Маяковская", 3);
graph.AddEdge("Маяковская", "Площадь Александра Невского-1", 3);
graph.AddEdge("Площадь Александра Невского-1", "Елизаровская", 4);
graph.AddEdge("Елизаровская", "Ломоносовская", 3);
graph.AddEdge("Ломоносовская", "Пролетарская", 4);
graph.AddEdge("Пролетарская", "Обухово", 3);
graph.AddEdge("Обухово", "Рыбацкое", 4);

// Фрузенско-Приморская ветка
graph.AddEdge("Комендантский проспект", "Старая Деревня", 4);
graph.AddEdge("Старая Деревня", "Крестовский остров", 3);
graph.AddEdge("Крестовский остров", "Чкаловская", 3);
graph.AddEdge("Чкаловская", "Спортивная", 2);
graph.AddEdge("Спортивная", "Адмиралтейская", 3);
graph.AddEdge("Адмиралтейская", "Садовая", 3);
graph.AddEdge("Садовая", "Звенигородская", 2);
graph.AddEdge("Звенигородская", "Обводный канал", 3);
graph.AddEdge("Обводный канал", "Волковская", 2);
graph.AddEdge("Волковская", "Бухарестская", 3);
graph.AddEdge("Бухарестская", "Международная", 3);
graph.AddEdge("Международная", "Проспект Славы", 2);
graph.AddEdge("Проспект Славы", "Дунайская", 3);
graph.AddEdge("Дунайская", "Шушары", 3);

// Московско-Петроградская ветка
graph.AddEdge("Парнас", "Проспект Просвещения", 3);
graph.AddEdge("Проспект Просвещения", "Озерки", 3);
graph.AddEdge("Озерки", "Удельная", 3);
graph.AddEdge("Удельная", "Пионерская", 3);
graph.AddEdge("Пионерская", "Черная речка", 3);
graph.AddEdge("Черная речка", "Петроградская", 3);
graph.AddEdge("Петроградская", "Горьковская", 2);
graph.AddEdge("Горьковская", "Невский проспект", 4);
graph.AddEdge("Невский проспект", "Сенная площадь", 2);
graph.AddEdge("Сенная площадь", "Технологический институт-2", 2);
graph.AddEdge("Технологический институт-2", "Фрузенская", 2);
graph.AddEdge("Фрузенская", "Московские ворота", 3);
graph.AddEdge("Московские ворота", "Электросила", 2);
graph.AddEdge("Электросила", "Парк Победы", 2);
graph.AddEdge("Парк Победы", "Московская", 3);
graph.AddEdge("Московская", "Звездная", 5);
graph.AddEdge("Звездная", "Купчино", 3);

// Кировско-Выборгская ветка
graph.AddEdge("Девяткино", "Гражданский проспект", 4);
graph.AddEdge("Гражданский проспект", "Академическая", 3);
graph.AddEdge("Академическая", "Политехническая", 2);
graph.AddEdge("Политехническая", "Площадь Мужества", 2);
graph.AddEdge("Площадь Мужества", "Лесная", 3);
graph.AddEdge("Лесная", "Выборгская", 4);
graph.AddEdge("Выборгская", "Площадь Ленина", 2);
graph.AddEdge("Площадь Ленина", "Чернышевская", 3);
graph.AddEdge("Чернышевская", "Площадь Восстания", 3);
graph.AddEdge("Площадь Восстания", "Владимирская", 2);
graph.AddEdge("Владимирская", "Пушкинская", 2);
graph.AddEdge("Пушкинская", "Технологический институт-1", 2);
graph.AddEdge("Технологический институт-1", "Балтийская", 2);
graph.AddEdge("Балтийская", "Нарвская", 3);
graph.AddEdge("Нарвская", "Кировский завод", 4);
graph.AddEdge("Кировский завод", "Автово", 2);
graph.AddEdge("Автово", "Ленинский проспект", 3);
graph.AddEdge("Ленинский проспект", "Проспект Ветеранов", 2);

// Правобережная ветка
graph.AddEdge("Спасская", "Достоевская", 4);
graph.AddEdge("Достоевская", "Лиговский проспект", 2);
graph.AddEdge("Лиговский проспект", "Площадь Александра Невского-2", 2);
graph.AddEdge("Площадь Александра Невского-2", "Новочеркасская", 3);
graph.AddEdge("Новочеркасская", "Ладожская", 3);
graph.AddEdge("Ладожская", "Проспект Большевиков", 3);
graph.AddEdge("Проспект Большевиков", "Улица Дыбенко", 3);

// Переходы
graph.AddEdge("Гостиный двор", "Невский проспект", 4);
graph.AddEdge("Маяковская", "Площадь Восстания", 3);
graph.AddEdge("Площадь Александра Невского-1", "Площадь Александра Невского-2", 3);
graph.AddEdge("Садовая", "Сенная площадь", 4);
graph.AddEdge("Сенная площадь", "Спасская", 4);
graph.AddEdge("Спасская", "Садовая", 3);
graph.AddEdge("Звенигородская", "Пушкинская", 3);
graph.AddEdge("Технологический институт-2", "Технологический институт-1", 2);
graph.AddEdge("Владимирская", "Достоевская", 3);

// Алгоритм Флойда-Уоршелла
FloydWarshallAlgorithm<string> fwAlgorithm = new(graph);
// Результат вичисления кратчайшего пути - пара [<список значений вершин, т.е. сам путь>, <длина пути>]
var result = fwAlgorithm.GetShortestPath("Беговая", "Площадь Ленина");

// Путь
var path = result.First;
// Длина пути (в данном случае - общее время в пути)
var length = result.Second;

// Вывод результата в консоль
Console.WriteLine($"\nВремя в пути: {length} мин");
Console.WriteLine("\nМаршрут:");

for (int i = 0; i < path.Count; i++)
{
    if (i == 0)
        Console.WriteLine(" _\n |");

    Console.WriteLine($" | - {path[i]}\n |");

    if (i == path.Count - 1)
        Console.WriteLine(" V");
}
