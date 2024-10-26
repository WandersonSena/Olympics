using AutoMapper;
using Olympics.Business.DTO;
using Olympics.Business.Interfaces;
using Olympics.Repository.DAO;
using Olympics.Repository.Interfaces;

namespace Olympics.Business;

public class CountryBusiness(
    ICountryRepository countryRepository,
    IMapper mapper) : ICountryBusiness
{
    #region Public Methods

    public DtoNewCountryResponse CreateNewCountry(DtoNewCountryRequest request)
    {
        var userId = countryRepository.CreateNewCountry(mapper.Map<NewCountryRequest>(request));
        return new DtoNewCountryResponse(userId);
    }
    
    #endregion
}