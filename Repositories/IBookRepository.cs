using JwtAuthorization.Models;

namespace JwtAuthorization.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> AddBook(Book book);
    }
}
