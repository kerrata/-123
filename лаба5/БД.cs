using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Cells;
using ЛБ5;
using Workbook = Aspose.Cells.Workbook;
using Worksheet = Aspose.Cells.Worksheet;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;


public class Database
{
    private List<Freelancer> freelancers;
    private List<Service> services;
    private List<Order> orders;

    public Database()
    {
        freelancers = new List<Freelancer>();
        services = new List<Service>();
        orders = new List<Order>();
    }

    // Чтение базы данных из excel файла
    public void LoadFromExcel()
    {
        Workbook wb;
        try
        {
            wb = new Workbook("LR5-var6.xls");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Файл не найден: {ex.Message}");
            return;
        }

        WorksheetCollection col = wb.Worksheets;

        // Чтение таблицы "Исполнители"
        Worksheet ws1 = col[2];
        for (int i = 1; i <= ws1.Cells.MaxDataRow; i++) // Пропускаем заголовок
        {
            if (int.TryParse(ws1.Cells[i, 0].StringValue, out int id) &&
                int.TryParse(ws1.Cells[i, 1].StringValue, out int age) &&
                !string.IsNullOrWhiteSpace(ws1.Cells[i, 2].StringValue))
            {
                freelancers.Add(new Freelancer(id, age, ws1.Cells[i, 2].StringValue));
            }
            else
            {
                Console.WriteLine($"Ошибка преобразования данных о фрилансере в строке {i}: " +
                                  $"ID='{ws1.Cells[i, 0].StringValue}', Возраст='{ws1.Cells[i, 1].StringValue}', Гражданство='{ws1.Cells[i, 2].StringValue}'");
            }
        }

        // Чтение таблицы "Услуги"
        Worksheet ws2 = col[1];
        for (int i = 1; i <= ws2.Cells.MaxDataRow; i++) // Пропускаем заголовок
        {
            if (int.TryParse(ws2.Cells[i, 0].StringValue, out int serviceId) &&
                !string.IsNullOrWhiteSpace(ws2.Cells[i, 1].StringValue))
            {
                services.Add(new Service(serviceId, ws2.Cells[i, 1].StringValue));
            }
            else
            {
                Console.WriteLine($"Ошибка преобразования данных об услуге в строке {i}: " +
                                  $"ID услуги='{ws2.Cells[i, 0].StringValue}', Название='{ws2.Cells[i, 1].StringValue}'");
            }
        }

        // Чтение таблицы "Заказы"
        Worksheet ws3 = col[0];
        for (int i = 1; i <= ws3.Cells.MaxDataRow; i++) // Пропускаем заголовок
        {
            if (int.TryParse(ws3.Cells[i, 0].StringValue, out int code) &&
            int.TryParse(ws3.Cells[i, 1].StringValue, out int serviceCode) &&
            int.TryParse(ws3.Cells[i, 2].StringValue, out int executorCode) &&
            decimal.TryParse(ws3.Cells[i, 3].StringValue.Replace("р.", "").Replace(" ", "").Trim(), out decimal cost))
            {
                // Если все преобразования успешны, добавляем заказ
                orders.Add(new Order(code, serviceCode, executorCode, cost));
            }
            else
            {
                // Обработка ошибок
                Console.WriteLine($"Ошибка преобразования данных о заказе в строке {i}: " +
                                  $"Код='{ws3.Cells[i, 0].StringValue}', Код услуги='{ws3.Cells[i, 1].StringValue}', Код исполнителя='{ws3.Cells[i, 2].StringValue}', Стоимость='{ws3.Cells[i, 3].StringValue}'");
            }
        }
    }

    // Вывод базы данных
    public void ViewDatabase(string type)
    {
        switch (type.ToLower())
        {
            case "1":
                Console.WriteLine("Исполнители:");
                foreach (var freelancer in freelancers)
                    Console.WriteLine(freelancer);
                break;
            case "2":
                Console.WriteLine("Услуги:");
                foreach (var service in services)
                    Console.WriteLine(service);
                break;
            case "3":
                Console.WriteLine("Заказы:");
                foreach (var order in orders)
                    Console.WriteLine(order);
                break;
        }
    }

    // Удаление элемента из базы данных
    public void DeleteElement(string type)
    {
        var workbook = new Workbook("LR5-var6.xls");
        Console.WriteLine("Введите какой код(ID) вы хотите удалить:");
        switch (type.ToLower())
        {
            // Исполнители
            case "1":
                int key = Input();
                var freelancer = freelancers.FirstOrDefault(a => a.FreelancerId == key);
                if (freelancer != null)
                {
                    freelancers.RemoveAll(f => f.FreelancerId == key);
                    var worksheet = workbook.Worksheets[2];

                    for (int i = 1; i <= worksheet.Cells.MaxDataRow; i++)
                    {
                        if (Convert.ToInt32(worksheet.Cells[i, 0].Value) == key)
                        {
                            worksheet.Cells.DeleteRow(i);
                            break;
                        }
                    }
                    workbook.Save("LR5-var6.xls");
                    Console.WriteLine($"Исполнитель с кодом {key} удален");
                    LogAction($"Удален элемент из таблицы 'Исполнители' с кодом {key}");
                }
                else
                {
                    Console.WriteLine($"Исполнитель с кодом {key} не найден.");
                }
                break;
            // Услуги
            case "2":
                key = Input();
                var service = services.FirstOrDefault(a => a.ServiceId == key);
                if (service != null)
                {
                    services.RemoveAll(s => s.ServiceId == key);
                    var worksheet = workbook.Worksheets[1];

                    for (int i = 1; i <= worksheet.Cells.MaxDataRow; i++)
                    {
                        if (Convert.ToInt32(worksheet.Cells[i, 0].Value) == key)
                        {
                            worksheet.Cells.DeleteRow(i);
                            break;
                        }
                    }
                    workbook.Save("LR5-var6.xls");
                    Console.WriteLine($"Услуга с кодом {key} удалена");
                    LogAction($"Удален элемент из таблицы 'Услуги' с кодом {key}");
                }
                else
                {
                    Console.WriteLine($"Услуга с кодом {key} не найден.");
                }
                break;
            // Заказы
            case "3":
                key = Input();
                var order = orders.FirstOrDefault(a => a.OrderId == key);
                if (order != null)
                {
                    orders.RemoveAll(o => o.OrderId == key);
                    var worksheet = workbook.Worksheets[0];

                    for (int i = 1; i <= worksheet.Cells.MaxDataRow; i++)
                    {
                        if (Convert.ToInt32(worksheet.Cells[i, 0].Value) == key)
                        {
                            worksheet.Cells.DeleteRow(i);
                            break;
                        }
                    }
                    workbook.Save("LR5-var6.xls");
                    Console.WriteLine($"Заказ с кодом {key} удален");
                    LogAction($"Удален элемент из таблицы 'Заказы' с кодом {key}");
                }
                else
                {
                    Console.WriteLine($"Заказ с кодом {key} не найден.");
                }
                break;
        }
    }

    // Корректировка элемента
    public void UpdateElement(string type)
    {
        var workbook = new Workbook("LR5-var6.xls");
        Console.WriteLine("Введите какой код(ID) вы хотите изменить:");
        switch (type)
        {
            // Исполнители
            case "1":
                int key = Input();
                var freelancer = freelancers.FirstOrDefault(a => a.FreelancerId == key);
                if (freelancer != null)
                {
                    Console.WriteLine("Введите возраст:");
                    int newAge = Input();
                    Console.WriteLine("Введите гражданство:");
                    string newCitizenship = Console.ReadLine();

                    freelancer.Age = newAge;
                    freelancer.Citizenship = newCitizenship;

                    var worksheet = workbook.Worksheets[2];
                    for (int i = 1; i <= worksheet.Cells.MaxDataRow; i++)
                    {
                        if (Convert.ToInt32(worksheet.Cells[i, 0].Value) == key)
                        {
                            worksheet.Cells[i, 0].Value = key;
                            worksheet.Cells[i, 1].Value = newAge;
                            worksheet.Cells[i, 2].Value = newCitizenship;
                            break;
                        }
                    }

                    workbook.Save("LR5-var6.xls");

                    LogAction($"Изменен элемент в таблице 'Исполнители' с кодом {key}. Новые данные: {newAge}, {newCitizenship}");
                }
                else
                {
                    Console.WriteLine($"Исполнитель с кодом {key} не найден.");
                }
                break;

            // Услуги
            case "2":
                key = Input();
                var service = services.FirstOrDefault(a => a.ServiceId == key);
                if (service != null)
                {
                    Console.WriteLine("Введите название:");
                    string newName = Console.ReadLine();

                    service.Name = newName;

                    var worksheet = workbook.Worksheets[1];
                    int lastRow = worksheet.Cells.MaxDataRow + 1;
                    worksheet.Cells[lastRow, 0].Value = key;
                    worksheet.Cells[lastRow, 1].Value = newName;
                    workbook.Save("LR5-var6.xls");

                    LogAction($"Изменен элемент в таблице 'Услуги' с кодом {key}. Новые данные: {newName}");
                }
                else
                {
                    Console.WriteLine($"Исполнитель с кодом {key} не найден.");
                }
                break;

            // Заказы
            case "3":
                key = Input();
                var order = orders.FirstOrDefault(a => a.OrderId == key);
                if (order != null)
                {
                    Console.WriteLine("Введите стоимость:");
                    decimal newCost;
                    while (true)
                    {
                        if (decimal.TryParse(Console.ReadLine(), out newCost))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Введите число");
                        }
                    }

                    Console.WriteLine("Введите код услуги:");
                    int newService = Input();
                    service = services.FirstOrDefault(a => a.ServiceId == newService);
                    if (service != null)
                    {
                        Console.WriteLine("Введите код исполнителя:");
                        int newFreelancer = Input();
                        freelancer = freelancers.FirstOrDefault(a => a.FreelancerId == newFreelancer);
                        if (freelancer != null)
                        {
                            order.ServiceId = newService;
                            order.FreelancerId = newFreelancer;
                            order.Cost = newCost;

                            var worksheet = workbook.Worksheets[0];
                            int lastRow = worksheet.Cells.MaxDataRow + 1;
                            worksheet.Cells[lastRow, 0].Value = key;
                            worksheet.Cells[lastRow, 1].Value = newService;
                            worksheet.Cells[lastRow, 2].Value = newFreelancer;
                            worksheet.Cells[lastRow, 3].Value = newCost;
                            workbook.Save("LR5-var6.xls");
                            LogAction($"Изменен элемент в таблице 'Услуги' с кодом {key}. Новые данные: {newService}, {newFreelancer}, {newCost}");
                        }
                        else
                        {
                            Console.WriteLine("Исполнителя с таким кодом нет");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Услуги с таким кодом нет");
                    }

                }
                else
                {
                    Console.WriteLine($"Исполнитель с кодом {key} не найден.");
                }
                break;
        }
    }

    // Добавление элемента
    public void AddElement(string type)
    {
        var workbook = new Workbook("LR5-var6.xls");
        Console.WriteLine("Введите какой код(ID) вы хотите добавить:");
        switch (type.ToLower())
        {
            // Исполнители
            case "1":
                int key = Input();
                var freelancer = freelancers.FirstOrDefault(a => a.FreelancerId == key);
                if (freelancer == null)
                {
                    Console.WriteLine("Введите возраст:");
                    int age = Input();
                    Console.WriteLine("Введите гражданство:");
                    string citizenship = Console.ReadLine();
                    var newFreelancer = new Freelancer(key, age, citizenship);
                    freelancers.Add(newFreelancer);

                    var worksheet = workbook.Worksheets[2];
                    int lastRow = worksheet.Cells.MaxDataRow + 1;
                    worksheet.Cells[lastRow, 0].Value = newFreelancer.FreelancerId;
                    worksheet.Cells[lastRow, 1].Value = newFreelancer.Age;
                    worksheet.Cells[lastRow, 2].Value = newFreelancer.Citizenship;

                    workbook.Save("LR5-var6.xls");
                    LogAction($"Добавлен элемент в таблицу 'Исполнители' с кодом {key}. Новые данные: {age}, {citizenship}");

                }
                else
                {
                    Console.WriteLine($"Исполнитель с кодом {key} уже есть.");
                }
                break;

            // Услуги
            case "2":
                key = Input();
                var service = services.FirstOrDefault(a => a.ServiceId == key);
                if (service == null)
                {
                    Console.WriteLine("Введите название:");
                    string name = Console.ReadLine();
                    var newService = new Service(key, name);
                    services.Add(newService);

                    var worksheet = workbook.Worksheets[1];
                    int lastRow = worksheet.Cells.MaxDataRow + 1;
                    worksheet.Cells[lastRow, 0].Value = newService.ServiceId;
                    worksheet.Cells[lastRow, 1].Value = newService.Name;

                    workbook.Save("LR5-var6.xls");
                    LogAction($"Добавлен элемент в таблицу 'Услуги' с кодом {key}. Новые данные: {name}");

                }
                else
                {
                    Console.WriteLine($"Услуга с кодом {key} уже есть.");
                }
                break;

            // Заказы
            case "3":
                key = Input();
                var order = orders.FirstOrDefault(a => a.OrderId == key);
                if (order == null)
                {
                    Console.WriteLine("Введите код услуги:");
                    int serviceId = Input();
                    service = services.FirstOrDefault(a => a.ServiceId == serviceId);
                    if (service != null)
                    {
                        Console.WriteLine("Введите код исполнителя:");
                        int freelancerId = Input();
                        freelancer = freelancers.FirstOrDefault(a => a.FreelancerId == freelancerId);
                        if (freelancer != null)
                        {
                            Console.WriteLine("Введите стоимость:");
                            decimal newCost = decimal.Parse(Console.ReadLine());
                            var newOrder = new Order(key, serviceId, freelancerId, newCost);

                            var worksheet = workbook.Worksheets[0];
                            int lastRow = worksheet.Cells.MaxDataRow + 1;
                            worksheet.Cells[lastRow, 0].Value = newOrder.OrderId;
                            worksheet.Cells[lastRow, 1].Value = newOrder.ServiceId;
                            worksheet.Cells[lastRow, 2].Value = newOrder.FreelancerId;
                            worksheet.Cells[lastRow, 3].Value = newOrder.Cost;
                            orders.Add(newOrder);
                            workbook.Save("LR5-var6.xls");
                            LogAction($"Добавлен элемент в таблицу 'Заказы' с кодом {key}. Новые данные: {serviceId}, {freelancerId}, {newCost}");
                        }
                        else
                        {
                            Console.WriteLine("Исполнителя с таким кодом нет");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Услуги с таким кодом нет");
                    }

                }
                else
                {
                    Console.WriteLine($"Заказ с кодом {key} уже есть.");
                }
                break;
        }
    }

    static int Input()
    {
        int num;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out num))
            {
                break;
            }
            else
            {
                Console.WriteLine("Введите число");
            }
        }
        return num;
    }

    public void Query(string i)
    {
        switch (i)
        {
            // Количество исполнителей старше 50 лет
            case "1":
                int countOver50 = freelancers.Count(f => f.Age > 50);
                Console.WriteLine($"Количество исполнителей старше 50 лет: {countOver50}");
                LogAction("Выполнен запрос 1");
                break;

            // Услуги стоимость которых превышает 1.9 млн руб 
            case "2":
                var expensiveServices = from service in services
                                        join order in orders on service.ServiceId equals order.ServiceId // Соединяем списки services и orders по полю ServiceId
                                        where order.Cost > 1900000 // Используем условие, чтобы отфильтровать только те заказы, стоимость которых превышает 1.5 миллиона рублей
                                        select new // Создаем объект, содержащий идентификатор услуги, название услуги и стоимость заказа
                                        {
                                            ServiceId = service.ServiceId,
                                            ServiceName = service.Name,
                                            OrderCost = order.Cost
                                        };

                foreach (var item in expensiveServices)
                {
                    Console.WriteLine($"Услуга: {item.ServiceName}, Стоимость: {item.OrderCost}");
                }
                LogAction("Выполнен запрос 2");
                break;
            // Разработчики игр из испании
            case "3":
                var result = from order in orders
                             join freelancer in freelancers on order.FreelancerId equals freelancer.FreelancerId // Соединяем заказы с исполнителями и услугами по соответствующим идентификаторам (FreelancerId и ServiceId)
                             join service in services on order.ServiceId equals service.ServiceId
                             where freelancer.Citizenship == "Испания" && service.Name == "Разработчик игр" // Фильтруем результаты так, чтобы оставить только тех исполнителей, у которых гражданство "Индия", и услуги с названием "Разработка игр"
                             select order;

                foreach (var order in result)
                {
                    Console.WriteLine($"Код заказа: {order.OrderId}, Стоимость: {order.Cost}");
                }
                LogAction("Выполнен запрос 3");
                break;
            // Кол-во заказов выполненных тестировщиками из индии, которые поучили меньше 1.5млн
            case "4":
                var count = (from order in orders
                             join freelancer in freelancers on order.FreelancerId equals freelancer.FreelancerId // Cоединяем таблицы заказов с исполнителями и услугами по соответствующим идентификаторам
                             join service in services on order.ServiceId equals service.ServiceId
                             where freelancer.Citizenship == "Индия" // Фильтруем результаты так
                                   && service.Name == "Тестировщик ПО"
                                   && order.Cost < 1500000
                             select freelancer).Distinct().Count(); // Cчитаем количество уникальных

                Console.WriteLine($"Количество исполнителей: {count}");
                LogAction("Выполнен запрос 4");
                break;

        }

    }

    public void LogAction(string action)
    {
        string logFile = "log.txt";

        using (StreamWriter writer = new StreamWriter(logFile, true))
        {
            writer.WriteLine($"{DateTime.Now}: {action}");
        }
    }
}
