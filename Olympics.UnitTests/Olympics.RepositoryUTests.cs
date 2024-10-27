using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Olympics.DataAccess;
using Olympics.DataAccess.Entities;
using Olympics.Repository;
using Olympics.Repository.AutoMapper;
using Olympics.Repository.DAO;

namespace Olympics.UnitTests;

public class Tests : UnitTestBase
{
    private CountryRepository _countryRepository;
    private Mock<DbSet<CountryData>> _countryDataMockSet;
    private Mock<DataContext> _dataContextMock;
    
    [SetUp]
    public void Setup()
    {
        _countryDataMockSet = new Mock<DbSet<CountryData>>();
        _dataContextMock = new Mock<DataContext>(new DbContextOptions<DataContext>());
    }

    [Test]
    public void CreateNewCountrySuccessfullyTest()
    {
        _dataContextMock.Setup(m => m.Set<CountryData>()).Returns(_countryDataMockSet.Object);
        _dataContextMock.Setup(m => m.Set<CountryData>().Any()).Returns(false);
        _countryRepository = new CountryRepository(_dataContextMock.Object, CreateAutoMapperFromProfileForTests(new RepositoryAutoMapperProfile()));
        
        _countryRepository.CreateNewCountry(new CountryDao()
        {
            Code = "BRA",
            Name = "Brazil",
            Population = 207847528,
            GdpPerCapita = (decimal)8538.589975
        });
        _countryDataMockSet.Verify(m => m.Add(It.IsAny<CountryData>()), Times.Once());
        _dataContextMock.Verify(m => m.SaveChanges(), Times.Once());
    }
}