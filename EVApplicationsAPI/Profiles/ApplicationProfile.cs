using AutoMapper;

namespace EVApplicationAPI.Profiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Entities.Application, Models.UserApplicationDto>();
        CreateMap<Models.UserApplicationCreationDto, Entities.Application>();
        CreateMap<Models.UserApplicationUpdateDto, Entities.Application>();
        CreateMap<Entities.Application, Models.UserApplicationUpdateDto>();
    }
}