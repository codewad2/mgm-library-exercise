using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project.Models;

public class Checkout
{
    [Column("member_id")]
    public int MemberId { get; set; }
    [Column("book_id")]
    public int BookId { get; set; }

    public Member Member { get; set; }
    public Book Book { get; set; }
}
