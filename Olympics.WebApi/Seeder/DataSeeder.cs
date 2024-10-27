using Enums;
using Olympics.DataAccess.Entities;

namespace Olympics.DataAccess.Seeder;

public class DataSeeder(DataContext context)
{
    public void Seed()
    {
        SeedCountries();
        SeedSummerGames();
        SeedWinterGames();
    }
    private void SeedCountries()
    {
        var countrySeedData = new List<CountryData>();
        using var reader = new StreamReader(@"Seeder/Countries.csv");
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');
            countrySeedData.Add(new CountryData
            {
                Name = values[0],
                Code = values[1],
                Population = !string.IsNullOrWhiteSpace(values[2]) ? int.Parse(values[2]) : 0,
                GdpPerCapita = !string.IsNullOrWhiteSpace(values[3]) ? decimal.Parse(values[3]) : 0
            });
        }
        
        context.Set<CountryData>().AddRange(countrySeedData);
        context.SaveChanges();
    }
    
    private void SeedSummerGames()
    {
        context.Set<OlympicMedalResultData>().AddRange(GetOlympicGamesData("Seeder/SummerGames.csv", OlympicType.Winter));
        context.SaveChanges();
    }
    
    private void SeedWinterGames()
    {
        context.Set<OlympicMedalResultData>().AddRange(GetOlympicGamesData("Seeder/WinterGames.csv", OlympicType.Winter));
        context.SaveChanges();
    }
    
    private List<OlympicMedalResultData> GetOlympicGamesData(string path, OlympicType olympicType)
    {
        var resultSeedData = new List<OlympicMedalResultData>();
        using var reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split(';');
            resultSeedData.Add(new OlympicMedalResultData
            {
                Year = !string.IsNullOrWhiteSpace(values[0]) ? int.Parse(values[0]) : 0,
                OlympicCity = values[1],
                Sport = values[2],
                Discipline = values[3],
                Athlete = values[4],
                CountryId = context.Set<CountryData>().FirstOrDefault(c => c.Code == values[5])?.CountryId,
                AthleteCountry = context.Set<CountryData>().FirstOrDefault(c => c.Code == values[5]),
                AthleteGender = (GenderEnum) Enum.Parse(typeof(GenderEnum), values[6]),
                OlympicEvent = values[7],
                OlympicMedal = (OlympicMedal) Enum.Parse(typeof(OlympicMedal), values[8]),
                OlympicType = olympicType
            });
        }

        return resultSeedData;
    }
}