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
    
        public int CreateNewCountry(CountryDao request)
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
        
        public List<CountryDao> GetAll(bool includeCountryMedals = false)
        {
            var countryData = !includeCountryMedals ? context.Set<CountryData>().ToList() :
                context.Set<CountryData>().Include(c => c.OlympicMedals).ToList();
            
            return mapper.Map<List<CountryDao>>(countryData);
        }
        
        public CountryDao GetCountryByCountryCode(string code, bool includeCountryMedals = false)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code), "Country code cannot be null or empty.");
            }
            
            var countryData = includeCountryMedals ? 
                context.Set<CountryData>().FirstOrDefault(c => c.Code == code) : 
                context.Set<CountryData>().Include(c => c.OlympicMedals).FirstOrDefault(c => c.Code == code);
            if (countryData is null)
            {
                throw new ArgumentException($"Country with Code {code} does not exist.");
            }
            
            return mapper.Map<CountryDao>(countryData);
        }
        
        public CountryDao UpdateCountryByCode(CountryDao request)
        {
            request.VerifyIfRequiredFieldsAreValid();
            var country = context.Set<CountryData>().FirstOrDefault(c => c.Code == request.Code);

            if (country is null)
            {
                throw new ArgumentException($"Country with Code {request.Code} does not exist.");
            
            }
            country.Name = request.Name;
            country.Population = request.Population;
            country.GdpPerCapita = request.GdpPerCapita;
            country.Code = request.Code;
            context.SaveChanges();
            
            return mapper.Map<CountryDao>(country);
        }
        
        public void DeleteCountryByCode(string countryCode)
        {
            var country = context.Set<CountryData>().FirstOrDefault(c => c.Code == countryCode);
            if (country is null)
            {
                throw new ArgumentException($"Country with Code {countryCode} does not exist.");
            
            }
            context.Set<CountryData>().Remove(country);
            context.SaveChanges();
        }
    
    #endregion

    #region Private Methods

    private void IsValidNewCountryRequest(CountryDao request)
    {
        request.VerifyIfRequiredFieldsAreValid();
        if (context.Set<CountryData>().Any(c => c.Code == request.Code))
        {
            throw new ArgumentException($"Country with Code {request.Code} already exists.");
        }
    }
    
    #endregion
    
}