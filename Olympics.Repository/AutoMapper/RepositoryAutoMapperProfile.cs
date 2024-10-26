using AutoMapper;
using Olympics.DataAccess.Entities;
using Olympics.Repository.DAO;

namespace Olympics.Repository.AutoMapper;

public class RepositoryAutoMapperProfile: Profile
{
    public RepositoryAutoMapperProfile()
    {
        CreateMap<CountryData, GetCountryResponse>()
            .ReverseMap();
    }
}