using Domain.Abstractions;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;


public  class DeliveryRepository:IDeliveryRepository
{
    private readonly ShopperContext _context;

    public DeliveryRepository(ShopperContext context)
    {
        _context = context;
    }
    public Guid Add(Delivery delivery)
    {
        delivery.PublicID = Guid.NewGuid(); 
        _context.Deliveries.Add(delivery);
        _context.SaveChanges();

        return delivery.PublicID; 
    }

    public async Task<Delivery> FindAsync(Guid id)
    {
        var delivery = await _context.Deliveries
            .Where(d => d.PublicID == id)
            .Include(d => d.Offer) 
                .ThenInclude(o => o.DeliveryRequest) 
                    .ThenInclude(r => r.User) 
            .FirstOrDefaultAsync();

        if (delivery == null)
        {
            throw new KeyNotFoundException("Nie znaleziono takiego id w bazie");
        }

        return delivery;
    }

    public void Update(Delivery delivery)
    {
        _context.Deliveries.Update(delivery);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}