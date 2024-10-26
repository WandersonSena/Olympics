namespace Olympics.Business.DTO;

public class DtoNewCountryResponse
{
    public bool Created { get; set; }
    public string Message { get; set; }

    public DtoNewCountryResponse(int newCountryId)
    {
        if (newCountryId is 0 or < 0)
        {
            Created = false;
            Message = "An error occured during the creation of the country";
        }
        
        Created = true;
        Message = "Country created successfully";
    }
}