using Olympics.Business.DTO;

namespace Olympics.Business.Interfaces;

public interface ICountryBusiness
{
    DtoCountryResponse CreateNewCountry(DtoNewCountryRequest request);
    List<DtoCountryResponse> GetAll();
    DtoCountryResponse GetByCode(string countryCode);
    DtoCountryResponse UpdateCountryByCode(DtoNewCountryRequest request);
    void DeleteCountryByCode(string countryCode);
}