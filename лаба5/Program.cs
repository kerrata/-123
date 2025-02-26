using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace ЛБ5
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            string newFile;
            Console.WriteLine("Хотите создать новый файл для записи ваших действий? (y/n)");
            while (true)
            {
                newFile = Console.ReadLine().ToLower();
                if (newFile == "y" || newFile == "n") break;
                else Console.WriteLine("Введите y или n");
            }


            if (newFile == "y")
                File.WriteAllText("log.txt", "Action Log:\n");

            database.LoadFromExcel();

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Просмотр базы данных");
                Console.WriteLine("2. Удалить элемент");
                Console.WriteLine("3. Корректировать элемент");
                Console.WriteLine("4. Добавить элемент");
                Console.WriteLine("5. Выполнить запрос");
                Console.WriteLine("6. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите какую таблицу просмотреть: 1 - Исполнители, 2 - Услуги, 3 - Заказы:");
                        string readType = Console.ReadLine();
                        if (readType == "1" || readType == "2" || readType == "3")
                        {
                            database.ViewDatabase(readType);
                            database.LogAction("Выведена База данных");
                        }
                        else
                        {
                            Console.WriteLine("Нет такой таблицы.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Введите из какой таблицы будет удалён элемент: 1 - Исполнители, 2 - Услуги, 3 - Заказы:");
                        string deleteType = Console.ReadLine();
                        if (deleteType == "1" || deleteType == "2" || deleteType == "3")
                        {
                            database.DeleteElement(deleteType);
                        }
                        else
                        {
                            Console.WriteLine("Нет такой таблицы.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Введите из какой таблицы будет изменен элемент: 1 - Исполнители, 2 - Услуги, 3 - Заказы:");
                        string updateType = Console.ReadLine();
                        if (updateType == "1" || updateType == "2" || updateType == "3")
                        {
                            database.UpdateElement(updateType);
                        }
                        else
                        {
                            Console.WriteLine("Нет такой таблицы.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Введите в какую таблицу будет добавлен элемент: 1 - Исполнители, 2 - Услуги, 3 - Заказы:");
                        string addType = Console.ReadLine();
                        if (addType == "1" || addType == "2" || addType == "3")
                        {
                            database.AddElement(addType);
                        }
                        else
                        {
                            Console.WriteLine("Нет такой таблицы.");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Введите номер запроса(1-4):");
                        Console.WriteLine("1. Количество исполнителей старше 50 лет");
                        Console.WriteLine("2. Список услуг, стоимость которых превышает 1.9 млн руб.");
                        Console.WriteLine("3. Список разработчиков игр из Испании");
                        Console.WriteLine("4. Количество заказов, выполненных тестировщиками из индии, которые поучили меньше 1.5млн.");
                        var queryChoice = Console.ReadLine();
                        database.Query(queryChoice);
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Нет такой опции.");
                        break;
                }
            }
        }
    }

}
