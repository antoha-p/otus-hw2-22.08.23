using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebClient.Interfaces;

public interface IWebApiService
{
    public Task<IEnumerable<Customer>> GetAllCustomers();

    public Task<Customer> GetCustomerById(long id);

    public Task<Customer> AddCustomer(object customerRequest);
}