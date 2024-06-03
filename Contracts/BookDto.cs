namespace BookService.Contracts
{
    public class CreateBook
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
    }

    public class BookDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string AuthorName { get; set; }
    }

    public class UpdateBook
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }
    }
}
