using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olympics.Business.DTO;
using Olympics.Business.Interfaces;
using Olympics.WebApi.ViewModels;

namespace Olympics.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OlympicResultController(
    IOlympicResultBusiness olympicResultBusiness, 
    IMapper mapper) : ControllerBase
{
    
     
    [HttpPost]
    [Route("")]
    public ActionResult CreateNewResult(NewOlympicResultViewModel newCountryViewModel)
    {
        try
        {
            olympicResultBusiness.CreateNewCountry(mapper.Map<DtoOlympicResultRequest>(newCountryViewModel));
            return new CreatedResult();
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpGet]
    [Route("")]
    public ActionResult GetAllResults()
    {
        try
        {
            var countries = olympicResultBusiness.GetAll();
            return new OkObjectResult(countries);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpPut]
    [Route("{resultId:int}")]
    public ActionResult UpdateResultById(int resultId, [FromBody]NewOlympicResultViewModel newResultViewModel)
    {
        try
        {
            var updatedCountry = olympicResultBusiness.UpdateResultById(mapper.Map<DtoOlympicResultRequest>(newResultViewModel));
            return new OkObjectResult(updatedCountry);
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
    
    [HttpDelete]
    [Route("{resultId:int}")]
    public ActionResult DeleteResultById(int resultId)
    {
        try
        {
            olympicResultBusiness.DeleteResultById(resultId);
            return new OkResult();
        }
        catch
        {
            return new BadRequestObjectResult("One ore more errors occured, please check the information provided.");
        }
    }
}

