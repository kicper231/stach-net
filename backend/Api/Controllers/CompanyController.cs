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
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService CompanyService)
    {
        _companyService = CompanyService;
    }

    [HttpGet("officeworker/get-all-inquiries")]
    // [Authorize("officeworker:permissions")]
    public async Task<ActionResult<List<InquiryCompanyDTO>>> GetCompanyInquiries()
    {

        try {

            var deliveryRequests = await _companyService.GetCompanyInquries();
            return Ok(deliveryRequests);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }



       
     
    }

    [HttpGet("officeworker/get-all-deliveries")]
    public async Task<ActionResult<List<DeliveryCompanyDTO>>> GetCompanyDeliveries()
    {

        try
        {

            var deliveryRequests = await _companyService.GetCompanyDeliveries();
            return Ok(deliveryRequests);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }





    }



    [HttpPost("officeworker/change-delivery-status")]


    // [Authorize("officeworker:permissions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InquiryRespondDTO))]

    public async Task<IActionResult> ChangeStatusByWorker(ChangeDeliveryStatusWorkerDTO Data)
    {

        try
        {
            var respond = await _companyService.ChangeStatusByWorker(Data);
            return Ok(respond);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("get-delivery/{deliveryid}")]
    // [Authorize("officeworker:permissions")] kurier i office worker
    public async Task<ActionResult<DTOIOD>> GetDelivery(Guid deliveryid)
    {

        try
        {

            var deliveryRequests = await _companyService.GetIODAsync(deliveryid);
            return Ok(deliveryRequests);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }





    }



    [HttpGet("courier/get-all-available-delivery")]
    // [Authorize("officeworker:permissions")]
    public async Task<ActionResult<List<DTOIOD>>> GetCompanyCourierAllDelivery()
    {

        try
        {

            var deliveryRequests = await _companyService.GetAvailableIOD();
            return Ok(deliveryRequests);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }





    }



    [HttpGet("courier/get-all-my-delivery/{Auth0Id}")]
    // [Authorize("officeworker:permissions")]
    public async Task<ActionResult<List<DTOIOD>>> GetCompanyCourierDelivery(string Auth0Id)
    {

        try
        {

            var deliveryRequests = await _companyService.GetCourierIOD(Auth0Id);
            return Ok(deliveryRequests);
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }





    }



   

  

    [HttpPost("courier/change-delivery-status")]
    public async Task<IActionResult> ChangeStatusByCourier(ChangeDeliveryStatusDTO Data)
    {

        try
        {
            var respond = await _companyService.ChangeStatusByCourier(Data);
            return Ok(respond);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}