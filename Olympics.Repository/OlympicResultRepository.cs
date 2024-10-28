using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Olympics.DataAccess;
using Olympics.DataAccess.Entities;
using Olympics.Repository.DAO;
using Olympics.Repository.Interfaces;

namespace Olympics.Repository;

public class OlympicResultRepository(
    DataContext context,
    IMapper mapper) : IOlympicResultRepository
{
    #region Public Methods
    
        public int CreateNewResult(OlympicResultDao request)
        {
            var newResult = new OlympicMedalResultData
            {
                Year = request.Year,
                OlympicCity = request.OlympicCity,
                Sport = request.Sport,
                Discipline = request.Discipline,
                Athlete = request.Athlete,
                AthleteCountry = request.AthleteCountry,
                AthleteGender = request.AthleteGender,
                OlympicEvent = request.OlympicEvent,
                OlympicMedal = request.OlympicMedal,
                OlympicType = request.OlympicType,
            };
            context.Set<OlympicMedalResultData>().Add(newResult);
            
            return context.SaveChanges();
        }
        
        public List<OlympicResultDao> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var resultList = context.Set<OlympicMedalResultData>().Skip(--pageNumber * pageSize).Take(pageSize).ToList();
            return mapper.Map<List<OlympicResultDao>>(resultList);
        }
       
        public OlympicResultDao UpdateResultById(OlympicResultDao request)
        {
            var result = context.Set<OlympicMedalResultData>().FirstOrDefault(c => c.OlympicMedalResultId == request.OlympicMedalResultId);
            if (result is null)
            {
                throw new ArgumentException($"Country with Code {request.OlympicMedalResultId} does not exist.");
            
            }
            result.Year = request.Year;
            result.OlympicCity = request.OlympicCity;
            result.Sport = request.Sport;
            result.Discipline = request.Discipline;
            result.Athlete = request.Athlete;
            result.AthleteCountry = request.AthleteCountry;
            result.AthleteGender = request.AthleteGender;
            result.OlympicEvent = request.OlympicEvent;
            result.OlympicMedal = request.OlympicMedal;
            result.OlympicType = request.OlympicType;
            context.SaveChanges();
            
            return mapper.Map<OlympicResultDao>(result);
        }
        
        public void DeleteResultById(int resultId)
        {
            var olympicMedalResult = context.Set<OlympicMedalResultData>().FirstOrDefault(c => c.OlympicMedalResultId == resultId);
            if (olympicMedalResult is null)
            {
                throw new ArgumentException($"Result with Id {resultId} does not exist.");
            
            }
            context.Set<OlympicMedalResultData>().Remove(olympicMedalResult);
            context.SaveChanges();
        }
    
    #endregion
}