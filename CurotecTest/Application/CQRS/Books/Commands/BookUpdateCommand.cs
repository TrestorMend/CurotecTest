namespace Application.CQRS.Books.Commands
{
    public class BookUpdateCommand : BaseBookCommand
    {
        public int Id { get; set; }
    }
}
