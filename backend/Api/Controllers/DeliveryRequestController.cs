using Api.Service;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> SendDeliveryRequest([FromBody] DeliveryRequestDTO DRDTO)
    {
        // Your existing code
        var response = await _deliveryRequestService.GetOffers(DRDTO);

        return Ok(response);  // This is now valid
    }


}
