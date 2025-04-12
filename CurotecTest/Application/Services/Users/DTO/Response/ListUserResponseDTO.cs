namespace Application.Services.Users.DTO.Response
{
    public class ListUserResponseDTO : ListDTOResponseBase<UserResponseDTO>
    {
        public ListUserResponseDTO(int totalCount, int pageSize, int pageIndex) : base(totalCount, pageSize, pageIndex)
        {
        }
    }
}
