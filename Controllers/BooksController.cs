using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBookshelf.Data;
using MvcBookshelf.Models;
using MvcBookshelf.Models.StatisticsViewModels;
using Microsoft.Extensions.Logging;
using MvcBookshelf.Services;

namespace MvcBookshelf.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookshelfService _books;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookshelfService context, ILogger<BooksController> logger)
        {
            _books = context;
            _logger = logger;
        }

        // GET: Books

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            IEnumerable<Book> all = await _books.GetAllAsync();
            IEnumerable<Book> books = all.AsQueryable(); // queryable for return


            // https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-9.0
            // EF Core with MVC sorting
            ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["RatingSort"] = sortOrder == "Rating" ? "rating_desc" : "Rating";
            ViewData["AuthorSort"] = sortOrder == "Author" ? "author_desc" : "Author";
            ViewData["GenreSort"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["PagesSort"] = sortOrder == "Pages" ? "pages_desc" : "Pages";

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            //var books = from b in _books
            //            select b;


            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => 
                                s.Title.ToUpper().Contains(searchString, StringComparison.OrdinalIgnoreCase) || 
                                s.Author.ToUpper().Contains(searchString, StringComparison.OrdinalIgnoreCase));
                // adjust to lecture example using ToUpper and contains and StringComparision for any case
            }

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    _logger.LogInformation("Sorting by title descending"); // suggested logging
                    break;
                case "Author":
                    books = books.OrderBy(b => b.Author);
                    _logger.LogInformation("Sorting by author ascending");
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    _logger.LogInformation("Sorting by author descending");
                    break;
                case "Genre":
                    books = books.OrderBy(b => b.Genre);
                    _logger.LogInformation("Sorting by genre ascending");
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(b => b.Genre);
                    _logger.LogInformation("Sorting by genre descending");
                    break;
                case "Rating":
                    books = books.OrderBy(b => b.Rating);
                    _logger.LogInformation("Sorting by rating ascending");
                    break;
                case "rating_desc":
                    books = books.OrderByDescending(b => b.Rating);
                    _logger.LogInformation("Sorting by rating descending");
                    break;
                case "Pages":
                    books = books.OrderBy(b => b.Pages);
                    _logger.LogInformation("Sorting by pages ascending");
                    break;
                case "pages_desc":
                    books = books.OrderByDescending(b => b.Pages);
                    _logger.LogInformation("Sorting by pages descending");
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            return View(books.ToList());
        }

        public async Task<IActionResult> Statistics()
        {
            var statsViewModel = new StatisticsViewModel();

            statsViewModel.TotalBooks = await _books.GetTotalBooksAsync(); // use IBookshelfService

            statsViewModel.TotalAuthors = await _books.GetTotalAuthorsAsync();

            statsViewModel.TotalGenres = await _books.GetTotalGenresAsync();

            statsViewModel.TotalPages = await _books.GetTotalPagesAsync();

            return View(statsViewModel);
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var book = await _books.GetByIdAsync(id);
            //.AsNoTracking()
            //.FirstOrDefaultAsync(m => m.ID == id);
            _logger.LogInformation("Fetching details for book ID {BookID}", id);
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            _logger.LogInformation("CREATE GET");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Author,Genre,Rating,Pages")] Book book)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("CREATE POST model invalid");
                return View(book);
            }
            await _books.AddAsync(book);
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var book = await _books.GetByIdAsync(id);
            _logger.LogInformation("EDIT GET book id");

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Author,Genre,Rating,Pages")] Book book)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("EDIT POST model is invalid {BookID}", id);
                return View(book);
            }

            await _books.UpdateAsync(book);
            return RedirectToAction(nameof(Index));

        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _books.GetByIdAsync(id);
            _logger.LogInformation("DELETE GET book id {BookID}", id);

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _books.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool BookExists(int id)
        //{
        //    return _context.Book.Any(e => e.ID == id);
        //}
    }
}
