using Domain.Abstractions;
using Domain.DTO;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infrastructure.Repositories;


public  class DeliveryRepository:IDeliveryRepository
{
    private readonly ShopperContext _context;

    public DeliveryRepository(ShopperContext context)
    {
        _context = context;
    }

    public void SaveInDatabaseDelivery(OfferDTO DRDTO, OfferRespondDTO respond)
    {
        var DeliveryDB = new Delivery
        {
            DeliveryGuid = respond.OfferRequestId,
            DeliveryStatus = DeliveryStatus.AcceptedByWorker
        };
        _context.Deliveries.Add(DeliveryDB);
        _context.SaveChanges();
    }
    public ShopperContext GetContext()
    {
        return _context;
    }
}