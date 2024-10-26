using Olympics.Business.DTO;

namespace Olympics.Business.Interfaces;

public interface ICountryBusiness
{
    DtoNewCountryResponse CreateNewCountry(DtoNewCountryRequest request);
}