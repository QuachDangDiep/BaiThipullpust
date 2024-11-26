using System;
using System.ComponentModel.DataAnnotations;

namespace baithi.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
