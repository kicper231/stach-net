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
            .ThenInclude(dr => dr.Package)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.SourceAddress)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.DestinationAddress)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.User)
    .Include(d => d.Courier) 
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

    public async Task<List<Delivery>> GetAllDeliveriesAsync()
    {
        return await _context.Deliveries.Include(o=>o.Courier)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.Package)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.SourceAddress)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.DestinationAddress)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.User)
    .Include(d => d.Courier).ToListAsync();
    }

    public List<Delivery> GetDeliveriesWithOffersAndRequests(IEnumerable<int> deliveryRequestIds)
    {
        var deliveries = _context.Deliveries
            .Include(d => d.Offer)
                .ThenInclude(o => o.DeliveryRequest)
            .Where(d => deliveryRequestIds.Contains(d.Offer.DeliveryRequestId))
            .ToList();

        return deliveries;
    }

    public async Task<List<Delivery>> FindAcceptedDelivery()
    {
        var deliveries = await _context.Deliveries
            .Where(d => d.Courier == null && d.DeliveryStatus == DeliveryStatus.accepted).Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.Package)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.SourceAddress)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.DestinationAddress)
    .Include(d => d.Offer)
        .ThenInclude(o => o.DeliveryRequest)
            .ThenInclude(dr => dr.User)
    .Include(d => d.Courier)
            .ToListAsync();

        return deliveries;
    }


    public async Task<List<Delivery>> FindDeliveriesByCourierId(string courierId)
    {
        var deliveries = await _context.Deliveries
        .Where(d => d.Courier != null && d.Courier.Auth0Id == courierId)
        .Include(d => d.Offer)
            .ThenInclude(o => o.DeliveryRequest)
                .ThenInclude(dr => dr.Package)
        .Include(d => d.Offer)
            .ThenInclude(o => o.DeliveryRequest)
                .ThenInclude(dr => dr.SourceAddress)
        .Include(d => d.Offer)
            .ThenInclude(o => o.DeliveryRequest)
                .ThenInclude(dr => dr.DestinationAddress)
        .Include(d => d.Offer)
            .ThenInclude(o => o.DeliveryRequest)
                .ThenInclude(dr => dr.User)
        .Include(d => d.Courier)
        .ToListAsync();

        return deliveries;
    }

}