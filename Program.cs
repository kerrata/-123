using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {

        // 1. Проверка на наличие одинаковых элементов в списке
        Console.WriteLine("Задание 1.");
        List<string> num = new List<string> { "543", "2", "3", "-13", "5", "1", "мама" };
        Console.Write("Список: ");
        Console.WriteLine(string.Join(", ", num));
        if (Check(num))
        {
            Console.WriteLine("В списке есть одинаковые элементы.");

        }
        else
        {
            Console.WriteLine("В списке нет одинаковых элементов.");
        }
        List<string> num1 = new List<string> { "543", "2", "мама", "-13", "5", "1", "мама" };
        Console.Write("Список: ");
        Console.WriteLine(string.Join(", ", num1));
        if (Check(num1))
        {
            Console.WriteLine("В списке есть одинаковые элементы.");

        }
        else
        {
            Console.WriteLine("В списке нет одинаковых элементов.");
        }

        // 2. Удаление первого вхождения элемента из LinkedList
        Console.WriteLine("\nЗадание 2.");
        LinkedList<string> L = new LinkedList<string>(new[] { "бананчик", "огурцы", "чипсы", "огурцы", "соль", "майонез", "бананчик", "соль", });
        Console.Write("Связный список: ");
        Console.WriteLine(string.Join(", ", L));
        Console.Write("Введите элемент для удаления: ");
        // Удаление элемента и вывод результата
        string elementToRemove = Console.ReadLine();
        if (RemoveFirst(L, elementToRemove))
        {
            Console.WriteLine("Список после удаления первого вхождения элемента: " + string.Join(", ", L));
        }
        else
        {
            Console.WriteLine("В этом списке нет первого вхождения данного элемента.");

        }

        // 3. Определение музыкальных произведений
        Console.WriteLine("\nЗадание 3.");
        Console.WriteLine("Перечень песен: 'Odetari - RUN!', 'Robin - Sway to My Beat in Cosmos','VALORANT - Die For You'");
        Console.Write("Cколько всего меломанов: ");
        int mel;
        HashSet<int> Song1 = new HashSet<int>();
        HashSet<int> Song2 = new HashSet<int>();
        HashSet<int> Song3 = new HashSet<int>();
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out mel) && mel > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число.");
            }
        }
        while (true)
        {
            Console.Write($"Cкольким меломанам поравилась песня 'Odetari - RUN!' : ");
            // Чтение числа N
            if (int.TryParse(Console.ReadLine(), out int N) && N >= 0 && N <= mel)
            {
                // Заполнение HashSet элементами N раз
                for (int i = 0; i < N; i++)
                {
                    Song1.Add(i); 
                }
                break;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число.");
            }
        }
        while (true)
        {
            Console.Write($"Cкольким меломанам поравилась песня 'Robin - Sway to My Beat in Cosmos' : ");
            // Чтение числа N
            if (int.TryParse(Console.ReadLine(), out int N1) && N1 >= 0 && N1 <= mel)
            {
                // Заполнение HashSet элементами N раз
                for (int i = 0; i < N1; i++)
                {
                    Song2.Add(i); 
                }
                break;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число.");
            }
        }
        while (true)
        {
            Console.Write($"Cкольким меломанам поравилась песня 'VALORANT - Die For You' : ");
            // Чтение числа N
            if (int.TryParse(Console.ReadLine(), out int N2) && N2 >= 0 && N2 <= mel)
            {
                // Заполнение HashSet элементами N раз
                for (int i = 0; i < N2; i++)
                {
                    Song3.Add(i); 
                }
                break;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число.");
            }
        }
        if ((Song1.Count == mel))
        {
            Console.WriteLine("песня 'Odetari - RUN!' нравится всем меломанам.");
        }
        else if (Song1.Count < mel && Song1.Count > 0)
        {
            Console.WriteLine("песня 'Odetari - RUN!' нравится некоторым меломанам.");
        }
        else if (Song1.Count == 0)
        {
            Console.WriteLine("песня 'Odetari - RUN!' никому не нравится.");
        }

        if ((Song2.Count == mel))
        {
            Console.WriteLine("песня 'Robin - Sway to My Beat in Cosmos' нравится всем меломанам.");
        }
        else if (Song2.Count < mel && Song2.Count > 0)
        {
            Console.WriteLine("песня 'Robin - Sway to My Beat in Cosmos' нравится некоторым меломанам.");
        }
        else if (Song2.Count == 0)
        {
            Console.WriteLine("песня 'Robin - Sway to My Beat in Cosmos' никому не нравится.");
        }

        if ((Song3.Count == mel))
        {
            Console.WriteLine("песня 'VALORANT - Die For You' нравится всем меломанам.");
        }
        else if (Song3.Count < mel && Song3.Count > 0)
        {
            Console.WriteLine("песня 'VALORANT - Die For You' нравится некоторым меломанам.");
        }
        else if (Song3.Count == 0)
        {
            Console.WriteLine("песня 'VALORANT - Die For You' никому не нравится.");
        }
    

        // 4. Печать гласных букв из файла
        Console.WriteLine("\nЗадание 4.");
        PrintUniqueVowels("text.txt");
    }
    static bool Check(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[i] == list[j])
                {
                    return true; // Дубликат найден
                }
            }
        }
        return false; // Дубликатов нет
    }
    static bool RemoveFirst(LinkedList<string> list, string element)
    {
        // Поиск узла с указанным элементом
        LinkedListNode<string> currentNode = list.First;

        while (currentNode != null)
        {
            if (currentNode.Value == element)
            {
                // Удаляем узел с помощью L.Remove
                list.Remove(currentNode);
                return true;
            }
            currentNode = currentNode.Next;
        }

        // Если элемент не найден, возвращаем false
        return false;
    }
    
    public static void PrintUniqueVowels(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не существует.");
            return;
        }

        var text = File.ReadAllText(filePath);
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Файл пуст.");
            return;
        }

        var uniqueVowels = GetUniqueVowels(text);
        PrintUniqueVowels(uniqueVowels);
    }

    // Метод для получения уникальных гласных
    private static HashSet<char> GetUniqueVowels(string text)
    {
        string vowels = "аеёиоуыэюяАЕЁИОУЫЭЮЯ";
        var words = text.Split(new[] { ' ', ',', '.', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
        var vowelCount = new Dictionary<char, int>();

        foreach (var word in words)
        {
            var foundVowels = new HashSet<char>();

            foreach (var ch in word)
            {
                if (vowels.Contains(ch) && !foundVowels.Contains(ch))
                {
                    foundVowels.Add(ch);
                    if (!vowelCount.ContainsKey(ch))
                    {
                        vowelCount[ch] = 0;
                    }
                    vowelCount[ch]++;
                }
            }
        }

        // Отбираем только те гласные, которые встречаются в одном слове
        var result = new HashSet<char>(vowelCount.Where(kvp => kvp.Value == 1).Select(kvp => kvp.Key));
        return result;
    }

    // Метод для вывода гласных
    private static void PrintUniqueVowels(HashSet<char> uniqueVowels)
    {
        if (uniqueVowels.Count == 0)
        {
            Console.WriteLine("Нет уникальных гласных.");
            return;
        }

        // Сортируем и выводим результат
        var sortedVowels = uniqueVowels.OrderBy(v => v);
        Console.WriteLine("Уникальные гласные: " + string.Join(", ", sortedVowels));
    }
}
