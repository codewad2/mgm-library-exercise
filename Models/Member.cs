namespace project.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Checkout> Checkouts { get; set; }
}
