using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.DTO;
using Api.Service;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.VisualBasic;
using Azure;
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
        [HttpPost("inquiries")]
        public async Task<ActionResult<InquiryDTO>> SendDeliveryRequest([FromBody] DeliveryRequestDTO DRDTO)
        {

            string Error = _deliveryRequestService.Validate(DRDTO);
            if(!ModelState.IsValid)
            {
                Error = "Nieprawidłowe dane wejściowe";
            }
     

            if (Error != "")
            {
                return BadRequest(Error);
            }
            var response =  _deliveryRequestService.GetOffers(DRDTO);
            _deliveryRequestService.SaveInDatabaseDeliveryRequest(DRDTO, response);
          

            return Ok(response);  // This is now valid
        }


        [HttpPost("offers")]
        public async Task<ActionResult<OfferRespondDTO>> GetOfferIdandAcceptOffer([FromBody] OfferDTO DRDTO)
        {
            
            var response = _deliveryRequestService.AcceptOffer(DRDTO);
            _deliveryRequestService.SaveInDatabaseDelivery(DRDTO,response);
            return Ok(response);  
        }

        [HttpGet("status/{OfferGuid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
        public async Task<IActionResult> GetStatus(Guid OfferGuid)
        {
            try
            {
                
                var deliveryRequest =  await _deliveryRequestService.Find(OfferGuid);

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

        [HttpPost("status/ChangeStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuidInt))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
        public async Task<IActionResult> SetStatus([FromBody] GuidInt OfferGuid)
        {
            
            try
            {
                
                var delivery = await _deliveryRequestService.Find(OfferGuid.g);
                if (delivery == null)
                {
                    return NotFound($"Nie znaleziono DeliveryRequest o GUID: {OfferGuid.g}");
                }
                else
                {
                    delivery.DeliveryStatus  = (DeliveryStatus)Enum.ToObject(typeof(DeliveryStatus), OfferGuid.i);
                    _deliveryRequestService.DatabaseSave();
                    return Ok("Status updated successfully");
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            };

        }


      
    }
}
