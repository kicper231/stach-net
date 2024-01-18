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
            // Your existing code
            var response =  _deliveryRequestService.GetOffers(DRDTO);
            SaveInDatabaseDeliveryRequest(DRDTO, response);
            
            //await _context.SaveChangesAsync();
            //// Zapisz zmiany w bazie danych
            //await _context.SaveChangesAsync();

            return Ok(response);  // This is now valid
        }


        [HttpPost("offers")]
        public async Task<ActionResult<OfferRespondDTO>> GetOfferIdandAcceptOffer([FromBody] OfferDTO DRDTO)
        {
            
            var response = _deliveryRequestService.AcceptOffer(DRDTO);
            SaveInDatabaseDelivery(DRDTO);

            return Ok(response);  
        }

        [HttpGet("status/{OfferGuid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
        public async Task<IActionResult> GetStatus([FromBody] Guid OfferGuid)
        {
            try
            {
                
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

        [HttpPost("status/ChangeStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuidInt))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ErrorResponse>))]
        public async Task<IActionResult> SetStatus([FromBody] GuidInt OfferGuid)
        {
            try
            {
                var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => (d.DeliveryGuid == OfferGuid.g));
                if (delivery == null)
                {
                    return NotFound($"Nie znaleziono DeliveryRequest o GUID: {OfferGuid.g}");
                }
                else
                {
                    delivery.DeliveryStatus  = (DeliveryStatus)Enum.ToObject(typeof(DeliveryStatus), OfferGuid.i);
                    _context.SaveChanges();
                    return Ok("Status updated successfully");
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            };

        }


        void SaveInDatabaseDeliveryRequest(DeliveryRequestDTO DRDTO,InquiryDTO response)
        {
            var PackageDB = new Package
            {
                Width = DRDTO.Package.Width,
                Height = DRDTO.Package.Height,
                Weight = DRDTO.Package.Weight,
                Length = DRDTO.Package.Length
            };
            
            _context.Packages.Add(PackageDB);
            _context.SaveChanges();
            int PackageID = PackageDB.PackageId;

            var SourceAddressDB = new Address
            { 
                HouseNumber=DRDTO.SourceAddress.HouseNumber,
                City=DRDTO.SourceAddress.City,
                Country=DRDTO.SourceAddress.Country,
                zipCode=DRDTO.SourceAddress.zipCode,
                Street=DRDTO.SourceAddress.Street,
                ApartmentNumber=DRDTO.SourceAddress.ApartmentNumber           
            };
            _context.Addresses.Add(SourceAddressDB);
            _context.SaveChanges();
        
            int SourceAddressID = SourceAddressDB.AddressId;

            var DestinationAddressDB = new Address
            {
                HouseNumber = DRDTO.DestinationAddress.HouseNumber,
                City = DRDTO.DestinationAddress.City,
                Country = DRDTO.DestinationAddress.Country,
                zipCode = DRDTO.DestinationAddress.zipCode,
                Street = DRDTO.DestinationAddress.Street,
                ApartmentNumber = DRDTO.DestinationAddress.ApartmentNumber
            };
            _context.Addresses.Add(DestinationAddressDB);
            _context.SaveChanges();
            int DestinationAddressID = DestinationAddressDB.AddressId;
            
            var DeliveryRequestDB = new DeliveryRequest
            {
                PackageId = PackageID,
                SourceAddressId=SourceAddressID,
                DestinationAddressId=DestinationAddressID,                
                DeliveryDate=DRDTO.DeliveryDate,
                RequestDate=DateTime.Now,
                Status=DeliveryRequestStatus.Pending,
                WeekendDelivery=DRDTO.WeekendDelivery,
                Priority=(DRDTO.Priority==true)?PackagePriority.High:PackagePriority.Low,
                DeliveryRequestGuid=response.InquiryDTOGuid

            };
            _context.DeliveryRequests.Add(DeliveryRequestDB);
            _context.SaveChanges();

        }


        void SaveInDatabaseDelivery(OfferDTO DRDTO)
        {
            var DeliveryDB = new Delivery
            {
                DeliveryGuid = DRDTO.OfferDTOGuid,              
                DeliveryStatus=DeliveryStatus.AcceptedByWorker
            };
            _context.Deliveries.Add(DeliveryDB);
            _context.SaveChanges();
        }
    }
}
