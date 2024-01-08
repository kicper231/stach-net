

namespace Domain.Model;


public class Package : BaseEntity
{
    public int PackageId { get; set; }
    public string Dimensions { get; set; }
    public double Weight { get; set; }
    public PackagePriority Priority { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public bool WeekendDelivery { get; set; }

    //public ICollection<DeliveryRequest> DeliveryRequests { get; set; }
}

public enum PackagePriority
{
    Low,
    High
}
public enum WeightUnit
{
    Kilograms,
    Pounds
}


