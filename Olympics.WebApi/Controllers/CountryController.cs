using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Olympics.Business.Interfaces;

namespace Olympics.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController(
    IOlympicResultBusiness olympicResultBusiness, 
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Route("{countryCode}")]
    public ActionResult GetAllResults(string countryCode, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var countries = olympicResultBusiness.GetByCountryCode(countryCode, pageNumber, pageSize);
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
}