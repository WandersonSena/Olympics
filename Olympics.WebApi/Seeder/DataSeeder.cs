using Enums;
using Olympics.DataAccess.Entities;

namespace Olympics.DataAccess.Seeder;

public class DataSeeder(DataContext context)
{
    public void Seed()
    {
        SeedSummerGames();
        SeedWinterGames();
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
                AthleteCountry = values[5],
                AthleteGender = (GenderEnum) Enum.Parse(typeof(GenderEnum), values[6]),
                OlympicEvent = values[7],
                OlympicMedal = (OlympicMedal) Enum.Parse(typeof(OlympicMedal), values[8]),
                OlympicType = olympicType
            });
        }

        return resultSeedData;
    }
}