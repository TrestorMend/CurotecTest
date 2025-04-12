namespace Application.Services
{
    public abstract class ListDTOResponseBase<T> where T : class
    {
        public ListDTOResponseBase(int totalCount, int pageSize, int pageIndex)
        {
            TotalCount = totalCount;
            PageCount = (int)Math.Ceiling((decimal)totalCount / pageSize);
            PageIndex = pageIndex;

            Items = new List<T>();
        }

        public int PageCount { get; private set; }
        public long PageIndex { get; private set; }
        public long TotalCount { get; private set; }
        public List<T> Items { get; set; }
    }
}
