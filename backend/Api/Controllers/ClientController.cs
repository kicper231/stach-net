using Api.Service;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _deliveryservice;

    public ClientController(IClientService ClientService)
    {
        _deliveryservice = ClientService;
    }

    [HttpGet("get-my-inquiries/{idAuth0}")]
    public ActionResult<List<UserInquiryDTO>> GetMyDeliveryRequests(string idAuth0)
    {
        if (string.IsNullOrEmpty(idAuth0))
        {
            return BadRequest("Auth0 id użytkownika jest wymagane.");
        }

        if (!_deliveryservice.UserExists(idAuth0))
        {
            return NotFound("Nie ma takiego uzytkownika.");
        }

        var deliveryRequests = _deliveryservice.GetUserDeliveryRequests(idAuth0);
        return Ok(deliveryRequests);
    }

    [HttpPost("send-inquiry")]
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
        var response = await _deliveryservice.GetOffers(DRDTO);
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

    [HttpPost("accept-offer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferRespondDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
    public async Task<IActionResult> AcceptedOffer([FromBody] OfferDTO ODTO)
    {
        try
        {
            var respond = await _deliveryservice.acceptoffer(ODTO);
            return Ok(respond);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("add-delivery")]
    // [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddDeliveryRespondDTO))]
    public async Task<IActionResult> AddDeliveryToYourAccount(AddDeliveryDTO AddDeliveryDTO)
    {
        try
        {
            AddDeliveryRespondDTO respond = await _deliveryservice.AddDeliveryToAccount(AddDeliveryDTO);
            return Ok(respond);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}