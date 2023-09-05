using PMSOFTBookTestTask.Repository.Entities;
using PMSOFTBookTestTask.Models.Response;
using PMSOFTBookTestTask.Models.Request;
using PMSOFTBookTestTask.Service.Dto;
using AutoMapper;

namespace PMSOFTBookTestTask.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BookEntity, BookDto>();
            CreateMap<BookDto, BookEntity>();

            CreateMap<BookDto, BookResponse>();
            CreateMap<BookResponse, BookDto>();

            CreateMap<BookDto, BookModel>();
            CreateMap<BookModel, BookDto>();


            CreateMap<GenreEntity, GenreDto>();
            CreateMap<GenreDto, GenreEntity>();

            CreateMap<AuthorEntity, AuthorDto>();
            CreateMap<AuthorDto, AuthorEntity>();
        }
    }
}
