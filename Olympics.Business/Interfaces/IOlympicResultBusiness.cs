using Olympics.Business.DTO;

namespace Olympics.Business.Interfaces;

public interface IOlympicResultBusiness
{
    int CreateNewOlympicResult(DtoOlympicResultRequest request);
    List<DtoOlympicResultResponse> GetAll(int pageNumber = 1, int pageSize = 10);
    DtoOlympicResultResponse UpdateResultById(DtoOlympicResultRequest request);
    void DeleteResultById(int resultId);
}