using AutoMapper;
using Olympics.Business.DTO;
using Olympics.Repository.DAO;

namespace Olympics.Business.AutoMapper;

public class BusinessAutoMapperProfile : Profile
{
    public BusinessAutoMapperProfile()
    {
        CreateMap<DtoNewCountryRequest, CountryDao>()
            .ReverseMap();
        CreateMap<DtoOlympicResultRequest, OlympicResultDao>()
            .ReverseMap();
    }
}