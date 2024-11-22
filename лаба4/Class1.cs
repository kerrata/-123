using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ЛАба4;

namespace ЛАба4
{
    public class C1
    {
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

            // Словарь для хранения уникальных гласных
            Dictionary<char, HashSet<string>> vowelInWords = new Dictionary<char, HashSet<string>>();

            // Проходим по каждому слову
            foreach (var word in words)
            {
                // Создаем множество для хранения уникальных гласных в текущем слове
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

                // Добавляем найденные гласные в общий словарь
                foreach (var vowel in vowelsInCurrentWord)
                {
                    if (!vowelInWords.ContainsKey(vowel))
                    {
                        vowelInWords[vowel] = new HashSet<string>();
                    }
                    vowelInWords[vowel].Add(word); // Добавляем слово, где встречается гласная
                }
            }

            // Создаем HashSet для хранения уникальных гласных
            HashSet<char> uniqueVowels = new HashSet<char>();

            // Добавляем в уникальные гласные только те, которые встречаются ровно в одном слове
            foreach (var pair in vowelInWords)
            {
                if (pair.Value.Count == 1) // Гласная встречается только в одном слове
                {
                    uniqueVowels.Add(pair.Key);
                }
            }

            return uniqueVowels; // Возвращаем уникальные гласные
        }

        public static void PrintVowels(HashSet<char> uniqueVowels)
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
