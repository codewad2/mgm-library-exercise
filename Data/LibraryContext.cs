using project.Models;
using Microsoft.EntityFrameworkCore;

namespace project.Data;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    public DbSet<Member> Members { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().ToTable("members");
        modelBuilder.Entity<Book>().ToTable("books");
        modelBuilder.Entity<Checkout>().ToTable("checkouts")
            .HasKey(nameof(Checkout.MemberId), nameof(Checkout.BookId));
    }
}
