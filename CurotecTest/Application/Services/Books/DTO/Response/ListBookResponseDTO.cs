namespace Application.Services.Books.DTO.Response
{
    public class ListBookResponseDTO : ListDTOResponseBase<BookResponseDTO>
    {
        public ListBookResponseDTO(int totalCount, int pageSize, int pageIndex) : base(totalCount, pageSize, pageIndex)
        {
        }
    }
}
