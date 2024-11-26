using baithi.Models;
using System.Collections.Generic;

namespace baithi.Data
{
    public class ComicSystemMemoryDbContext
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<ComicBook> ComicBooks { get; set; } = new List<ComicBook>();
        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
