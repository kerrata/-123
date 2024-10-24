using System;
using System.Linq;
using System.Text;

internal class Program
{
    // задание 1.2 
    public int sumLastNums(int x)
    {

        int num1 = x % 10;
        x /= 10;
        int num2 = x % 10;

        return num1 + num2;
    }
    // задание 1.4
    public bool isPositive(int x)
    {
        if (x > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        if (x == 0)
        {
            return false;
        }
    }
    // задание 1.6
    public bool isUpperCase(char x)
    {
        //диапазон ASCII кода больших букв
        if (64 < (int)x && (int)x < 91)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //1.8
    public bool isDivisor(int a, int b)
    {
        if (a != 0 && b != 0)
        {
            if (a % b == 0 || b % a == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (a == 0 && b != 0)
        {
            if (a % b == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (a != 0 && b == 0)
        {
            if (b % a == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    //задание 1.10
    public int lastNumSum(int a, int b)
    {
        return a % 10 + b % 10;
    }
    // задание 2.2
    public double safeDiv(int x, int y)
    {
        if (y != 0)
        {
            return (double)x / y;
        }
        else
        {
            return 0;
        }
    }
    // задание 2.4
    public String makeDecision(int x, int y)
    {
        if (x > y)
        {
            return x.ToString() + " > " + y.ToString();
        }
        else if (x < y)
        {
            return x.ToString() + " < " + y.ToString();
        }
        else
        {
            return x.ToString() + " == " + y.ToString();
        }
    }
    // задание 2.6
    public bool sum3(int x, int y, int z)
    {
        if (x + y == z || x + z == y || y + z == x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // задание 2.8
    public String age(int x)
    {
        if (x % 10 == 1 && x != 11)
        {
            return x.ToString() + " год";
        }
        else if ((x % 10 == 2 || x % 10 == 3 || x % 10 == 4) && x != 12 && x != 13 && x != 14)
        {
            return x.ToString() + " года";
        }
        else
        {
            return x.ToString() + " лет";
        }
    }

    // задание 2.10
    public void printDays(String x)
    {
        x = x.ToLower();
        Console.WriteLine(x);
        switch (x)
        {
            case "понедельник":
                Console.WriteLine("понедельник\nвторник\nсреда\nчетверг\nпятница\nсуббота\nвоскресенье");
                break;
            case "вторник":
                Console.WriteLine("вторник\nсреда\nчетверг\nпятница\nсуббота\nвоскресенье");
                break;
            case "среда":
                Console.WriteLine("среда\nчетверг\nпятница\nсуббота\nвоскресенье");
                break;
            case "четверг":
                Console.WriteLine("четверг\nпятница\nсуббота\nвоскресенье");
                break;
            case "пятница":
                Console.WriteLine("пятница\nсуббота\nвоскресенье");
                break;
            case "суббота":
                Console.WriteLine("суббота\nвоскресенье");
                break;
            case "воскресенье":
                Console.WriteLine("воскресенье\n");
                break;
            default:
                Console.WriteLine("Это не день недели.\n");
                break;
        }
    }
    // задание 3.2
    public String reverseListNums(int x)
    {
        string s = "";
        if (x > 0)
        {
            while (x >= 0)
            {
                s = s + x--.ToString() + " ";
            }
        }
        else
        {
            while (x <= 0)
            {
                s = s + x++.ToString() + " ";
            }
        }
        return s;
    }

    // задание 3.4
    public int pow(int x, int y)
    {
        int t = x;
        for (int i = 0; i < y - 1; i++)
        {
            t *= x;
        }
        return t;
    }

    // задание 3.6
    public bool equalNum(int x)
    {
        int t = x % 10;
        x /= 10;
        //сравниваем цифры числа по очереди, если хоть один раз цифры разные, то false
        while (x > 0)
        {
            if (t == x % 10)
            {
                t = x % 10;
                x /= 10;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    // задание 3.8
    public void leftTriangle(int x)
    {
        string t = "";
        for (int i = 0; i < x; i++)
        {
            t += "*";
            Console.WriteLine(t);
        }
        Console.WriteLine('\n');
    }

    // задание 3.10
    public void guessGame()
    {
        int a, k = 0;
        Random rnd = new Random();
        int t = rnd.Next(0, 10);
        //ввод числа с проверкой на целое число и на вхождение в диапазон
        while (true)
        {
            k++;
            Console.WriteLine("Введите число от 0 до 9: ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    if (a >= 0 && a <= 9)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Нужно число от 0 до 9!");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод! Введите целое число!");
                }
            }
            if (a == t)
            {
                Console.WriteLine("Вы угадали!");
                Console.WriteLine(String.Format("Вы угадали за {0} попытки!\n", k));
                break;
            }
            else
            {
                Console.WriteLine("Вы не угадали!");
            }
        }
    }

    // задаие 4.2
    public int findLast(int[] arr, int x)
    {
        int t = -1; //принимает значение последнего вхождения элемента, если он есть
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x)
            {
                t = i;
            }
        }
        return t;
    }

    // задание 4.4
    public int[] add(int[] arr, int x, int pos)
    {
        int[] t = new int[arr.Length + 1];
        int k = 0;
        //вставляем в новый массив элементы из старого до нужной позиции
        for (int i = 0; i < pos; i++)
        {
            t[k++] = arr[i];
        }
        Array.Copy(arr, t, pos);
        //вставляем в новый массив данный элемент в нужную позиции
        t[pos] = x;
        k++;
        //вставляем в новый массив элементы из старого после нужной позиции
        for (int i = pos; i < arr.Length; i++)
        {
            t[k++] = arr[i];
        }
        return t;
    }

    // задание 4.6
    public void reverse(int[] arr)
    {
        //создаем новый массив и записываем в него элементы старого, задом наперед
        int[] t = new int[arr.Length];
        int k = arr.Length - 1;
        for (int i = 0; i < arr.Length; i++)
        {
            t[k--] = arr[i];
        }
        Console.WriteLine('\n' + string.Join(" ", t));
    }

    // задание 4.8
    public int[] concat(int[] arr1, int[] arr2)
    {
        int k = 0;
        int[] arr = new int[arr1.Length + arr2.Length];
        //создаем новый массив, вставляем в него элементы первого массива, а затем второго
        for (int i = 0; i < arr1.Length; i++)
        {
            arr[k++] = arr1[i];
        }
        for (int i = 0; i < arr2.Length; i++)
        {
            arr[k++] = arr2[i];
        }
        return arr;
    }

    // задание 4.10
    public int[] deleteNegative(int[] arr)
    {
        int k = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] >= 0)
            {
                k++;
            }
        }
        //создаем новый массив, записываем в него только положмтельные элементы из данного массива
        int[] t = new int[k];
        int j = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] >= 0)
            {
                t[j] = arr[i];
                j++;
            }
        }
        return t;
    }
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int a, b, c;
        int[] arr = null, arr1 = null;
        char x;
        string y, ar = "";
        Program p = new();
        while (true)
        {
            Console.WriteLine("Выберите номер задания (например 1.2): ");
            ar = Console.ReadLine();
            switch (ar)
            {
                case "1.2":
                    Console.WriteLine("\n№1.2: ");
                    Console.Write("Введите число для нахождения суммы его последних знаков: ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            a = Math.Abs(a);
                            if (a >= 10)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("В числе должно быть не менее двух знаков.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод данных.");
                        }
                    }
                    Console.WriteLine("\nСумма последних двух знаков: " + p.sumLastNums(a) + "\n");
                    break;
                case "1.4":
                    Console.WriteLine("\n№1.4: ");
                    Console.Write("Введите число: ");
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
                    Console.WriteLine("\nЧисло является положительным: " + p.isPositive(a) + "\n");
                    break;
                case "1.6":
                    Console.WriteLine("\n№1.6: ");
                    Console.Write("Введите символ: ");
                    while (true)
                    {
                        if (char.TryParse(Console.ReadLine(), out x))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод данных. Нужно ввести один латинский символ.");
                        }
                    }
                    Console.WriteLine("\nБуква является большой: " + p.isUpperCase(x) + "\n");
                    break;
                case "1.8":
                    Console.WriteLine("\n№1.8: ");
                    Console.Write("Введите первое число: ");
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
                    Console.Write("Введите второе число: ");
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
                    Console.WriteLine("\nЧисло делит другое нацело: " + p.isDivisor(a, b) + "\n");
                    break;
                case "1.10":
                    Console.WriteLine("\n№1.10: ");
                    Console.WriteLine("Введите 1-е число");
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
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine(String.Format("Введите {0}-е число", i + 2));
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
                        a = p.lastNumSum(a, b);
                        Console.WriteLine("\nРезультат:" + a + '\n');
                    }
                    break;
                case "2.2":
                    Console.WriteLine("\n№ 2.2: ");
                    Console.WriteLine("Введите первое число для деления: ");
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
                    Console.WriteLine("Введите второе число для деления: ");
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
                    Console.WriteLine("\nРезультат: " + p.safeDiv(a, b) + "\n");
                    break;
                case "2.4":
                    Console.WriteLine("\n№2.4: ");
                    Console.WriteLine("Введите первое число для сравнения: ");
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
                    Console.WriteLine("Введите второе число для сравнения: ");
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
                    Console.WriteLine("\nРезультат: " + p.makeDecision(a, b) + "\n");
                    break;
                case "2.6":
                    Console.WriteLine("\n№2.6: ");
                    Console.WriteLine("Введите первое число: ");
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
                    Console.WriteLine("Введите второе число: ");
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
                    Console.WriteLine("Введите третье число: ");
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
                    Console.WriteLine("\nРезультат: " + p.sum3(a, b, c) + "\n");
                    break;
                case "2.8":
                    Console.WriteLine("\n№2.8: ");
                    Console.WriteLine("Введите число, обозначающее возраст: ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            if (a > 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Отрицательное число не может обозначать возраст.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод данных");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + p.age(a) + "\n");
                    break;
                case "2.10":
                    Console.WriteLine("\n№2.10: ");
                    Console.WriteLine("Введите день недели: ");
                    y = Console.ReadLine();
                    Console.WriteLine("\nРезультат: ");
                    p.printDays(y);
                    break;
                case "3.2":
                    Console.WriteLine("\n№3.2: ");
                    Console.WriteLine("Введите число: ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + p.reverseListNums(a) + "\n");
                    break;
                case "3.4":
                    Console.WriteLine("\nЗадание 3.4: ");
                    Console.WriteLine("Введите первое число: ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("Введите второе число: ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out b))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + p.pow(a, b) + "\n");
                    break;
                case "3.6":
                    Console.WriteLine("\nЗадание 3.6: ");
                    Console.WriteLine("Введите число: ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + p.equalNum(a) + "\n");
                    break;
                case "3.8":
                    Console.WriteLine("\nЗадание 3.8: ");
                    Console.WriteLine("Введите высоту треугольника: ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            if (a > 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Высота не может быть отрицательной!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("\nРезультат: ");
                    p.leftTriangle(a);
                    break;
                case "3.10":
                    Console.WriteLine("\nЗадание 3.10: ");
                    p.guessGame();
                    break;
                case "4.2":
                    Console.WriteLine("\nЗадание 4.2: ");
                    Console.WriteLine("Введите числа массива через пробел:");
                    while (true)
                    {
                        var line = Console.ReadLine();
                        try
                        {
                            arr = line.Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод! Введите целые числа разделенные одним пробелом!");
                        }
                    }
                    Console.WriteLine("Введите число:");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + p.findLast(arr, a) + "\n");
                    break;
                case "4.4":
                    Console.WriteLine("\nЗадание 4.4: ");
                    Console.WriteLine("Введите числа массива через пробел:");
                    while (true)
                    {
                        var line = Console.ReadLine();
                        try
                        {
                            arr = line.Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод! Введите целые числа разделенные одним пробелом!");
                        }
                    }
                    Console.WriteLine("Введите число:");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out a))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("Введите позицию:");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out b))
                        {
                            if (b >= 0 && b <= arr.Length)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Нет такой позиций!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод! Введите целое число!");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + string.Join(" ", p.add(arr, a, b)) + "\n");
                    break;
                case "4.6":
                    Console.WriteLine("\nЗадание 4.6: ");
                    Console.WriteLine("Введите числа массива через пробел:");
                    while (true)
                    {
                        var line = Console.ReadLine();
                        try
                        {
                            arr = line.Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод! Введите целые числа разделенные одним пробелом!");
                        }
                    }
                    p.reverse(arr);
                    break;
                case "4.8":
                    Console.WriteLine("\nЗадание 4.8: ");
                    Console.WriteLine("Введите числа первого массива через пробел:");
                    while (true)
                    {
                        var line = Console.ReadLine();
                        try
                        {
                            arr = line.Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод! Введите целые числа разделенные одним пробелом!");
                        }
                    }
                    Console.WriteLine("Введите числа второго массива через пробел:");
                    while (true)
                    {
                        var line = Console.ReadLine();
                        try
                        {
                            arr1 = line.Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод! Введите целые числа разделенные одним пробелом!");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + string.Join(" ", p.concat(arr, arr1)) + "\n");
                    break;
                case "4.10":
                    Console.WriteLine("\nЗадание 4.10: ");
                    Console.WriteLine("Введите числа массива через пробел:");
                    while (true)
                    {
                        var line = Console.ReadLine();
                        try
                        {
                            arr = line.Split(' ').Select(z => Convert.ToInt32(z)).ToArray();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод! Введите целые числа разделенные одним пробелом!");
                        }
                    }
                    Console.WriteLine("\nРезультат: " + string.Join(" ", p.deleteNegative(arr)) + "\n");
                    break;
                default:
                    Console.WriteLine("Такого задания нет.");
                    break;
            }

        }


    }

}
