namespace Domain.Model;

public class CourierCompany : BaseEntity
{
    public int CourierCompanyId { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
}