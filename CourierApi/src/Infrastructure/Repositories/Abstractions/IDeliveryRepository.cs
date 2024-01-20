using Domain.DTO;
using Domain.Model;
using Infrastructure;
using System;

namespace Domain.Abstractions;

public interface IDeliveryRepository
{

    public void SaveInDatabaseDelivery(OfferDTO DRDTO, OfferRespondDTO respond);

    public ShopperContext GetContext();
    public Task<Delivery> Find(Guid g);
    public void DatabaseSave();
}

//05637187-c18d-445d-84d1-3a22f01a29af