using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Triangle;
using static TriangleDetails;

internal class Program
{
    static void Main(string[] args)
    {
        int a, b, c;

        while (true) 
        { 
            Console.WriteLine("Введите стороны треугольника (целые числа):");
       
            while (true)
            {
                Console.Write("Сторона A: ");
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out a))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод данных.");
                    }
                }
                Console.Write("Сторона B: ");
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out b))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод данных.");
                    }
                }
                Console.Write("Сторона C: ");
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out c))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод данных.");
                    }
                }
                // Проверка на существование треугольника
                if (a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
                {
                    Console.WriteLine("Треугольник с такими сторонами не может существовать.");
                }
                else
                {
                    break;
                }
            }
        

            // Создание экземпляра базового класса
            Triangle triangle = new Triangle(a, b, c);
            Console.WriteLine(triangle.ToString());
            // Тестирование конструкутора копирования 
            Console.WriteLine("\nТестирование конструкутора копирования:");
            Triangle triangle1 = triangle;
            Console.WriteLine(triangle1.ToString());
            Console.WriteLine($"Минимальная последняя цифра сторон: {triangle.MinLastDigit()}");
        
            // Создание экземпляра дочернего класса
            TriangleDetails triangleDetails = new TriangleDetails(a, b, c);
            Console.WriteLine($"Периметр треугольника: {triangleDetails.P()}");
            Console.WriteLine($"Площадь треугольника: {triangleDetails.Area()}");
        }
            Console.ReadKey();
    }
}


