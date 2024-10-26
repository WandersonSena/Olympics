using Olympics.Repository.DAO;

namespace Olympics.Repository.Interfaces;

public interface ICountryRepository
{
    int CreateNewCountry(NewCountryRequest request);
    GetCountryResponse GetCountryByCountryCode(string code, bool includeCountryMedals = false);
}