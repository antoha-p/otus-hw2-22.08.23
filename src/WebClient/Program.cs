using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Menu;
using Menu.Handler;
using Menu.Handlers;
using Menu.Interface;
using WebClient.Services;

namespace WebClient;

static class Program
{
    static async Task Main(string[] args)
    {
        ShowTopInfo();
        Menu(Console.CursorTop + 1);
    }

    /// <summary>
    /// Меню
    /// </summary>
    /// <param name="cursorTopPosition"></param>
    private static void Menu(int cursorTopPosition)
    {
        IMenu menu = new MenuHandler(GetMenuItems(), cursorTopPosition, 0);

        var cursorTop = 0;

        while (true)
        {
            var selection = menu.RunMenu();

            // очищаем информативную часть консоли
            ClearBottomInfo(cursorTop + 1);

            try
            {
                menu.GetMenuItem(selection).Handler.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            // запоминаем текущее положение курсора для дальнейшей очистки консоли
            cursorTop = Console.CursorTop;
        }
    }

    /// <summary>
    /// Возвращает пункты меню
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<IMenuItem> GetMenuItems()
    {
        var item1 = new MenuItem
        (
            "Добавить случайного клиента",
            new CreateFakeCustomerHandler(new WebApiService())
        );

        var item2 = new MenuItem
        (
            "Найти клиента по id",
            new GetCustomerHandler(new WebApiService())
        );

        var item3 = new MenuItem
        (
            "Вывести всех клиентов",
            new GetAllCustomersHandler(new WebApiService())
        );

        var item6 = new MenuItem
        (
            "Выход",
            new ExitHandler()
        );

        IMenuItem[] menuItems = { item1, item2, item3, item6 };

        return menuItems;
    }

    /// <summary>
    /// Очищает нижнюю область с выводом информации
    /// </summary>
    private static void ClearBottomInfo(int cursorTop = 0)
    {
        var currentLineCursor = Console.CursorTop;

        for (var i = currentLineCursor; i < cursorTop + 1; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        Console.SetCursorPosition(0, currentLineCursor + 1);
    }

    /// <summary>
    /// Выводит основную информацию
    /// </summary>
    private static void ShowTopInfo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Используйте кнопки вверх и вниз для навигации по меню и Enter для выбора.\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("1. Создать эндпоинты в проекте WebApi (https://gitlab.com/otus-education/dotnetdev.homework.7).");
        Console.WriteLine("2. Доработать консольное приложение, чтобы оно удовлетворяло следующим требованиям:");
        Console.WriteLine("3. Принимает с консоли ID \"Клиента\", запрашивает его с сервера и отображает его данные по пользователю;");
        Console.WriteLine("4. Генерирует случайным образом данные для создания нового \"Клиента\" на сервере;");
        Console.WriteLine("5. Отправляет данные, созданные в пункте 2.2., на сервер;");
        Console.WriteLine("6. По полученному ID от сервера запросить созданного пользователя с сервера и вывести на экран.");
    }
}