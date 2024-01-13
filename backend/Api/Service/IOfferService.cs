using Domain.DTO;

namespace Api.Service
{
    public interface IOfferService
    {
         
        public Task<string> GetTokenAsync();

        public  Task<OfferRespondDTO> GetOfferOurID(OfferDTO a);
        public Task<OfferRespondDTO> GetOfferSzymonID(OfferDTO a);
    }
}
