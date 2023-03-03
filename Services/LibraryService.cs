using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using project.Data;
using project.Models;

namespace project.Services;

public interface ILibraryService
{
    public List<Member> GetAllMembers();
    public List<Book> GetAllBooks();
    public List<Checkout> GetAllCheckouts();
    public List<Book> GetBooks(string searchTerms);
    public List<Member> GetMembers(string searchTerms);
    public Member GetMember(int memberId);
    public bool CheckoutBookForMember(int memberId, int bookId);
    public bool CheckinBook(int bookId);
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
        return _context.Members
            .Include(member => member.Checkouts)
                .ThenInclude(co => co.Book)
            .ToList();
    }

    public List<Book> GetAllBooks()
    {
        return _context.Books
            .Include(book => book.Checkout.Member)
            .ToList();
    }

    public List<Checkout> GetAllCheckouts()
    {
        return _context.Checkouts
            .Include(co => co.Member)
            .Include(co => co.Book)
            .ToList();
    }

    public Member GetMember(int memberId)
    {
        return _context.Members
            .Where(member => member.Id == memberId)
            .Include(member => member.Checkouts)
                .ThenInclude(co => co.Book)
            .SingleOrDefault();
    }

    public List<Book> GetBooks(string searchTerms)
    {
        string terms = searchTerms.ToLower();

        return _context.Books
            .Include(book => book.Checkout)
            .Where(book =>
                book.Name.ToLower().Contains(terms) ||
                book.Author.ToLower().Contains(terms) ||
                book.Year.ToString().Contains(terms)
            ).ToList();
    }

    public List<Member> GetMembers(string searchTerms)
    {
        string terms = searchTerms.ToLower();

        return _context.Members
            .Where(member => member.Name.ToLower().Contains(terms))
            .ToList();
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

        _context.Checkouts.Add(new Checkout { MemberId = memberId, BookId = bookId });
        _context.SaveChanges();

        return true;
    }

    public bool CheckinBook(int bookId)
    {
        if (!_context.Books.Any(b => b.Id == bookId))
            return false;

        Checkout checkout = _context.Checkouts.SingleOrDefault(co => co.BookId == bookId);
        if (checkout == null)
            return false;

        _context.Checkouts.Remove(checkout);
        _context.SaveChanges();

        return true;
    }
}