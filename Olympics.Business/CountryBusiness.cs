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

    public DtoCountryResponse CreateNewCountry(DtoNewCountryRequest request)
    {
        var userId = countryRepository.CreateNewCountry(mapper.Map<CountryDao>(request));
        return new DtoCountryResponse(userId);
    }

    public List<DtoCountryResponse> GetAll()
    {
        var countryList = countryRepository.GetAll();
        return countryList.Select(country => new DtoCountryResponse(country, true, string.Empty)).ToList();
    }

    public DtoCountryResponse GetByCode(string countryCode)
    {
        var country = countryRepository.GetCountryByCountryCode(countryCode);
        return new DtoCountryResponse(country, true, string.Empty);
    }

    public DtoCountryResponse UpdateCountryByCode(DtoNewCountryRequest request)
    {
        var country = countryRepository.UpdateCountryByCode(mapper.Map<CountryDao>(request));
        return new DtoCountryResponse(country, true, "Country updated successfully.");
    }
    
    public void DeleteCountryByCode(string countryCode)
    {
        countryRepository.DeleteCountryByCode(countryCode);
    }

    #endregion
}