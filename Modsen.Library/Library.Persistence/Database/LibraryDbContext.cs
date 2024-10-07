using Microsoft.EntityFrameworkCore;
using Library.Domain.Data;
namespace Library.Persistence.Database
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> books { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<UserBook> userBooks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //
            //User -> Book 1:M
            modelBuilder.Entity<UserBook>()
           .HasKey(ub => new { ub.UserId, ub.BookId });
        }
    }
}
