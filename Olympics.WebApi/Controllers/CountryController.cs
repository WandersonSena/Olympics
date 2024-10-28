using AutoMapper;
using Enums;
using Microsoft.AspNetCore.Mvc;
using Olympics.Business.Interfaces;

namespace Olympics.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/{countryCode}")]
public class CountryController(
    IOlympicResultBusiness olympicResultBusiness, 
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Route("")]
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
    
    [HttpGet]
    [Route("medals/{medalType}")]
    public ActionResult GetAllResults(string countryCode, OlympicMedal medalType)
    {
        try
        {
            var countries = olympicResultBusiness.GetAllCountryMedalsByMedalType(countryCode, medalType);
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpGet]
    [Route("year/{olympicYear:int}")]
    public ActionResult GetAllResults(string countryCode, int olympicYear)
    {
        try
        {
            var countries = olympicResultBusiness.GetAllCountryMedalsFromYear(countryCode, olympicYear);
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
}