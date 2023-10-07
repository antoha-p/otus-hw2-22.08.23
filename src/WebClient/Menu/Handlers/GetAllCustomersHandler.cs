using System;
using System.Linq;
using Menu.Interface;
using WebClient.Interfaces;

namespace Menu.Handlers;

public class GetAllCustomersHandler : IHandler
{
    private readonly IWebApiService _webApiService;

    public GetAllCustomersHandler(IWebApiService webApiService)
    {
        _webApiService = webApiService;
    }

    public void Run()
    {
        Console.WriteLine("Поиск клиентов...\n");

        var customers = _webApiService.GetAllCustomers().Result;

        if (customers?.Any() == false)
        {
            Console.WriteLine("База клиентов пуста.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        foreach (var customer in customers)
        {
            Console.WriteLine($"Id={customer.Id} | FirstName={customer.Firstname} | LastName={customer.Lastname}");
        }
        Console.ResetColor();
    }
}