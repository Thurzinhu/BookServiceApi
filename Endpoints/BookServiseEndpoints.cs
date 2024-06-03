using BookService.Contracts;
using BookService.Mapping;
using BookService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookService.Endpoints
{
    public static class BookServiseEndpoints
    {
        public static void MapBooksEndpoints(this WebApplication app)
        {
            var booksGroup = app.MapGroup("/books");

            booksGroup.MapGet("/", (BookServiceContext db) =>
                db.Books
                .Include(b => b.Author)
                .Select(b => b.ToBookDetailDto())
            );

            booksGroup.MapGet("/{id}", (BookServiceContext db, int id) =>
            {
                var book = db.Books.Find(id);

                if (book is null)
                    return Results.NotFound();

                book.Author = db.Authors.Find(book.AuthorId);
                return Results.Ok(book.ToBookDetailDto());
            });

            booksGroup.MapPost("/", (BookServiceContext db, CreateBook book) =>
            {
                var author = db.Authors.Find(book.AuthorId);

                if (author is null)
                    return Results.BadRequest("Author not found");

                if (db.Books.Any(b => b.Title == book.Title))
                    return Results.BadRequest("Book with this title already exists");

                var newBook = book.ToEntity();
                newBook.Author = author;

                db.Books.Add(newBook);
                db.SaveChanges();

                return Results.Created($"/books/{newBook.Id}", newBook.ToBookDetailDto());
            });

            booksGroup.MapPut("/{id}", (BookServiceContext db, int id, UpdateBook book) =>
            {
                var bookToUpdate = db.Books.Find(id);

                if (bookToUpdate is null)
                    return Results.NotFound();

                db.Entry(bookToUpdate)
                    .CurrentValues
                    .SetValues(book.ToEntity(id));

                db.SaveChanges();

                return Results.NoContent();
            });

            booksGroup.MapDelete("/{id}", (BookServiceContext db, int id) =>
            {
                var book = db.Books.Find(id);

                if (book is null)
                    return Results.NotFound();

                db.Books.Remove(book);
                db.SaveChanges();

                return Results.NoContent();
            });

        }

        public static void MapAuthorsEndpoints(this WebApplication app)
        {
            var authorsGroup = app.MapGroup("/authors");

            authorsGroup.MapGet("/", (BookServiceContext db) =>
               db.Authors
            );

            authorsGroup.MapGet("/{id}", (BookServiceContext db, int id) =>
            {
                var author = db.Authors.Find(id);

                if (author is null)
                    return Results.NotFound();

                return Results.Ok(author);
            });

            authorsGroup.MapPost("/", (BookServiceContext db, Author author) =>
            {
                if (db.Authors.Any(a => a.Name == author.Name))
                    return Results.BadRequest("Author with this name already exists");

                db.Authors.Add(author);
                db.SaveChanges();

                var createdAuthor = new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name
                };

                return Results.Created($"/authors/{author.Id}", createdAuthor);
            });

            authorsGroup.MapPut("/{id}", (BookServiceContext db, int id, Author updatedAuthor) =>
            {
                var author = db.Authors.Find(id);

                if (author is null)
                    return Results.NotFound();

                author.Name = updatedAuthor.Name;
                db.SaveChanges();

                return Results.NoContent();
            });

            authorsGroup.MapDelete("/{id}", (BookServiceContext db, int id) =>
            {
                var author = db.Authors.Find(id);

                if (author is null)
                    return Results.NotFound();

                db.Authors.Remove(author);
                db.SaveChanges();

                return Results.NoContent();
            });
        }
    }
}
