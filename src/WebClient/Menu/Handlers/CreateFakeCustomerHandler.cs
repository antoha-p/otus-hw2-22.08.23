using System;
using Bogus;
using Menu.Interface;
using WebClient;
using WebClient.Interfaces;

namespace Menu.Handlers;

public class CreateFakeCustomerHandler : IHandler
{
    private readonly IWebApiService _webApiService;

    public CreateFakeCustomerHandler(IWebApiService webApiService)
    {
        _webApiService = webApiService;
    }

    public void Run()
    {
        Console.WriteLine("Генерируем клиента...\n");

        Generate(out var firstName, out var lastName);

        var customer = _webApiService.AddCustomer(new Customer
        {
            Firstname = firstName,
            Lastname = lastName,
        }).Result;

        Console.WriteLine("Клиент добавлен: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Id={customer.Id} | FirstName={customer.Firstname} | LastName={customer.Lastname}");
        Console.ResetColor();
    }

    /// <summary>
    /// Генерирует случайные данные.
    /// </summary>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    private static void Generate(
        out string firstName,
        out string lastName)
    {
        var faker = new Faker();

        firstName = faker.Person.FirstName;
        lastName = faker.Person.LastName;
    }
}