using System.ComponentModel.DataAnnotations;

namespace baithi.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PricePerDay { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
