using System.Security.Claims;
using Api.Service;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
//[Route()]
//[Authorize] 
public class InquiriesController : ControllerBase
{
    private readonly IDeliveryRequestService _Deliveryservice;

    public InquiriesController(IDeliveryRequestService deliveryRequestService)
    {
        _Deliveryservice = deliveryRequestService;
    }

    [HttpGet("getmyinquries")]
    [Authorize]
    public ActionResult<List<DeliveryRequest>> GetMyDeliveryRequests()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized("Brak identyfikatora użytkownika.");

        var deliveryRequests = _Deliveryservice.GetUserDeliveryRequests(userId);
        return Ok(deliveryRequests);
    }


    [HttpPost("sendinquiry")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InquiryRespondDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
    public async Task<ActionResult<InquiryRespondDTO>> SendDeliveryRequest([FromBody] InquiryDTO DRDTO)
    {
        //try if (DRDTO != null && DRDTO.Weight == 1000)
        //if (DRDTO != null && DRDTO.Package.Weight > 1000)
        //{
        //    ModelState.AddModelError("Weight", "Waga nie może wynosić dokładnie 1000.");
        //}

        //// Sprawdź, czy ModelState.IsValid jest false po dodaniu błędów
        //if (!ModelState.IsValid)
        //{
        //    var errors = ModelState.Keys
        //        .SelectMany(key => ModelState[key].Errors.Select(error => new ErrorResponse
        //        {
        //            PropertyName = key,
        //            ErrorMessage = error.ErrorMessage,
        //            Severity = "Error",
        //            ErrorCode = "ValidationError",
        //        }))
        //        .ToList();


        //    return BadRequest(errors);
        //}
        //{ // dla stacha
        var response = await _Deliveryservice.GetOffers(DRDTO);
        if (response != null)
            return Ok(response); // Sukces
        return NotFound("Nie znaleziono ofert.");
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


    [HttpPost("acceptoffer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferRespondDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
    public async Task<IActionResult> AcceptedOffer([FromBody] OfferDTO ODTO )
    {

        try
        {
            var respond = await _Deliveryservice.acceptoffer(ODTO);
            return Ok(respond);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        };
    }

}