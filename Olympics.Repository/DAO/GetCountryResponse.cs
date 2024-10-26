namespace Olympics.Repository.DAO;

public class GetCountryResponse
{
    public int CountryId { get; set; }
    
    public string Code { get; set; }
    
    public string Name { get; set; }
    
    public long Population { get; set; }
    
    public decimal GdpPerCapita { get; set; }
}