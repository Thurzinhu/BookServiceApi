using BookService.Contracts;
using BookService.Models;

namespace BookService.Mapping
{
    public static class BookMapping
    {
        public static BookDetail ToBookDetailDto(this Book book)
        {
            return new BookDetail
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                Genre = book.Genre,
                AuthorName = book.Author.Name
            };
        }

        public static Book ToEntity(this CreateBook book)
        {
            return new Book
            {
                Title = book.Title,
                Year = book.Year,
                Genre = book.Genre,
                AuthorId = book.AuthorId
            };
        }

        public static Book ToEntity(this UpdateBook book, int id)
        {
            return new Book
            {
                Id = id,
                Title = book.Title,
                Year = book.Year,
                Genre = book.Genre,
                AuthorId = book.AuthorId
            };
        }
    }
}
