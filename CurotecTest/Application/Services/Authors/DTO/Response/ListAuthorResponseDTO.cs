namespace Application.Services.Authors.DTO.Response
{
    public class ListAuthorResponseDTO : ListDTOResponseBase<AuthorResponseDTO>
    {
        public ListAuthorResponseDTO(int totalCount, int pageSize, int pageIndex) : base(totalCount, pageSize, pageIndex)
        {
        }
    }
}
