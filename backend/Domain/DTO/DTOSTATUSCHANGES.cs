using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;


public class ChangeDeliveryStatusDTO
{
    public Guid DeliveryId { get; set; }
    public string DeliveryStatus { get; set; }
    public string? Message { get; set; }
    public string? Auth0Id { get; set; }

}

public class ChangeDeliveryStatusWorkerDTO
{
    public Guid DeliveryId { get; set; }
    public string DeliveryStatus { get; set; }
    public string? Auth0Id { get; set; }
}