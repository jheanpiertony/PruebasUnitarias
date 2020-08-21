using AutoMapper;
using Common.Dtos;
using Common.Entities;

namespace Common.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<ActorCreacionDto, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorEdicionDto, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore()).ReverseMap();
            RecognizePostfixes("File");
            CreateMap<ActorPatchDto, Actor>().ReverseMap();
        }
    }
}
