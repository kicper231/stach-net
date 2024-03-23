namespace Domain.DTO;

public class InquiryRespondDTO
{
    public string CompanyName { get; set; }
    public string Currency { get; set; }
    public double totalPrice { get; set; }

    public DateTime expiringAt { get; set; }
    public string InquiryId { get; set; }

    public List<PriceBreakdown> PriceBreakDown { get; set; }
    //public InquiryRespondDTO(string text, int a, DateTime date)
    //{
    //    CompanyName = text;
    //    Cost = a;
    //    DeliveryDate = date;
    //}
}

public class ErrorResponse
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
    public string Severity { get; set; }
    public string ErrorCode { get; set; }
}