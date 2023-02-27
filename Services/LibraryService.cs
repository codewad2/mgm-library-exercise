using project.Data;
using project.Models;

namespace project.Services;

public interface ILibraryService
{
    public List<Member> GetAllMembers();
    public List<Book> GetBooks(string? searchTerms);
}

public class LibraryService : ILibraryService
{
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
}