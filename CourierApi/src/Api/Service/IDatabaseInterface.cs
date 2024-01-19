using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Service;

public interface IDatabaseInterface
{
    public string Validate(DeliveryRequestDTO DRDTO);
    public void SaveInDatabaseDelivery(OfferDTO DRDTO, OfferRespondDTO respond);
    public void SaveInDatabaseDeliveryRequest(DeliveryRequestDTO DRDTO, InquiryDTO response);


}
