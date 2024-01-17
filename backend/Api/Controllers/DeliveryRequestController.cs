using System;
using System.Security.Claims;
using Api.Service;
using Domain.DTO;
using Domain.Model;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
//[Route()]
//[Authorize] 
public class InquiriesController : ControllerBase
{
    private readonly IDeliveryRequestService _Deliveryservice;
    private readonly ShopperContext _context;

    public InquiriesController(IDeliveryRequestService deliveryRequestService,ShopperContext context)
    {
        _Deliveryservice = deliveryRequestService;
        _context = context;
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
        if (!ModelState.IsValid || DRDTO == null)
        {
            return BadRequest("Nieprawidłowe dane wejściowe"); // Bad Request, ponieważ model nie spełnia warunków
        }


        if (DRDTO.Package.Weight >= 1000)
        {
            return BadRequest("Zbyt duża waga paczki");
        }
        if (DRDTO.Package.Height > 1000 || DRDTO.Package.Length > 1000 || DRDTO.Package.Width > 1000)
        {
            return BadRequest("Zbyt duże wymiary paczki");
        }

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
    public async Task<IActionResult> AcceptedOffer([FromBody] OfferDTO ODTO)
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

    [HttpPost("status")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
    public async Task<IActionResult> GetStatus([FromBody] Guid ODTO)
    {
        try
        {
            // Poczekaj na POST z GUID
            // Wyszukaj GUID w bazie danych
            var deliveryRequest = await _context.Deliveries.FirstOrDefaultAsync(d =>( d.DeliveryGuid == ODTO));
            if (deliveryRequest == null)
            {
                return NotFound($"Nie znaleziono DeliveryRequest o GUID: {ODTO}");
            }
            return Ok(deliveryRequest.DeliveryStatus);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        };
    }

}