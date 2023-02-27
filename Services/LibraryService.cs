using project.Data;
using project.Models;

namespace project.Services;

public interface ILibraryService
{
    public List<Member> GetAllMembers();
    public List<Book> GetBooks(string? searchTerms);
    public bool CheckoutBookForMember(int memberId, int bookId);
}

public class LibraryService : ILibraryService
{
    public const int CHECKOUT_LIMIT = 5;
    private readonly LibraryContext _context;

    public LibraryService(LibraryContext context)
    {
        _context = context;
    }

    public List<Member> GetAllMembers()
    {
        return _context.Members.ToList();
    }

    public List<Book> GetBooks(string? searchTerms)
    {
        if (searchTerms == null)
            return _context.Books.ToList();

        return _context.Books
            .Where(book =>  book.Name.Contains(searchTerms) ||
                            book.Author.Contains(searchTerms) ||
                            book.Year.ToString().Contains(searchTerms)
            ).ToList();
    }

    public bool CheckoutBookForMember(int memberId, int bookId)
    {
        if (!_context.Members.Any(m => m.Id == memberId))
            return false;

        if (!_context.Books.Any(b => b.Id == bookId))
            return false;

        if (_context.Checkouts.Where(co => co.MemberId == memberId).Count() >= CHECKOUT_LIMIT)
            return false;

        if (_context.Checkouts.Any(co => co.BookId == bookId))
            return false;

        Checkout newCheckout = new Checkout { MemberId = memberId, BookId = bookId };
        _context.Checkouts.Add(newCheckout);
        _context.SaveChanges();

        return true;
    }
}