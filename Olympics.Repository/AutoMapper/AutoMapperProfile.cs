using AutoMapper;
using Olympics.DataAccess.Entities;
using Olympics.Repository.DAO;

namespace Olympics.Repository.AutoMapper;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CountryData, GetCountryResponse>()
            .ReverseMap();
    }
}