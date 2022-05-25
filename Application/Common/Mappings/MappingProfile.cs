using Application.DTO;
using AutoMapper;
using ConsumeApi.Model;
using Domain.Entites;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<MovieDto, MovieDom>().ReverseMap();
            CreateMap<Movie,MovieDom>().ReverseMap();
            CreateMap<UserDto,User>().ReverseMap();
            
        }
    }
}