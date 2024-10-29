using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Olympics.DataAccess;

namespace Olympics.IntegrationTests;

public class IntegrationTestBase
{
    protected static DataContext CreateDataContextForTests(string databaseName)
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName).Options;
        return new DataContext(options);
    }
    
    protected static IMapper CreateAutoMapperFromProfileForTests(Profile profile)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(profile);
        });
        return mappingConfig.CreateMapper();
    }
}