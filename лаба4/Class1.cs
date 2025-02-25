using System;
using System.Collections.Generic;
using System.IO;
using Лаба4;

namespace Лаба4
{
    public class C1
    {
        // 1. Проверка на наличие одинаковых элементов в списке
        public static bool Check<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[i]?.Equals(list[j]) == true)
                    {
                        return true; // Дубликат найден
                    }
                }
            }
            return false; // Дубликатов нет
        }

        // 2. Удаление первого вхождения элемента из LinkedList
        public static bool RemoveFirst<T>(LinkedList<T> list, T element)
        {
            // Поиск узла с указанным элементом
            LinkedListNode<T> currentNode = list.First;

            while (currentNode != null)
            {
                if (currentNode.Value?.Equals(element) == true)
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

        // 3. Определение музыкальных произведений
        public static void AnalyzeMusic(HashSet<string> songs, List<HashSet<string>> musicLovers)
        {
            var allLike = new HashSet<string>(songs);
            var someLike = new HashSet<string>();
            var noneLike = new HashSet<string>(songs);

            foreach (var lover in musicLovers)
            {
                allLike.IntersectWith(lover);
                someLike.UnionWith(lover);
            }

            noneLike.ExceptWith(someLike);

            Console.WriteLine("Нравятся всем: " + string.Join(", ", allLike));
            Console.WriteLine("Нравятся некоторым: " + string.Join(", ", someLike));
            Console.WriteLine("Не нравятся никому: " + string.Join(", ", noneLike));
        }

        // 4. Печать гласных букв из файла
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

            // Печатаем результат
            PrintVowels(uniqueVowels);
        }

        // Метод для получения уникальных гласных
        public static HashSet<char> GetUniqueVowels(string text)
        {
            // Определяем гласные буквы
            char[] vowels = new char[] { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };

            // Приводим текст к нижнему регистру и разбиваем на слова
            string[] words = text.ToLower().Split(new char[] { ' ', ',', '.', ';', ':', '-', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            // Список содержащий гласные буквы слов
            List<char> vowelInWords = new List<char>();

            // Проходим по каждому слову
            foreach (var word in words)
            {
                // Создаем HashSet для хранения уникальных элемен
                HashSet<char> vowelsInCurrentWord = new HashSet<char>();

                // Проходим по буквам в слове
                foreach (char letter in word)
                {
                    // Если буква - гласная, добавляем в текущее множество
                    if (Array.Exists(vowels, v => v == letter))
                    {
                        vowelsInCurrentWord.Add(letter);
                    }
                }

                // Добавляем найденные гласные в общий список
                vowelInWords.AddRange(vowelsInCurrentWord);
            }

            // Создаем HashSet для хранения уникальных гласных
            HashSet<char> uniqueVowels = new HashSet<char>();

            int k;

            foreach (var i in vowelInWords)
            {
                k = 0;
                foreach (var j in vowelInWords)
                {
                    if (i == j)
                    {
                        k++;
                    }
                }
                if (k == 1)
                {
                    uniqueVowels.Add(i);
                }
            }

            return uniqueVowels; // Возвращаем уникальные гласные
        }

        public static void PrintVowels(HashSet<char> uniqueVowels)
        {
            // Проверяем, есть ли уникальные гласные
            if (uniqueVowels.Count == 0)
            {
                Console.WriteLine("В вашем тексте нет уникальных гласных букв.");
            }
            else
            {
                // Сортируем уникальные гласные
                List<char> sortedUniqueVowels = new List<char>(uniqueVowels);
                sortedUniqueVowels.Sort();

                // Печатаем результат
                Console.WriteLine("Уникальные гласные буквы в алфавитном порядке:");
                Console.WriteLine(string.Join(", ", sortedUniqueVowels));
            }
        }
    }
}
