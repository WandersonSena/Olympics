using Olympics.Repository.DAO;

namespace Olympics.Repository.Interfaces;

public interface IOlympicResultRepository
{
    int CreateNewResult(OlympicResultDao request);
    List<OlympicResultDao> GetAll(int pageNumber = 1, int pageSize = 10);
    OlympicResultDao UpdateResultById(OlympicResultDao request);
    void DeleteResultById(int resultId);
}