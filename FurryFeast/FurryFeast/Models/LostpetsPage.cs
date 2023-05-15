using FurryFeast.Models;

public class LostpetsPage
{
    public List<Lostpets> Pets { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
