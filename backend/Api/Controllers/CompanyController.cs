using Api.Service;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService CompanyService)
    {
        _companyService = CompanyService;
    }

    [HttpGet("office-worker/get-all-inquiries")]
    public async Task<ActionResult<List<InquiryCompanyDTO>>> GetCompanyInquiries()
    {
        try
        {
            var deliveryRequests = await _companyService.GetCompanyInquries();
            return Ok(deliveryRequests);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("office-worker/get-all-deliveries")]
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

    [HttpPost("office-worker/change-delivery-status")]

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