using project.Models;

namespace project.ViewModels;

public class HomeViewModel
{
    public List<Member> Members { get; set; }
    public List<Book> Books { get; set; }
}