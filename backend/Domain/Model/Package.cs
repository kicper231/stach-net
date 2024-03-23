namespace Domain.Model;

public class Package : BaseEntity
{
    public int PackageId { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Weight { get; set; }

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