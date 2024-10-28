using Olympics.Business.DTO;

namespace Olympics.Business.Interfaces;

public interface IOlympicResultBusiness
{
    int CreateNewOlympicResult(DtoOlympicResultRequest request);
    List<DtoOlympicResultResponse> GetAll(int pageNumber = 1, int pageSize = 10);
    DtoOlympicResultResponse GetById(int olympicResultId);
    List<DtoOlympicResultResponse> GetByCountryCode(string countryCode, int pageNumber = 1, int pageSize = 10);
    List<DtoOlympicResultResponse> GetByOlympicYear(int olympicYear, int pageNumber = 1, int pageSize = 10);
    DtoOlympicResultResponse UpdateResultById(int olympicResultId, DtoOlympicResultRequest request);
    void DeleteResultById(int olympicResultId);
}