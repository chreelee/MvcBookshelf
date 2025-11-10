using System.ComponentModel.DataAnnotations;

namespace MvcBookshelf.Models
{
    public class Book
    {
        public int ID { get; set; } // becomes primary key column of db table
        // ErrorMessage validation message https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-9.0#error-messages
        [StringLength(60, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 2), Required]

        public required string Title { get; set; } // made required to avoid visual studio errors
        [StringLength(60, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 2), Required]
        public required string Author { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")] // genre limitations, starts with capital and only has letters
        public required string Genre { get; set; }

        // can use simple Range from Microsoft tutorial, decided range of 1 - 5 instead of 0 - 5
        // https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/validation?view=aspnetcore-9.0
        [Range(1, 5)]
        public int Rating { get; set; }

        public int Pages { get; set; }
    }
}