using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Olympics.DataAccess.Entities;

[Table("T_CountryData")]
[Index(nameof(Code), IsUnique = true)]
public class CountryData
{
    [Key]
    public int CountryId { get; set; }
    
    [Required]
    [MaxLength(3)]
    public string Code { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public long Population { get; set; }
    
    public decimal GdpPerCapita { get; set; }
    
    public ICollection<OlympicMedalResultData> OlympicMedals { get; set; }
}