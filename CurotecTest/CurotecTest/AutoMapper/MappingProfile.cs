using Application.CQRS.Authors.Commands;
using Application.CQRS.Authors.Queries;
using Application.CQRS.Books.Commands;
using Application.CQRS.Books.Queries;
using Application.CQRS.Users.Commands;
using Application.CQRS.Users.Queries;
using Application.Responses;
using Application.Services.Authors.DTO.Request;
using Application.Services.Authors.DTO.Response;
using Application.Services.Books.DTO.Request;
using Application.Services.Books.DTO.Response;
using Application.Services.Login.DTO.Request;
using Application.Services.Users.DTO.Request;
using Application.Services.Users.DTO.Response;
using AutoMapper;
using CurotecTest.Controllers.Base;
using CurotecTest.ViewModels.Auth;
using CurotecTest.ViewModels.Author;
using CurotecTest.ViewModels.Book;
using CurotecTest.ViewModels.User;
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
            CreateMap<LoginRequestViewModel, LoginRequestDTO>();

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


            #region User

            CreateMap<UserInsertViewModel, UserInsertCommand>();
            CreateMap<UserUpdateViewModel, UserUpdateCommand>();
            CreateMap<UserInsertCommand, UserRequestDTO>();
            CreateMap<UserUpdateCommand, UserRequestDTO>();
            CreateMap<UserRequestDTO, User>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserGetViewModel, UserGetQuery>();
            CreateMap<UserGetQuery, ListUserRequestDTO>();

            #endregion

            #region Author

            CreateMap<AuthorInsertViewModel, AuthorInsertCommand>();
            CreateMap<AuthorUpdateViewModel, AuthorUpdateCommand>();
            CreateMap<AuthorInsertCommand, AuthorRequestDTO>();
            CreateMap<AuthorUpdateCommand, AuthorRequestDTO>();
            CreateMap<AuthorRequestDTO, Author>();
            CreateMap<Author, AuthorResponseDTO>();
            CreateMap<AuthorGetViewModel, AuthorGetQuery>();
            CreateMap<AuthorGetQuery, ListAuthorRequestDTO>();

            #endregion
        }
    }
}
