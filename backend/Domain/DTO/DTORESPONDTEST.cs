using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;

public class DeliveryRespondDTO
{
    public string CompanyName { get; set; }

    public decimal Cost { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string InquiryId { get; set; }

    public List<PriceBreakdown> PriceBreakDown { get; set; }
    //public DeliveryRespondDTO(string text, int a, DateTime date)
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