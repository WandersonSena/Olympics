using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
    [Route("")]
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
    
    [HttpGet]
    [Route("")]
    public ActionResult GetAllCountries()
    {
        try
        {
            var countries = countryBusiness.GetAll();
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpGet]
    [Route("{countryCode}")]
    public ActionResult GetCountryByCode(string countryCode)
    {
        try
        {
            var countries = countryBusiness.GetByCode(countryCode);
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpPut]
    [Route("UpdateCountry")]
    public ActionResult UpdateCountry(NewCountryViewModel newCountryRequest)
    {
        try
        {
            var updatedCountry = countryBusiness.UpdateCountryByCode(mapper.Map<DtoNewCountryRequest>(newCountryRequest));
            return new OkObjectResult(updatedCountry);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpDelete]
    [Route("{countryCode}")]
    public ActionResult DeleteCountryByCode(string countryCode)
    {
        try
        {
            countryBusiness.DeleteCountryByCode(countryCode);
            return new OkResult();
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
}