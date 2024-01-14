using Domain.Model;

namespace Infrastructure;

public interface ICourierCompanyRepository
{
    List<CourierCompany> GetAll();
    CourierCompany GetByName(string name);
    void Add(CourierCompany courierCompany);
    void SaveChanges();
}

public class DbCourierCompanyRepository : ICourierCompanyRepository
{
    private readonly ShopperContext _context;

    public DbCourierCompanyRepository(ShopperContext context)
    {
        _context = context;
    }

    public List<CourierCompany> GetAll()
    {
        return _context.CourierCompanies.ToList();
    }

    public CourierCompany GetByName(string name)
    {
        return _context.CourierCompanies.FirstOrDefault(cc => cc.Name == name);
    }

    public void Add(CourierCompany courierCompany)
    {
        _context.CourierCompanies.Add(courierCompany);
        _context.SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}