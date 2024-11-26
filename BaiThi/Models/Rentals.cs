using System;
using System.Collections.Generic;

namespace baithi.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public List<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
