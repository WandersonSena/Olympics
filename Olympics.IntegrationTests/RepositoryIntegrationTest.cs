using Enums;
using Olympics.Repository;
using Olympics.Repository.AutoMapper;
using Olympics.Repository.DAO;

namespace Olympics.IntegrationTests;

public class RepositoryIntegrationTest : IntegrationTestBase
{
    private OlympicResultRepository olympicResultRepository;
    
    [Test]
    public void CreateNewResultSuccessfullyTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("CreateNewResultSuccessfullyTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));
        
        var numberOfAddedEntities = olympicResultRepository.CreateNewResult(GetOlympicResultMock(0));
        Assert.That(numberOfAddedEntities, Is.EqualTo(1));
        var addedEntity = olympicResultRepository.GetById(1);
        Assert.Multiple(() =>
        {
            Assert.That(addedEntity.OlympicMedalResultId, Is.EqualTo(1));
            Assert.That(addedEntity.OlympicEvent, Is.EqualTo("100M Freestyle"));
        });
    }
    
    [Test]
    public void GetNonExistentResultTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("GetNonExistentResultTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));
        
        Assert.Throws<KeyNotFoundException>(() => olympicResultRepository.GetById(100));
    }
    
    [Test]
    public void AddMultipleResultsAndRetrieveListTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("AddMultipleResultsAndRetrieveListTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));
        
        var newEntity = GetOlympicResultMock(0);
        olympicResultRepository.CreateNewResult(newEntity);
        newEntity.Athlete = "HERSCHMANN, Otto";
        newEntity.AthleteCountry = "AUT";
        newEntity.OlympicMedal = OlympicMedal.Silver;
        olympicResultRepository.CreateNewResult(newEntity);
        newEntity.Athlete = "DRIVAS, Dimitrios";
        newEntity.AthleteCountry = "GRE";
        newEntity.OlympicMedal = OlympicMedal.Bronze;
        olympicResultRepository.CreateNewResult(newEntity);
        
        var resultList = olympicResultRepository.GetByOlympicYear(1896);
        Assert.That(resultList.Count, Is.EqualTo(3));
        
        resultList = olympicResultRepository.GetByCountryCode("GRE");
        Assert.That(resultList.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void AddMultipleResultsAndRetrieveListUsingPaginationTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("AddMultipleResultsAndRetrieveListUsingPaginationTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));

        var newEntity = GetOlympicResultMock(0);
        olympicResultRepository.CreateNewResult(newEntity);
        newEntity.Athlete = "HERSCHMANN, Otto";
        newEntity.OlympicMedal = OlympicMedal.Silver;
        olympicResultRepository.CreateNewResult(newEntity);
        newEntity.Athlete = "DRIVAS, Dimitrios";
        newEntity.OlympicMedal = OlympicMedal.Bronze;
        olympicResultRepository.CreateNewResult(newEntity);
        
        var resultList = olympicResultRepository.GetByOlympicYear(1896, 1, 10);
        Assert.That(resultList.Count, Is.EqualTo(3));
        
        resultList = olympicResultRepository.GetByOlympicYear(1896, 1, 2);
        Assert.That(resultList.Count, Is.EqualTo(2));
        
        resultList = olympicResultRepository.GetByCountryCode("HUN", 1, 1);
        Assert.That(resultList.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void UpdateResultSuccessfullyTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("UpdateResultSuccessfullyTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));

        var newEntity = GetOlympicResultMock(0);
        olympicResultRepository.CreateNewResult(newEntity);
        
        newEntity.OlympicMedal = OlympicMedal.Silver;
        olympicResultRepository.UpdateResultById(1, newEntity);
        
        var updatedResult = olympicResultRepository.GetById(1);
        Assert.That(updatedResult.OlympicMedal, Is.EqualTo(OlympicMedal.Silver));
    }
    
    [Test]
    public void TryToUpdateNonExistentResultTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("TryToUpdateNonExistentResultTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));

        var newEntity = GetOlympicResultMock(0);
        olympicResultRepository.CreateNewResult(newEntity);
        
        newEntity.OlympicMedal = OlympicMedal.Silver;
        Assert.Throws<ArgumentException>(() => olympicResultRepository.UpdateResultById(2, newEntity));
    }
    
    [Test]
    public void DeleteResultSuccessfullyTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("DeleteResultSuccessfullyTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));

        var newEntity = GetOlympicResultMock(0);
        olympicResultRepository.CreateNewResult(newEntity);
        
        olympicResultRepository.DeleteResultById(1);
        var resultList = olympicResultRepository.GetAll( 1, 10);
        Assert.That(resultList.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void TryToDeleteNonExistentResultTest()
    {
        olympicResultRepository = new OlympicResultRepository(CreateDataContextForTests("TryToDeleteNonExistentResultTest"), CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));

        var newEntity = GetOlympicResultMock(0);
        olympicResultRepository.CreateNewResult(newEntity);
        
        Assert.Throws<ArgumentException>(() => olympicResultRepository.DeleteResultById(14));
        
        var resultList = olympicResultRepository.GetAll();
        Assert.That(resultList.Count, Is.EqualTo(1));
    }

    
    private static OlympicResultDao GetOlympicResultMock(int olympicResultId = 1)
    {
        return new OlympicResultDao
        {
            OlympicMedalResultId = olympicResultId,
            Year = 1896,
            OlympicCity = "Athens",
            Sport = "Aquatics",
            Discipline = "Swimming",
            Athlete = "HAJOS, Alfred",
            AthleteCountry = "HUN",
            AthleteGender = GenderEnum.Men,
            OlympicEvent = "100M Freestyle",
            OlympicMedal = OlympicMedal.Gold,
            OlympicType = OlympicType.Summer,
        };
    }
}