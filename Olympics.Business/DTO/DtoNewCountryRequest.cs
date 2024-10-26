namespace Olympics.Business.DTO;

public class DtoNewCountryRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public long Population { get; set; }
    public decimal GdpPerCapita { get; set; }
}