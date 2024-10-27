using Enums;

namespace Olympics.WebApi.ViewModels;

public class NewOlympicResultViewModel
{
    public int OlympicMedalResultId { get; set; }
    public int Year { get; set; }
    public string OlympicCity { get; set; }
    public string Sport { get; set; }
    public string Discipline { get; set; }
    public string Athlete { get; set; }
    public GenderEnum AthleteGender { get; set; }
    public string OlympicEvent { get; set; }
    public string OlympicMedal { get; set; }
    public OlympicType OlympicType { get; set; }
    public string AthleteCountryCode { get; set; }
}