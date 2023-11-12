using Domain.Abstractions;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbCustomerRepository : ICustomerRepository
{
    private readonly ShopperContext context;

    public DbCustomerRepository(ShopperContext context)
    {
        this.context = context;
    }

    public List<Customer> GetAll()
    {
        return context.Customers.ToList();
    }

    public Customer GetById(int id)
    {
        return context.Customers.Find(id);
    }

    public void Add(Customer customer)
    {
        context.Customers.Add(customer);
        context.SaveChanges();
    }
}

// dotnet add package Microsoft.EntityFrameworkCore
public class ShopperContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public ShopperContext(DbContextOptions options)
    : base(options)
    {
    }
}