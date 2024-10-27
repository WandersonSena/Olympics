using Olympics.Repository.DAO;

namespace Olympics.Repository.Interfaces;

public interface ICountryRepository
{
    int CreateNewCountry(CountryDao request);
    List<CountryDao> GetAll(bool includeCountryMedals = false);
    CountryDao GetCountryByCountryCode(string code, bool includeCountryMedals = false);
    CountryDao UpdateCountryByCode(CountryDao request);
    void DeleteCountryByCode(string countryCode);
}