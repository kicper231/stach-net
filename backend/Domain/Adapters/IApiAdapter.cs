using Domain.DTO;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters;
   public interface IApiAdapter
    {

    public InquiryToSzymonDTO ConvertToInquiryToSzymonDTO(InquiryDTO inquiryDTO);

    public AddressDTO ConvertToAddressDTO(Address address);


    public PackageDTO ConvertToPackageDTO(Package package);

    public UserData? ConvertToUserData(User? user);


    public DeliveryStatus ConvertStringToDeliveryStatus(string statusString);

    public string ConvertStatusToString(DeliveryStatus status);

}
   

