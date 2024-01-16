using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AddDeliveryDTO
    {
        public required Guid DeliveryID { get; set; }
        public required string  UserAuth0 { get; set; }

    }

    public class AddDeliveryRespondDTO
    {

    }
}

