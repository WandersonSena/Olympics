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
    public void CreateNewCountry(DtoOlympicResultRequest request)
    {
        olympicResultRepository.CreateNewResult(mapper.Map<OlympicResultDao>(request));
    }

    public List<DtoOlympicResultResponse> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var resultList = olympicResultRepository.GetAll(pageNumber, pageSize);
        return resultList.Select(result => new DtoOlympicResultResponse(result)).ToList();
    }

    public DtoOlympicResultResponse UpdateResultById(DtoOlympicResultRequest request)
    {
        var result = olympicResultRepository.UpdateResultById(mapper.Map<OlympicResultDao>(request));
        return new DtoOlympicResultResponse(result);
    }
    
    public void DeleteResultById(int resultId)
    {
        olympicResultRepository.DeleteResultById(resultId);
    }
}