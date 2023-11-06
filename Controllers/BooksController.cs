using JwtAuthorization.Models;
using JwtAuthorization.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            IEnumerable<Book> books = await _bookRepository.GetBooks();
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(BookVM b)
        {
            await _bookRepository.AddBook(new Book()
            {
                Title = b.Title,
                Author = b.Author,
            });

            return Ok(true);
        }
    }
}
