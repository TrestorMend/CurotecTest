namespace CurotecTest.ViewModels.Book
{
    public class BookGetViewModel : ListBase
    {
        public string? Title { get; set; }
        public int? YearPublished { get; set; }
        public int? AuthorId { get; set; }
    }
}
