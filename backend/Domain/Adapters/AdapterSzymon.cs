using Domain.DTO;

namespace Domain.Adapters;

//raczej konwenter nie adapter ale cicho  jak starczy czasu to zmienie strukture kodu na adapter // jak bede wiedzial jak 
public class SzymonApiAdapter
{
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
}