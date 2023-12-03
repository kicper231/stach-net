using Domain.Abstractions;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

// Klasa DbCustomerRepository implementuje interfejs ICustomerRepository
public class DbCustomerRepository : ICustomerRepository
{
    // Prywatne pole przechowuj¹ce kontekst bazy danych
    private readonly ShopperContext context;

    // Konstruktor inicjuj¹cy kontekst bazy danych
    public DbCustomerRepository(ShopperContext context)
    {
        this.context = context;
    }

    // Metoda zwracaj¹ca wszystkich klientów z bazy danych
    public List<Customer> GetAll()
    {
        // Zwraca wszystkich klientów jako listê
        return context.Customers.ToList();
    }

    // Metoda zwracaj¹ca klienta na podstawie jego ID
    public Customer GetById(int id)
    {
        // Znajduje klienta w bazie danych na podstawie ID
        return context.Customers.Find(id);
    }

    // Metoda dodaj¹ca nowego klienta do bazy danych
    public void Add(Customer customer)
    {
        // Dodaje klienta do zbioru Customers w kontekœcie bazy danych
        context.Customers.Add(customer);
        // Zapisuje zmiany w bazie danych
        context.SaveChanges();
    }
}

// Klasa kontekstu bazy danych, rozszerzaj¹ca DbContext z Entity Framework
public class ShopperContext : DbContext
{
    // Zbiory reprezentuj¹ce tabele w bazie danych
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    // Konstruktor przyjmuj¹cy opcje konfiguracyjne DbContext
    public ShopperContext(DbContextOptions options)
    : base(options)
    {
    }
}
