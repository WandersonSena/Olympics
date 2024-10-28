using AutoMapper;
using Olympics.Business.DTO;
using Olympics.WebApi.ViewModels;

namespace Olympics.WebApi.AutoMapper;

public class WebApiAutoMapperProfile : Profile
{
    public WebApiAutoMapperProfile()
    {
        CreateMap<NewOlympicResultViewModel, DtoOlympicResultRequest>()
            .ReverseMap();
    }
}