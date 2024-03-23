namespace Domain.DTO;

public class CancelDeliveryDTO
{
    public Guid DeliveryId { get; set; }
    public string UserAuth0 { get; set; }
}