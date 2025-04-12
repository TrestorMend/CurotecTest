using Application.Services.Authors.DTO.Request;
using Application.Services.Authors.DTO.Response;
using Domain.Entities;

namespace Application.Services.Authors
{
    public interface IAuthorService
    {
        public Task<Author> Add(AuthorRequestDTO dto);
        public Task<Author?> Update(AuthorRequestDTO dto);
        public Task<AuthorResponseDTO> GetById(int entityId);
        public Task<ListAuthorResponseDTO> Get(ListAuthorRequestDTO request);
        public Task Delete(int entityId);
    }
}
