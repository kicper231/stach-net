using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.DTO;
using Api.Service;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{

    [Route("inquiries")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly IInquiries _deliveryRequestService;
        public CourierController(IInquiries deliveryRequestService)
        {
            _deliveryRequestService = deliveryRequestService;
        }
        // GET: api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<InquiryDTO>> SendDeliveryRequest([FromBody] DeliveryRequestDTO DRDTO)
        {
            // Your existing code
            var response =  _deliveryRequestService.GetOffers(DRDTO);

            return Ok(response);  // This is now valid
        }




    }
}
