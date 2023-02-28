namespace project.Models;

public class Checkout
{
    public int MemberId { get; set; }
    public int BookId { get; set; }

    public Member Member { get; set; }
    public Book Book { get; set; }
}
