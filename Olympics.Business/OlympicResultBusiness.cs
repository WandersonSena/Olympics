using AutoMapper;
using Olympics.Business.DTO;
using Olympics.Business.Interfaces;
using Olympics.Repository.DAO;
using Olympics.Repository.Interfaces;

namespace Olympics.Business;

public class OlympicResultBusiness(
    IOlympicResultRepository olympicResultRepository,
    ICountryBusiness countryBusiness,
    IMapper mapper) : IOlympicResultBusiness
{
    public void CreateNewCountry(DtoOlympicResultRequest request)
    {
        var mappedRequest = mapper.Map<OlympicResultDao>(request);
        if (!string.IsNullOrWhiteSpace(request.AthleteCountryCode))
        {
            mappedRequest.AthleteCountry = mapper.Map<CountryDao>(countryBusiness.GetByCode(request.AthleteCountryCode));
        }
        olympicResultRepository.CreateNewResult(mappedRequest);
    }

    public List<DtoOlympicResultResponse> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var resultList = olympicResultRepository.GetAll(pageNumber, pageSize);
        return resultList.Select(result => new DtoOlympicResultResponse(result)).ToList();
    }
    
    public List<DtoOlympicResultResponse> GetAllWithCountryData(int pageNumber = 1, int pageSize = 10)
    {
        var resultList = olympicResultRepository.GetAllWithCountryData(pageNumber, pageSize);
        return resultList.Select(result => new DtoOlympicResultResponse(result, 
            new DtoCountryResponse(result.AthleteCountry))).ToList();
    }

    public DtoOlympicResultResponse UpdateResultById(DtoOlympicResultRequest request)
    {
        var mappedRequest = mapper.Map<OlympicResultDao>(request);
        if (!string.IsNullOrWhiteSpace(request.AthleteCountryCode))
        {
            mappedRequest.AthleteCountry = mapper.Map<CountryDao>(countryBusiness.GetByCode(request.AthleteCountryCode));
        }
        var result = olympicResultRepository.UpdateResultById(mappedRequest);
        return new DtoOlympicResultResponse(result);
    }
    
    public void DeleteResultById(int resultId)
    {
        olympicResultRepository.DeleteResultById(resultId);
    }
}