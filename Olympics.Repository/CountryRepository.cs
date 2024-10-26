using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Olympics.DataAccess;
using Olympics.DataAccess.Entities;
using Olympics.Repository.DAO;
using Olympics.Repository.Interfaces;

namespace Olympics.Repository;

public class CountryRepository(
    DataContext context,
    IMapper mapper) : ICountryRepository 
{
     #region Public Methods
    
        public int CreateNewCountry(NewCountryRequest request)
        {
            IsValidNewCountryRequest(request);
            context.Set<CountryData>().Add(new CountryData()
            {
                Code = request.Code,
                Name = request.Name,
                Population = request.Population,
                GdpPerCapita = request.GdpPerCapita
            });
            context.SaveChanges();
            
            return context.SaveChanges();
        }
        
        public GetCountryResponse GetCountryByCountryCode(string code, bool includeCountryMedals = false)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code), "Country code cannot be null or empty.");
            }
            
            var countryData = includeCountryMedals ? context.Set<CountryData>().FirstOrDefault(): 
                context.Set<CountryData>().Include(c => c.OlympicMedals).FirstOrDefault();
            if (countryData == null)
            {
                throw new ArgumentException($"Country with Code {code} does not exist.");
            }
            
            return mapper.Map<GetCountryResponse>(countryData);
        }
    
    #endregion

    #region Private Methods

    private void IsValidNewCountryRequest(NewCountryRequest request)
    {
        request.VerifyIfRequestIsValid();
        if (context.Set<CountryData>().Any(c => c.Code == request.Code))
        {
            throw new ArgumentException($"Country with Code {request.Code} already exists.");
        }
    }
    
    #endregion
    
}