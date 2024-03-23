using Domain.Abstractions;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbRequestRepository : IDeliveryRequestRepository
{
    private readonly ShopperContext _context;

    public DbRequestRepository(ShopperContext context)
    {
        _context = context;
    }

    public List<DeliveryRequest> GetDeliveryRequestsByUserId(string userId)
    {
        return _context.DeliveryRequests
            .Where(dr => dr.UserAuth0 == userId)

        .Include(dr => dr.Package)
        .Include(dr => dr.SourceAddress)
        .Include(dr => dr.DestinationAddress)
            .ToList();
    }

    public void Add(DeliveryRequest delivery)
    {
        _context.DeliveryRequests.Add(delivery);

        _context.SaveChanges();
    }

    public async Task<List<DeliveryRequest>> GetAllInquiriesAsync()
    {
        return await _context.DeliveryRequests
            .Include(dr => dr.Package)
            .Include(dr => dr.SourceAddress)
            .Include(dr => dr.DestinationAddress)
            .ToListAsync();
    }
}