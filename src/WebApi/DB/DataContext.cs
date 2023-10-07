using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.DB;

public sealed class DataContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}