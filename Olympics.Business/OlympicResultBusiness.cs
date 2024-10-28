using AutoMapper;
using Olympics.Business.DTO;
using Olympics.Business.Interfaces;
using Olympics.Repository.DAO;
using Olympics.Repository.Interfaces;

namespace Olympics.Business;

public class OlympicResultBusiness(
    IOlympicResultRepository olympicResultRepository,
    IMapper mapper) : IOlympicResultBusiness
{
    public int CreateNewOlympicResult(DtoOlympicResultRequest request)
    {
        var mappedRequest = mapper.Map<OlympicResultDao>(request);
        return olympicResultRepository.CreateNewResult(mappedRequest);
    }

    public List<DtoOlympicResultResponse> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var resultList = olympicResultRepository.GetAll(pageNumber, pageSize);
        return resultList.Select(result => new DtoOlympicResultResponse(result)).ToList();
    }
    
    public DtoOlympicResultResponse GetById(int olympicResultId)
    {
        var olympicResult = olympicResultRepository.GetById(olympicResultId);
        return new DtoOlympicResultResponse(olympicResult);
    }

    public List<DtoOlympicResultResponse> GetByCountryCode(string countryCode, int pageNumber = 1, int pageSize = 10)
    {
        var resultList = olympicResultRepository.GetByCountryCode(countryCode, pageNumber, pageSize);
        return resultList.Select(result => new DtoOlympicResultResponse(result)).ToList();
    }

    public List<DtoOlympicResultResponse> GetByOlympicYear(int olympicYear, int pageNumber = 1, int pageSize = 10)
    {
        var resultList = olympicResultRepository.GetByOlympicYear(olympicYear, pageNumber, pageSize);
        return resultList.Select(result => new DtoOlympicResultResponse(result)).ToList();
    }

    public DtoOlympicResultResponse UpdateResultById(int olympicResultId, DtoOlympicResultRequest request)
    {
        var mappedRequest = mapper.Map<OlympicResultDao>(request);
        var result = olympicResultRepository.UpdateResultById(olympicResultId, mappedRequest);
        return new DtoOlympicResultResponse(result);
    }
    
    public void DeleteResultById(int olympicResultId)
    {
        olympicResultRepository.DeleteResultById(olympicResultId);
    }
}