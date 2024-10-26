namespace Olympics.Repository.DAO;

public class NewCountryRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public long Population { get; set; }
    public decimal GdpPerCapita { get; set; }

    
    public void VerifyIfRequestIsValid()
    {
        if (string.IsNullOrWhiteSpace(Code))
        {
            throw new ArgumentException("Country code is required.");
        }
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("Country name is required.");
        }
    }
}