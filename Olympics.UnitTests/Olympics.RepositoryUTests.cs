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
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateNewCountrySuccessfullyTest()
    {
        Assert.Pass();
    }
}