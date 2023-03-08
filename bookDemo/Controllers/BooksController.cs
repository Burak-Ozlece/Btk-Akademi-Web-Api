using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name ="id")] int id)
        {
            var book = ApplicationContext.Books.Where(i => i.Id.Equals(id)).SingleOrDefault();
            if (book is null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book book)
        {
            try
            {
                if(book is null)
                    return BadRequest();
                ApplicationContext.Books.Add(book);
                return StatusCode(201);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")] int id, [FromBody] Book book)
        {
            var entity = ApplicationContext.Books.Find(i => i.Id.Equals(id));
            if (entity is null)
                return NotFound();

            if (id != book.Id)
                return BadRequest();   

            ApplicationContext.Books.Remove(entity);

            book.Id = entity.Id;
            ApplicationContext.Books.Add(entity);
            return Ok(book);

        }
    }
}
