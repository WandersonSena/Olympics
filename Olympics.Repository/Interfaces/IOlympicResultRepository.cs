using Olympics.Repository.DAO;

namespace Olympics.Repository.Interfaces;

public interface IOlympicResultRepository
{
    int CreateNewResult(OlympicResultDao request);
    List<OlympicResultDao> GetAll();
    OlympicResultDao UpdateResultById(OlympicResultDao request);
    void DeleteResultById(int resultId);
}