using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

    public interface IDeliveryRequestRepository
    {
        List<DeliveryRequest> GetDeliveryRequestsByUserId(string userId);
        public void Add(DeliveryRequest delivery);


    }

