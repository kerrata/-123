using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuadraticEquation
{
    // Поля для коэффициентов
    private double a;
    private double b;
    private double c;

    // Конструктор
    public QuadraticEquation(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    // Свойства для доступа к коэффициентам
    public double A { get => a; set => a = value; }
    public double B { get => b; set => b = value; }
    public double C { get => c; set => c = value; }

    // Метод для вычисления корней квадратного уравнения
    public double[] CalculateRoots()
    {
        double discriminant = Discriminant();
        if (discriminant < 0)
        {
            return new double[0]; // Нет корней
        }
        else if (discriminant == 0)
        {
            return new double[] { -b / (2 * a) }; // Один корень
        }
        else
        {
            //Console.WriteLine(Math.Sqrt(discriminant));
            double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            return new double[] { root1, root2 }; // Два корня
        }
    }

    // Метод для вычисления дискриминанта
    private double Discriminant()
    {
        return b * b - 4 * a * c;
    }

    // Перегрузка ToString()
    public override string ToString()
    {
        return $"Квадратное уравнение: {a}x^2 + {b}x + {c} = 0";
    }

    // Унарная операция ++
    public static QuadraticEquation operator ++(QuadraticEquation equation)
    {
        return new QuadraticEquation(equation.a + 1, equation.b + 1, equation.c + 1);
    }

    // Унарная операция --
    public static QuadraticEquation operator --(QuadraticEquation equation)
    {
        return new QuadraticEquation(equation.a - 1, equation.b - 1, equation.c - 1);
    }

    // Операция приведения типа (неявная) к дискриминанту
    public static implicit operator double(QuadraticEquation equation)
    {
        return equation.Discriminant();
    }

    // Операция приведения типа (явная) к bool
    public static explicit operator bool(QuadraticEquation equation)
    {
        return equation.Discriminant() >= 0;
    }

    // Бинарная операция ==
    public static bool operator ==(QuadraticEquation eq1, QuadraticEquation eq2)
    {
        return eq1.a == eq2.a && eq1.b == eq2.b && eq1.c == eq2.c;
    }

    // Бинарная операция !=
    public static bool operator !=(QuadraticEquation eq1, QuadraticEquation eq2)
    {
        return !(eq1 == eq2);
    }
}
