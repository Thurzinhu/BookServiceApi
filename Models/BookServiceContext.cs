using Microsoft.EntityFrameworkCore;

namespace BookService.Models
{
    public class BookServiceContext : DbContext 
    {
        public BookServiceContext(DbContextOptions<BookServiceContext> options) :
        base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
