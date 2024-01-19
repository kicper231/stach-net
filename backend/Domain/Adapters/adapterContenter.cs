using Domain.DTO;
using Domain.Model;
using System.Text.RegularExpressions;

namespace Domain.Adapters;

//raczej konwenter nie adapter ale cicho  jak starczy czasu to zmienie strukture kodu na adapter // jak bede wiedzial jak 
public class ApiAdapter
{
    // interfejs
    public InquiryToSzymonDTO ConvertToInquiryToSzymonDTO(InquiryDTO inquiryDTO)
    {
        return new InquiryToSzymonDTO
        {
            Dimensions = new DimensionsDTO
            {
                Width = inquiryDTO.Package.Width,
                Height = inquiryDTO.Package.Height,
                Length = inquiryDTO.Package.Length,
                DimensionUnit = "Meters"
            },
            Currency = "PLN", // 
            Weight = inquiryDTO.Package.Weight,
            WeightUnit = "Kilograms",
            Source = inquiryDTO.SourceAddress,
            Destination = inquiryDTO.DestinationAddress,
            PickupDate = inquiryDTO.DeliveryDate,
            DeliveryDay = inquiryDTO.DeliveryDate.AddDays(3),
            DeliveryInWeekend = inquiryDTO.WeekendDelivery,
            Priority = inquiryDTO.Priority ? "High" : "Low",
            VipPackage = false,
            IsCompany = false
        };
    }



    public AddressDTO ConvertToAddressDTO(Address address)
    {
        if (address == null) return null;

        return new AddressDTO
        {
            HouseNumber = address.HouseNumber,
            ApartmentNumber = address.ApartmentNumber,
            Street = address.Street,
            City = address.City,
            zipCode = address.zipCode,
            Country = address.Country
        };
    }

  
    public PackageDTO ConvertToPackageDTO(Package package)
    {
        if (package == null) return null;

        return new PackageDTO
        {
            Width = package.Width,
            Height = package.Height,
            Length = package.Length,
            Weight = package.Weight
        };
    }

    public UserData? ConvertToUserData(User? user)
    {
        if (user == null) return null;
        else return new UserData()
        { Email= user.Email,
         FirstName = user.FirstName,
        LastName =user.LastName};

    }


    public  DeliveryStatus ConvertStringToDeliveryStatus(string statusString)
    {
        if (Enum.TryParse(statusString, true, out DeliveryStatus status))
        {
            return status;
        }
        else
        {
            return DeliveryStatus.nostatus;
        }
    }


    public  string ConvertStatusToString(DeliveryStatus status)
    {
        string statusString = status.ToString();
        string withSpaces = Regex.Replace(statusString, "(\\B[A-Z])", " $1");
        return withSpaces.ToLower();
    }




}