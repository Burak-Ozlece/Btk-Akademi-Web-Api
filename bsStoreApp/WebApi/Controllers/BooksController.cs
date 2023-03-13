using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EfCore;

namespace WebApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")]int id)
        {
            try
            {
                var book = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

                if (book is null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book books)
        {
            try
            {

                if (books is null)
                    return NotFound();
                _context.Books.Add(books);
                _context.SaveChanges();
                return StatusCode(201,books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book books)
        {
            try
            {
                var entity = _context.Books.Where(i => i.Id.Equals(id)).SingleOrDefault();
                if (entity is null)
                    return NotFound();

                if (id != books.Id)
                    return BadRequest();
                entity.Title = books.Title;
                entity.Price = books.Price;
                _context.SaveChanges();
                return Ok(entity);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
                if (entity is null)
                    return NotFound();
                _context.Books.Remove(entity);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
    }

        [HttpPatch("{id:int}")]
        public IActionResult PartialUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPath)
        {
            try
            {
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
                if (entity is null)
                {
                    return NotFound();
                }
                bookPath.ApplyTo(entity);
                _context.SaveChanges();
                return NoContent();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
