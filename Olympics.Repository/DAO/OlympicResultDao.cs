using Enums;

namespace Olympics.Repository.DAO;

public class OlympicResultDao
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
    public CountryDao AthleteCountry { get; set; }
}