using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.DTO;
using Api.Service;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{

    
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly IDeliveryRequest _deliveryRequestService;
        private readonly ShopperContext _context;
        public CourierController(IDeliveryRequest deliveryRequestService,ShopperContext context)
        {
            _deliveryRequestService = deliveryRequestService;
            _context = context;
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

        [HttpGet("status/{OfferGuid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
        public async Task<IActionResult> GetStatus([FromBody] Guid OfferGuid)
        {
            try
            {
                // Poczekaj na POST z GUID
                // Wyszukaj GUID w bazie danych
                var deliveryRequest = await _context.Deliveries.FirstOrDefaultAsync(d => (d.DeliveryGuid == OfferGuid));
                if (deliveryRequest == null)
                {
                    return NotFound($"Nie znaleziono DeliveryRequest o GUID: {OfferGuid}");
                }
                return Ok(deliveryRequest.DeliveryStatus);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            };
        }


    }
}
