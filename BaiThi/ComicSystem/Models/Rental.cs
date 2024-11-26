public class Rental
{
    public int RentalID { get; set; }
    public int CustomerID { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; }

    // Relationships
    public Customer Customer { get; set; }
    public ICollection<RentalDetail> RentalDetails { get; set; }
}