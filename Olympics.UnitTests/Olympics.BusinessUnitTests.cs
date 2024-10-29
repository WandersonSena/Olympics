using AutoMapper;
using Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Olympics.Business;
using Olympics.Business.AutoMapper;
using Olympics.Business.DTO;
using Olympics.DataAccess;
using Olympics.DataAccess.Entities;
using Olympics.Repository;
using Olympics.Repository.AutoMapper;
using Olympics.Repository.DAO;
using Olympics.Repository.Interfaces;

namespace Olympics.UnitTests;

public class OlympicBusinesUnitTests : UnitTestBase
{
    private OlympicResultBusiness olympicResultBusiness;
    private Mock<IOlympicResultRepository> olympicResultRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        olympicResultRepositoryMock = new Mock<IOlympicResultRepository>();
    }

    [Test]
    public void CreateNewResultSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.CreateNewResult(It.IsAny<OlympicResultDao>())).Returns(1);
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));

        var newOlympicResultDao = new DtoOlympicResultRequest()
        {
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
        var addedEntities = olympicResultBusiness.CreateNewOlympicResult(newOlympicResultDao);
        Assert.That(addedEntities, Is.EqualTo(1));
    }
    
    [Test]
    public void GetAllSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetAll(1, 10)).Returns([
            GetOlympicResultMock(),
            GetOlympicResultMock(2),
            GetOlympicResultMock(3)
        ]);
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));

        var resultList = olympicResultBusiness.GetAll(1, 10);
        Assert.That(resultList.Count, Is.EqualTo(3));
        Assert.That(resultList[2].OlympicMedalResultId, Is.EqualTo(3));
    }
    
    [Test]
    public void GetAllWithNegativeIndexTest()
    {
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));

        var ex = Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetAll(-1, 10));
        Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetAll(2, -5));
        Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetAll(0, 15));
        Assert.That(ex.Message, Is.EqualTo("Page number and page size must be greater than 0"));
    }
    
    [Test]
    public void GetByIdSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetById(1)).Returns(GetOlympicResultMock());
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        var olympicResult = olympicResultBusiness.GetById(1);
        Assert.Multiple(() =>
        {
            Assert.That(olympicResult.OlympicMedalResultId, Is.EqualTo(1));
            Assert.That(olympicResult.Athlete, Is.EqualTo("HAJOS, Alfred"));
        });
    }
    
    [Test]
    public void GetNonExistentResultIdTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetById(123)).Throws(new KeyNotFoundException());
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));

        Assert.Throws<KeyNotFoundException>(() => olympicResultBusiness.GetById(123));
    }
    
    [Test]
    public void GetByCountryCodeSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetByCountryCode("HUN", 1, 10)).Returns([
            GetOlympicResultMock(),
            GetOlympicResultMock(2),
            GetOlympicResultMock(3)
        ]);
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        var resultList = olympicResultBusiness.GetByCountryCode("HUN");
        Assert.Multiple(() =>
        {
            Assert.That(resultList.Count, Is.EqualTo(3));
            Assert.That(resultList[1].OlympicMedalResultId, Is.EqualTo(2));
        });
    }
    
    [Test]
    public void GetByCountryWithNegativeIndexesTest()
    {
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        Assert.Multiple(() =>
        {
             var ex = Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetByCountryCode("HUN",2, -5));
            Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetByCountryCode("HUN",-8, 15));
            Assert.That(ex.Message, Is.EqualTo("Page number and page size must be greater than 0"));
        });
    }
    
    [Test]
    public void GetByCountryWithWrongSizeCountryCodeTest()
    {
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetByCountryCode("HUNG",2, 5));
            Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetByCountryCode("HU",8, 15));
            Assert.That(ex.Message, Is.EqualTo("Country code must be 3 characters long"));
        });
    }
    
    [Test]
    public void GetAllCountryMedalsByMedalTypeSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetByCountryCode("HUN")).Returns([
            GetOlympicResultMock(),
            GetOlympicResultMock(2),
            GetOlympicResultMock(3)
        ]);
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));

        var resultList = olympicResultBusiness.GetAllCountryMedalsByMedalType("HUN", OlympicMedal.Gold);
        Assert.That(resultList.Count, Is.EqualTo(3));
        resultList = olympicResultBusiness.GetAllCountryMedalsByMedalType("HUN", OlympicMedal.Bronze);
        Assert.That(resultList.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void GetAllCountryMedalsFromYearSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetByCountryCode("HUN")).Returns([
            GetOlympicResultMock(),
            GetOlympicResultMock(2)
        ]);
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));

        var resultList = olympicResultBusiness.GetAllCountryMedalsFromYear("HUN", 1896);
        Assert.That(resultList.Count, Is.EqualTo(2));
        resultList = olympicResultBusiness.GetAllCountryMedalsFromYear("HUN", 1900);
        Assert.That(resultList.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void GetByOlympicYearSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetByOlympicYear(1896, 1, 10)).Returns([
            GetOlympicResultMock(),
            GetOlympicResultMock(2),
            GetOlympicResultMock(3)
        ]);
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        var resultList = olympicResultBusiness.GetByOlympicYear(1896);
        Assert.Multiple(() =>
        {
            Assert.That(resultList.Count, Is.EqualTo(3));
            Assert.That(resultList[1].OlympicMedalResultId, Is.EqualTo(2));
        });
    }
    
    [Test]
    public void GetByOlympicYearWithNegativeYearTest()
    {
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetByOlympicYear(-1987,2, 5));
            Assert.That(ex.Message, Is.EqualTo("Olympic year must be greater than 0"));
        });
    }
    
    [Test]
    public void GetByOlympicSportAndYearSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.GetByOlympicYear(1896)).Returns([
            GetOlympicResultMock(),
            GetOlympicResultMock(2)
        ]);
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        var resultList = olympicResultBusiness.GetByOlympicSportAndYear(1896, "Aquatics");
        Assert.That(resultList.Count, Is.EqualTo(2));
        resultList = olympicResultBusiness.GetByOlympicSportAndYear(1896, "Ice Hockey");
        Assert.That(resultList.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void GetByOlympicSportAndYearWithEmptySportNameTest()
    {
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));
        
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => olympicResultBusiness.GetByOlympicSportAndYear(1987, string.Empty));
            Assert.That(ex.Message, Is.EqualTo("Sport name cannot be empty"));
        });
    }
    
    [Test]
    public void UpdateResultByIdSuccessfullyTest()
    {
        olympicResultRepositoryMock.Setup(m => m.UpdateResultById(1, It.IsAny<OlympicResultDao>())).Returns(GetOlympicResultMock());
        olympicResultBusiness = new OlympicResultBusiness(olympicResultRepositoryMock.Object, CreateAutoMapperFromProfileForTests(new BusinessAutoMapperProfile()));

        var newOlympicResultDao = new DtoOlympicResultRequest()
        {
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
        var updatedEntity = olympicResultBusiness.UpdateResultById(1, newOlympicResultDao);
        Assert.Multiple(() =>
        {
            Assert.That(updatedEntity.OlympicMedalResultId, Is.EqualTo(1));
            Assert.That(updatedEntity.OlympicEvent, Is.EqualTo("100M Freestyle"));
        });
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