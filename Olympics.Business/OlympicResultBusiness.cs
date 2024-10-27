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
        var userId = olympicResultRepository.CreateNewResult(mapper.Map<OlympicResultDao>(request));
    }

    public List<DtoOlympicResultResponse> GetAll()
    {
        var resultList = olympicResultRepository.GetAll();
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