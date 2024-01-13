using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.DTO;
using Api.Service;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{

    
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly IDeliveryRequest _deliveryRequestService;
        public CourierController(IDeliveryRequest deliveryRequestService)
        {
            _deliveryRequestService = deliveryRequestService;
        }
        // GET: api/<ValuesController>
        [HttpPost("inquiries")]
        public async Task<ActionResult<InquiryDTO>> SendDeliveryRequest([FromBody] DeliveryRequestDTO DRDTO)
        {
            // Your existing code
            var response =  _deliveryRequestService.GetOffers(DRDTO);

            return Ok(response);  // This is now valid
        }


        [HttpPost("offers")]
        public async Task<ActionResult<OfferRespondDTO>> GetOfferIdandAcceptOffer([FromBody] OfferDTO DRDTO)
        {
            
            var response = _deliveryRequestService.AcceptOffer(DRDTO);

            return Ok(response);  
        }




    }
}
