namespace baithi.Models
{
    public class RentalDetail
    {
        public int RentalDetailId { get; set; }
        public int RentalId { get; set; }
        public int ComicBookId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
    }
}