using Olympics.Repository.DAO;

namespace Olympics.Business.DTO;

public class DtoCountryResponse
{
    public int CountryId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public long Population { get; set; }
    public decimal GdpPerCapita { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }

    public DtoCountryResponse(int newCountryId)
    {
        if (newCountryId is 0 or < 0)
        {
            Success = false;
            Message = "An error occured during the creation of the country";
        }

        CountryId = newCountryId;
        Success = true;
        Message = "Country created successfully";
    }
    
    public DtoCountryResponse(CountryDao country, bool success, string message)
    {
        CountryId = country.CountryId;
        Code = country.Code;
        Name = country.Name;
        Population = country.Population;
        GdpPerCapita = country.GdpPerCapita;
        
        Success = success;
        Message = message;
    }
}