using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO;

   public class CancelDeliveryDTO
{
    public Guid DeliveryId { get; set; }
    public string UserAuth0 { get; set; }
}

