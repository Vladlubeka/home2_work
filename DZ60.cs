using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<string> collection = new List<string>();

        Console.WriteLine("Введите элементы коллекции (для завершения введите 'exit'):");

        string input;
        while ((input = Console.ReadLine()) != "exit")
        {
            collection.Add(input);
        }

        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Фильтрация");
        Console.WriteLine("2. Сортировка");
        Console.WriteLine("3. Группировка");
        Console.WriteLine("4. Преобразование");
        Console.WriteLine("5. Агрегация");
        Console.WriteLine("6. Выход");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Введите условие для фильтрации:");
                string filterCondition = Console.ReadLine();
                var filteredCollection = collection.Where(item => item.Contains(filterCondition));
                PrintCollection(filteredCollection);
                break;
            case "2":
                var sortedCollection = collection.OrderBy(item => item);
                PrintCollection(sortedCollection);
                break;
            case "3":
                var groupedCollection = collection.GroupBy(item => item.Length);
                foreach (var group in groupedCollection)
                {
                    Console.WriteLine($"Группа по длине {group.Key}:");
                    PrintCollection(group);
                }
                break;
            case "4":
                var transformedCollection = collection.Select(item => item.ToUpper());
                PrintCollection(transformedCollection);
                break;
            case "5":
                int aggregateResult = collection.Select(item => item.Length).Sum();
                Console.WriteLine($"Сумма длин элементов коллекции: {aggregateResult}");
                break;
            case "6":
                return;
            default:
                Console.WriteLine("Некорректный выбор операции.");
                break;
        }

        SaveToXml(collection);
    }

    static void PrintCollection<T>(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }
    }

    static void SaveToXml(List<string> collection)
    {
        XDocument xmlDocument = new XDocument(
            new XElement("Collection",
                collection.Select(item => new XElement("Item", item))
            )
        );

        string filePath = "collection.xml";
        xmlDocument.Save(filePath);
        Console.WriteLine($"Данные сохранены в файл {filePath}");
    }
}
