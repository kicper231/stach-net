using System.Linq.Expressions;
using System.Net;
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
public class OfficeWorkerController : ControllerBase
{
    private readonly IDeliveryRequestService _deliveryservice;

    public OfficeWorkerController(IDeliveryRequestService deliveryRequestService)
    {
        _deliveryservice = deliveryRequestService;
    }

    //[HttpGet("get-company-orders")]
    //// [Authorize("client:permission")]
    //public ActionResult<List<UserInquiryDTO>> GetMyDeliveryRequests(string idAuth0)
    //{
    //    if (string.IsNullOrEmpty(idAuth0))
    //    {
    //        return BadRequest("Auth0 id użytkownika jest wymagane.");
    //    }


    //    if (!_deliveryservice.UserExists(idAuth0))
    //    {
    //        return NotFound("Nie ma takiego uzytkownika.");
    //    }

    //    var deliveryRequests = _deliveryservice.GetUserDeliveryRequests(idAuth0);
    //    return Ok(deliveryRequests);
    //}


    
}