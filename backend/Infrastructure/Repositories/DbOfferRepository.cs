using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public interface IOfferRepository
{
    public void Add(Offer offer);
    public Offer? GetByInquiryId(Guid id);
    public Task<List<Offer>> GetAllByCompany(string CompanyName);
}

public class DbOfferRepository : IOfferRepository
{
    private readonly ShopperContext _context;

    public DbOfferRepository(ShopperContext context)
    {
        _context = context;
    }


    public Offer? GetByInquiryId(Guid id)
    {
        return _context.Offers.Include(o => o.DeliveryRequest)
                          .FirstOrDefault(a => a.InquiryId == id);
    }

    public void Add(Offer offer)
    {
        _context.Offers.Add(offer);
       
        _context.SaveChanges();
    }

    public async Task<List<Offer>> GetAllByCompany(string CompanyName)
    {
        return await _context.Offers.Where(a => a.CourierCompany.Name == CompanyName).ToListAsync();
    }
}