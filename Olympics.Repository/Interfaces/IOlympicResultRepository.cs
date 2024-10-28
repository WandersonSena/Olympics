using Olympics.Repository.DAO;

namespace Olympics.Repository.Interfaces;

public interface IOlympicResultRepository
{
    int CreateNewResult(OlympicResultDao request);
    List<OlympicResultDao> GetAll(int pageNumber = 1, int pageSize = 10);
    OlympicResultDao GetById(int olympicResultId);
    List<OlympicResultDao> GetByCountryCode(string countryCode, int pageNumber = 1, int pageSize = 10);
    List<OlympicResultDao> GetByOlympicYear(int year, int pageNumber = 1, int pageSize = 10);
    OlympicResultDao UpdateResultById(int olympicResultId, OlympicResultDao request);
    void DeleteResultById(int olympicResultId);
}