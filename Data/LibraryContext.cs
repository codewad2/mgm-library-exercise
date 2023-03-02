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
        modelBuilder.Entity<Member>().ToTable("members")
            .HasMany(member => member.Checkouts)
            .WithOne(checkout => checkout.Member);
        modelBuilder.Entity<Book>().ToTable("books")
            .HasOne(book => book.Checkout)
            .WithOne(checkout => checkout.Book);
        modelBuilder.Entity<Checkout>().ToTable("checkouts")
            .HasKey(nameof(Checkout.MemberId), nameof(Checkout.BookId));
    }
}
