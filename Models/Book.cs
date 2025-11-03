using System.ComponentModel.DataAnnotations;

namespace MvcBookshelf.Models
{
    public class Book
    {
        public int ID { get; set; } // becomes primary key column of db table

        [StringLength(60, MinimumLength = 2), Required]

        public string Title { get; set; }
        [StringLength(60, MinimumLength = 2), Required]
        public string Author { get; set; }
        public string Genre { get; set; }

        // [RegularExpression([0 - 5])]
        public int Rating { get; set; }

        public int Pages { get; set; }
    }
}