using System;
using Menu.Interface;
using WebClient.Interfaces;

namespace Menu.Handlers;

public class GetCustomerHandler : IHandler
{
    private readonly IWebApiService _webApiService;

    public GetCustomerHandler(IWebApiService webApiService)
    {
        _webApiService = webApiService;
    }

    public void Run()
    {
        Console.Write("Введите id клиента: ");

        var input = Console.ReadLine() ?? string.Empty;
        var id = int.Parse(input);

        var customer = _webApiService.GetCustomerById(id).Result;

        Console.WriteLine();

        if (customer != null)
        {
            Console.WriteLine("Клиент найден: ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Id={customer.Id} | FirstName={customer.Firstname} | LastName={customer.Lastname}");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Клиент не найден!");
        }
    }
}