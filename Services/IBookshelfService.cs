using System.Collections.Generic;
using System.Threading.Tasks;
using MvcBookshelf.Models;

namespace MvcBookshelf.Services
{
    public interface IBookshelfService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);

        // move statistics methods from BookController here as a task, and add to Bookshelfservice
        Task<int> GetTotalBooksAsync();
        Task<int> GetTotalAuthorsAsync();
        Task<int> GetTotalGenresAsync();
        Task<int> GetTotalPagesAsync();

    }
}
