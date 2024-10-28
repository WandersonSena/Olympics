using Microsoft.EntityFrameworkCore;
using Olympics.DataAccess.Entities;

namespace Olympics.DataAccess;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<OlympicMedalResultData> OlympicMedalResultData { get; init; }
}