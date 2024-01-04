using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;

public class DeliveryRespondDTO
{
    public string CompanyName { get; set; }

    public int Cost { get; set; }
    public DateTime DeliveryDate { get; set; }
    //public DeliveryRespondDTO(string text, int a, DateTime date)
    //{
    //    CompanyName = text;
    //    Cost = a;
    //    DeliveryDate = date;
    //}
}
