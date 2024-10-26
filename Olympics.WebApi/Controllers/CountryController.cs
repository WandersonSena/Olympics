using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Olympics.Business.DTO;
using Olympics.Business.Interfaces;
using Olympics.WebApi.ViewModels;

namespace Olympics.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController(
    ICountryBusiness countryBusiness, 
    IMapper mapper) : ControllerBase
{
    
    [HttpPost]
    [Route("CreateNewCountry")]
    public ActionResult CreateNewRegister(NewCountryViewModel newCountryRequest)
    {
        try
        {
            countryBusiness.CreateNewCountry(mapper.Map<DtoNewCountryRequest>(newCountryRequest));
            return new CreatedResult();
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
}