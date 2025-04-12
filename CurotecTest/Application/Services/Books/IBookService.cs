using Application.Services.Books.DTO.Request;
using Application.Services.Books.DTO.Response;
using Domain.Entities;

namespace Application.Services.Books
{
    public interface IBookService
    {
        public Task<Book> Add(BookRequestDTO dto);
        public Task<Book?> Update(BookRequestDTO dto);
        public Task<BookResponseDTO> GetById(int entityId);
        public Task<ListBookResponseDTO> Get(ListBookRequestDTO request);
        public Task Delete(int entityId);
    }
}
