using AutoMapper;
using practiceApi.Models;
using practiceApi.Models.Dto;

namespace practiceApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa,VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
        }
    }
}
