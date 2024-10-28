using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Olympics.DataAccess.Entities;

[Table("T_OlympicMedalResult")]
public class OlympicMedalResultData
{
    [Key]
    public int OlympicMedalResultId { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public string OlympicCity { get; set; }
    [Required]
    public string Sport { get; set; }
    [Required]
    public string Discipline { get; set; }
    [Required]
    public string Athlete { get; set; }
    [Required]
    public GenderEnum AthleteGender { get; set; }
    [Required]
    public string OlympicEvent { get; set; }
    [Required]
    public OlympicMedal OlympicMedal { get; set; }
    [Required]
    public OlympicType OlympicType { get; set; }
    public string AthleteCountry { get; set; }
}