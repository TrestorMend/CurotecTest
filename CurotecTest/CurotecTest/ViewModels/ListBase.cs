namespace CurotecTest.ViewModels
{
    public class ListBase
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string? OrderByProperty { get; set; }
        public bool OrderByDesc { get; set; }
    }
}
