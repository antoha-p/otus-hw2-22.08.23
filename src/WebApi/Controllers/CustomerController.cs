using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("customers")]
public class CustomerController : Controller
{
    private readonly IRepository<Customer> _customerRepository;

    public CustomerController(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _customerRepository.GetAllAsync());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var customer = await _customerRepository.GetAsync(id);
        return customer != null ? Ok(customer) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Customer customer)
    {
        var existCustomer = await _customerRepository.GetAsync(customer.Id);
        return existCustomer != null ? Conflict("Customer is exists") : Ok(await _customerRepository.AddAsync(customer));
    }
}