using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcBookshelf.Models;

namespace MvcBookshelf.Data
{
    public class MvcBookshelfContext : DbContext
    {
        public MvcBookshelfContext (DbContextOptions<MvcBookshelfContext> options)
            : base(options)
        {
        }

        public DbSet<MvcBookshelf.Models.Book> Book { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
        }
    }
}
