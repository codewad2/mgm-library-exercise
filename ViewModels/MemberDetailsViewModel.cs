using project.Models;

namespace project.ViewModels;

public class MemberDetailsViewModel
{
    public Member Member { get; set; }
    public IEnumerable<Book> Books { get; set; }
}