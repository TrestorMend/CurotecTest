namespace CurotecTest.ViewModels.Book.Base
{
    public class BaseBookViewModel
    {
        public required string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
    }
}
