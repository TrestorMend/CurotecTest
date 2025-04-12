namespace Application.Services
{
    public abstract class ListDTOBase
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string? OrderByProperty { get; set; }
        public bool OrderByDesc { get; set; }
    }
}
