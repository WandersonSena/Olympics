using AutoMapper;
using Olympics.Repository.AutoMapper;

namespace Olympics.UnitTests;

public class UnitTestBase
{
    protected static IMapper CreateAutoMapperFromProfileForTests(RepositoryAutoMapperProfile profile)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(profile);
        });
        return mappingConfig.CreateMapper();
    }
}