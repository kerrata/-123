using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

internal class Triangle
{
    private int sideA;
    private int sideB;
    private int sideC;

    // Конструктор
    public Triangle(int a, int b, int c)
    {
        sideA = a;
        sideB = b;
        sideC = c;
    }

    // Конструктор копирования
    public Triangle(Triangle triangle)
    {
        sideA = triangle.sideA;
        sideB = triangle.sideB;
        sideC = triangle.sideC;
    }

    // Метод для нахождения минимальной последней цифры сторон
    public int MinLastDigit()
    {
        int lastDigitA = Math.Abs(sideA) % 10;
        int lastDigitB = Math.Abs(sideB) % 10;
        int lastDigitC = Math.Abs(sideC) % 10;

        return Math.Min(lastDigitA, Math.Min(lastDigitB, lastDigitC));
    }

    // Переопределение ToString()
    public override string ToString()
    {
        return $"Стороны треугольника: A = {sideA}, B = {sideB}, C = {sideC}";
    }
    //свойство для доступа к приватному полю главного класса
    public int SideA => sideA;
    public int SideB => sideB;
    public int SideC => sideC;
}


