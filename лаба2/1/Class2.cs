using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

internal class TriangleDetails : Triangle
{
    public TriangleDetails(int a, int b, int c) : base(a, b, c) { }

    // Метод для нахождения периметра
    public int P()
    {
        return SideA + SideB + SideC;
    }

    // Метод для нахождения площади по формуле Герона
    public double Area()
    {
        double s = P() / 2.0;
        return Math.Round(Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC)),2);
    }
}