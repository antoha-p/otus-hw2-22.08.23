using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebClient.Interfaces;

namespace WebClient.Services;

public class WebApiService : IWebApiService
{
    private const string CustomersUri = "/customers/";

    private readonly HttpClient _httpClient;

    public WebApiService(string host = null)
    {
        host ??= ConfigurationManager.AppSettings["WebApiHost"] ?? "";

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(host);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        using var response = await _httpClient.GetAsync(CustomersUri);
        return await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
    }

    public async Task<Customer> GetCustomerById(long id)
    {
        using var response = await _httpClient.GetAsync(CustomersUri + $"{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        return null;
    }

    public async Task<Customer> AddCustomer(object customerRequest)
    {
        var requestJson = JsonSerializer.Serialize(customerRequest);
        var content = new StringContent(
            requestJson,
            Encoding.UTF8,
            "application/json");

        using var response = await _httpClient.PostAsync(CustomersUri, content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var customer = JsonSerializer.Deserialize<Customer>(responseString);
            return customer;
        }

        return null;
    }
}
