using Application.CQRS.Books.Commands;
using Application.CQRS.Books.Queries;
using Application.Responses;
using Application.Services.Books.DTO.Request;
using Application.Services.Books.DTO.Response;
using AutoMapper;
using CurotecTest.Controllers.Base;
using CurotecTest.ViewModels.Book;
using Domain.Entities;

namespace CurotecTest.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ResponseState, RequestResult>()
               .ForMember(x => x.Errors, opt => opt.MapFrom(src => src.Notifications));
            CreateMap<Notification, RequestResultErrorItem>();
            //CreateMap<LoginRequestViewModel, LoginRequestDTO>();

            #region Book

            CreateMap<BookInsertViewModel, BookInsertCommand>();
            CreateMap<BookUpdateViewModel, BookUpdateCommand>();
            CreateMap<BookInsertCommand, BookRequestDTO>();
            CreateMap<BookUpdateCommand, BookRequestDTO>();
            CreateMap<BookRequestDTO, Book>();
            CreateMap<Book, BookResponseDTO>();
            CreateMap<BookGetViewModel, BookGetQuery>();
            CreateMap<BookGetQuery, ListBookRequestDTO>();

            #endregion
        }
    }
}
