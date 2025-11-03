using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcBookshelf.Data;
using MvcBookshelf.Models;
using System;
using System.Linq;

namespace MvcBookshelf.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcBookshelfContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcBookshelfContext>>()))
        {
            // Look for any books
            if (context.Book.Any())
            {
                return; // db has been seeded
            }
            context.Book.AddRange(
                new Book { Title = "The Final Empire", Author = "Brandon Sanderson", Genre = "Fantasy", Rating = 5, Pages = 647 },
                new Book { Title = "Be Ready When the Luck Happens", Author = "Ina Garten", Genre = "Biography", Rating = 5, Pages = 320 },
                new Book { Title = "Yellowface", Author = "R.F Kuang", Genre = "Fiction", Rating = 1, Pages = 336 },
                new Book { Title = "The Well of Ascension", Author = "Brandon Sanderson", Genre = "Fantasy", Rating = 4, Pages = 781 },
                new Book { Title = "Anima Rising", Author = "Christopher Moore", Genre = "Fantasy", Rating = 5, Pages = 400 },
                new Book { Title = "By Any Other Name", Author = "Jodi Picoult", Genre = "Historical Fiction", Rating = 4, Pages = 544 },
                new Book { Title = "The Last Wish", Author = "Andrzej Sapkowski", Genre = "Fantasy", Rating = 5, Pages = 400 },
                new Book { Title = "Hawaii's Story by Hawaii's Queen", Author = "Lili'uokalani", Genre = "Biography", Rating = 5, Pages = 464 },
                new Book { Title = "The Water Dancer", Author = "Ta-Nehisi Coates", Genre = "Historical Fiction", Rating = 4, Pages = 416 },
                new Book { Title = "Outlander", Author = "Diana Gabaldon", Genre = "Fantasy", Rating = 4, Pages = 850 },
                new Book { Title = "Dragonfly in Amber", Author = "Diana Gabaldon", Genre = "Fantasy", Rating = 4, Pages = 752 },
                new Book { Title = "Voyager", Author = "Diana Gabaldon", Genre = "Fantasy", Rating = 4, Pages = 870 },
                new Book { Title = "Killers of the Flower Moon", Author = "David Grann", Genre = "Nonfiction", Rating = 5, Pages = 352 }
            );
            context.SaveChanges();
        }
    }
}
