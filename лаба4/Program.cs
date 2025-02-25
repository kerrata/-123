using System;
using System.Collections.Generic;
using Лаба4;

public class Program
{
    public static void Main(string[] args)
    {
        // 1. Проверка на наличие одинаковых элементов в списке
        Console.WriteLine("Задание 1.");
        List<string> num = new List<string> { "543", "2", "3", "-13", "5", "1", "мама" };

        Console.Write("Список: ");
        Console.WriteLine(string.Join(", ", num));
        if (C1.Check(num))
        {
            Console.WriteLine("В списке есть одинаковые элементы.");

        }
        else
        {
            Console.WriteLine("В списке нет одинаковых элементов.");
        }
        List<int> num2 = new List<int> { 543, 2, 3, -13, 2, 5, 1 };
        Console.Write("Список: ");
        Console.WriteLine(string.Join(", ", num2));
        if (C1.Check(num2))
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
        if (C1.RemoveFirst(L, elementToRemove))
        {
            Console.WriteLine("Список после удаления первого вхождения элемента: " + string.Join(", ", L));
        }
        else
        {
            Console.WriteLine("В этом списке нет первого вхождения данного элемента.");

        }

        LinkedList<int> L1 = new LinkedList<int>(new[] { 56, 32, 87, -1, 0, 32, -32, 0 });
        Console.Write("Связный список: ");
        Console.WriteLine(string.Join(", ", L1));
        Console.Write("Введите элемент для удаления: ");
        // Удаление элемента и вывод результата
        string input = Console.ReadLine();
        if (int.TryParse(input, out int elementToRemove1))
        {
            if (C1.RemoveFirst(L1, elementToRemove1))
            {
                Console.WriteLine("Список после удаления первого вхождения элемента: " + string.Join(", ", L1));
            }
            else
            {
                Console.WriteLine("В этом списке нет первого вхождения данного элемента.");

            }
        }
        else
        {
            // Обработка ошибки, если ввод не является целым числом
            Console.WriteLine("Введите корректное целое число.");
        }

        

        // 3. Определение музыкальных произведений
        Console.WriteLine("\nЗадание 3.");
        var songs = new HashSet<string> { "Крутая песня", "Громкая песня", "Глупая песня", "Скучная песня" };

        // Предпочтения меломанов
        var musicLovers = new List<HashSet<string>>
        {
            new HashSet<string> { "Громкая песня", "Крутая песня" }, // Меломан 1
            new HashSet<string> { "Крутая песня" }, // Меломан 2
            new HashSet<string> { "Глупая песня", "Крутая песня" } // Меломан 3
        };

        C1.AnalyzeMusic(songs, musicLovers);


        // 4. Печать гласных букв из файла
        Console.WriteLine("\nЗадание 4.");
        C1.PrintUniqueVowels("text.txt");

        Console.ReadKey();
    }
}
