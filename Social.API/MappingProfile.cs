using AutoMapper;
using Social.API.Dtos;
using Social.API.Models.Fake;

public class MappingProfile : Profile 
{
    public MappingProfile() {
        // Add as many of these lines as you need to map your objects
        CreateMap<Fake, FakesForListDto>();
        CreateMap<FakesForListDto, Fake>();
    }
}