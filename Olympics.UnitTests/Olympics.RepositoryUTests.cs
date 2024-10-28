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

public class Tests : UnitTestBase
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
        olympicResultRepositoryMock.Setup(m => m.CreateNewResult(GetOlympicResultMock())).Returns(1);
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
        Assert.That(addedEntities, Is.EqualTo(0));
    }

    private static OlympicResultDao GetOlympicResultMock()
    {
        return new OlympicResultDao
        {
            OlympicMedalResultId = 0,
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