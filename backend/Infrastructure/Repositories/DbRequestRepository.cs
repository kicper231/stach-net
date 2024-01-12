using Domain.Abstractions;
using Domain.Model;

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
            .ToList();
    }

    public void Add(DeliveryRequest delivery)
    {
        _context.DeliveryRequests.Add(delivery);

        _context.SaveChanges();
    }
}