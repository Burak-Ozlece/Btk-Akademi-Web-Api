using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
            ApplicationContext.Books.Add(book);
            return Ok(book);

        }

        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); 
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")] int id)
        {
            var entity = ApplicationContext.Books.Find(x=>x.Id.Equals(id));
            if(entity is null) 
                return NotFound();
            ApplicationContext.Books.Remove(entity);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartialUpdateOneBook([FromRoute(Name ="id")]int id, [FromBody] JsonPatchDocument<Book> bookPath)
        {
            var entity = ApplicationContext.Books.Find(x => x.Id.Equals(id));
            if (entity is null)
            {
                return NotFound();
            }
            bookPath.ApplyTo(entity);
            return NoContent();
        }
    }
}
