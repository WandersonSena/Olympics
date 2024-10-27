using Olympics.Business.DTO;

namespace Olympics.Business.Interfaces;

public interface IOlympicResultBusiness
{
    void CreateNewCountry(DtoOlympicResultRequest request);
    List<DtoOlympicResultResponse> GetAll();
    DtoOlympicResultResponse UpdateResultById(DtoOlympicResultRequest request);
    void DeleteResultById(int resultId);
}