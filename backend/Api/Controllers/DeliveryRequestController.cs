using Api.Service;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Api.Controllers;


[ApiController]
[Route("api/requestdelivery")]
//[Authorize] 
public class DeliveryRequestController : ControllerBase
{
    private readonly IDeliveryRequestService _deliveryRequestService;

    public DeliveryRequestController(IDeliveryRequestService deliveryRequestService)
    {
        _deliveryRequestService = deliveryRequestService;
    }

    [HttpGet("getmyinquries")]
    [Authorize]
    public ActionResult<List<DeliveryRequest>> GetMyDeliveryRequests()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("Brak identyfikatora użytkownika.");
        }

        var deliveryRequests = _deliveryRequestService.GetUserDeliveryRequests(userId);
        return Ok(deliveryRequests);
    }




    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeliveryRespondDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public async Task<ActionResult<DeliveryRespondDTO>> SendDeliveryRequest([FromBody] DeliveryRequestDTO DRDTO)
    {

        //try
        //{
            var response = await _deliveryRequestService.GetOffers(DRDTO);
            if (response != null)
            {
                return Ok(response); // Sukces
            }
            else
            {
               
                return NotFound("Nie znaleziono ofert.");
            }
        //}
        //catch (HttpRequestException ex)
        //{
        //    if (ex.StatusCode == HttpStatusCode.BadRequest)
        //    {
        //        return BadRequest("Błąd żądania: " + ex.Message);
        //    }
        //    // Obsługa innych wyjątków
        //    return StatusCode(404, $"{ex.Message}");
        //}
    }


}
