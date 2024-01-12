using Domain.Model;

namespace Infrastructure;


public class DbAddressRepository : IAddressRepository
{
    private readonly ShopperContext _context;

    public DbAddressRepository(ShopperContext context)
    {
        _context = context;
    }

    public List<Address> GetAll()
    {
        return _context.Addresses.ToList();
    }

    public Address GetById(int id)
    {
        return _context.Addresses.Find(id);
    }

    public void Add(Address address)
    {
        _context.Addresses.Add(address);
        _context.SaveChanges();
    }
}

