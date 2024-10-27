using Enums;
using Olympics.Repository.DAO;

namespace Olympics.Business.DTO;

public class DtoOlympicResultResponse
{
    public int OlympicMedalResultId { get; set; }
    public int Year { get; set; }
    public string OlympicCity { get; set; }
    public string Sport { get; set; }
    public string Discipline { get; set; }
    public string Athlete { get; set; }
    public GenderEnum AthleteGender { get; set; }
    public string OlympicEvent { get; set; }
    public OlympicMedal OlympicMedal { get; set; }
    public OlympicType OlympicType { get; set; }
    
    public DtoCountryResponse? AthleteCountry { get; set; }

    public DtoOlympicResultResponse(OlympicResultDao resultDao)
    {
        OlympicMedalResultId = resultDao.OlympicMedalResultId;
        Year = resultDao.Year;
        OlympicCity = resultDao.OlympicCity;
        Sport = resultDao.Sport;
        Discipline = resultDao.Discipline;
        Athlete = resultDao.Athlete;
        AthleteGender = resultDao.AthleteGender;
        OlympicEvent = resultDao.OlympicEvent;
        OlympicMedal = resultDao.OlympicMedal;
        OlympicType = resultDao.OlympicType;
    }
    
    public DtoOlympicResultResponse(OlympicResultDao resultDao, DtoCountryResponse athleteCountry)
    {
        OlympicMedalResultId = resultDao.OlympicMedalResultId;
        Year = resultDao.Year;
        OlympicCity = resultDao.OlympicCity;
        Sport = resultDao.Sport;
        Discipline = resultDao.Discipline;
        Athlete = resultDao.Athlete;
        AthleteGender = resultDao.AthleteGender;
        OlympicEvent = resultDao.OlympicEvent;
        OlympicMedal = resultDao.OlympicMedal;
        OlympicType = resultDao.OlympicType;
        AthleteCountry = athleteCountry;
    }
}