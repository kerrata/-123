using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        
        double a, b, c;
        
        while (true)
        { 
            Console.WriteLine("\nВведите коэффициенты квадратного уравнения (a, b, c):");
        
            while (true)
            {
                Console.Write("Коэффициент a: ");
                if (double.TryParse(Console.ReadLine(), out a))
                {
                    if (a == 0)
                    {
                        Console.WriteLine("Уравнение не является квадратным.");
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных.");
                }
            }
        
            while (true)
            {
                Console.Write("Коэффициент b: ");
                if (double.TryParse(Console.ReadLine(), out b))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных.");
                }
            }
        
            while (true)
            {
                Console.Write("Коэффициент c: ");
                if (double.TryParse(Console.ReadLine(), out c))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных");
                }
            }

            // Создание экземпляра класса QuadraticEquation
            QuadraticEquation equation = new QuadraticEquation(a, b, c);
            Console.WriteLine(equation.ToString());

            // Вычисление корней
            // Результат в виде маасива величины типа double
            double[] roots = equation.CalculateRoots();
            if (roots.Length == 0)
            {
                Console.WriteLine("Нет действительных корней.");
            }
            else
            {
                Console.WriteLine("Корни уравнения:");
                for(int i = 0; i < roots.Length; i++)
                {   
                    var root = roots[i];
                    Console.Write($"{Math.Round(root, 2)} ");
                   ;
                }
            }
           
            // Тестирование унарных операций
            var incrementedEquation = ++equation;
            Console.WriteLine($"\nУвеличенное уравнение: {incrementedEquation}");

            var decrementedEquation = --equation;
            Console.WriteLine($"Уменьшенное уравнение: {decrementedEquation}");

            // Тестирование явного приведения к bool
            bool hasRoots = (bool)equation;
            Console.WriteLine($"Существуют ли корни? {hasRoots}");

            // Тестирование неявного приведения к дискриминанту
            double discriminant = equation;
            Console.WriteLine($"Дискриминант: {discriminant}");

            // Тестирование бинарных операций
            Console.WriteLine("\nВведите коэффициенты второго квадратного уравнения (a1, b1, c1):");
        
            while (true)
            {
                Console.Write("Коэффициент a1: ");
                if (double.TryParse(Console.ReadLine(), out a))
                {
                    if (a == 0)
                    {
                        Console.WriteLine("Уравнение не является квадратным.");
                    }

                    else
                    {
                        break;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных.");
                }
            }
        
            while (true)
            {
                Console.Write("Коэффициент b1: ");
                if (double.TryParse(Console.ReadLine(), out b))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных.");
                }
            }
        
            while (true)
            {
                Console.Write("Коэффициент c1: ");
                if (double.TryParse(Console.ReadLine(), out c))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод данных.");
                }
            }
            var anotherEquation = new QuadraticEquation(a, b, c);
            Console.WriteLine(anotherEquation.ToString());
            Console.WriteLine($"Уравнения равны? {equation == anotherEquation}");
            Console.WriteLine($"Уравнения не равны? {equation != anotherEquation}");
        }
        Console.ReadKey();  
    }
}

