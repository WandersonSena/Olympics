using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Olympics.Business.Interfaces;

namespace Olympics.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OlympicsController(
    IOlympicResultBusiness olympicResultBusiness, 
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Route("{olympicYear:int}")]
    public ActionResult GetAllResults(int olympicYear, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var countries = olympicResultBusiness.GetByOlympicYear(olympicYear, pageNumber, pageSize);
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpGet]
    [Route("{olympicYear:int}/sport/{sportName}")]
    public ActionResult GetAllResults(int olympicYear, string sportName)
    {
        try
        {
            var countries = olympicResultBusiness.GetByOlympicSportAndYear(olympicYear, sportName);
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
}