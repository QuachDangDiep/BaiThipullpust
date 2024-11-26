public class Customer
{
    public int CustomerID { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }

    // Relationships
    public ICollection<Rental> Rentals { get; set; }
}
