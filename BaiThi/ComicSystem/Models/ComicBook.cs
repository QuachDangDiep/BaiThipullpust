public class ComicBook
{
    public int ComicBookID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal PricePerDay { get; set; }

    // Relationships
    public ICollection<RentalDetail> RentalDetails { get; set; }
}
