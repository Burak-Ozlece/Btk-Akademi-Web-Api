<<<<<<< HEAD
﻿using Entities.Models;
using Microsoft.AspNetCore.Http;
=======
﻿using Microsoft.AspNetCore.Http;
>>>>>>> 2dde400210ae6ccef9419ace948392c737ce9787
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contacts;
using Repositories.EfCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetlAllBooks()
        {
            var books = _manager.Book.GetAllBooks(false);
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")]int id) 
        {
            try
            {
                var book = _manager.Book.GetOneBookById(id,false);
                if (book is null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book book)
        {
            try
            {
                if (book is null)
                {
                    return BadRequest();
                }
                _manager.Book.UpdateOneBook(book);
                _manager.Save();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromBody]Book book,[FromRoute(Name ="id")]int id) 
        {
            try
            {
                var entity = _manager.Book.GetOneBookById(id,true);
                if (entity is null)
                {
                    return NotFound();
                }
                if (id != book.Id)
                {
                    return BadRequest();
                }
                entity.Title = book.Title;
                entity.Price = book.Price;
                _manager.Save();
                return Ok();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute]int id)
        {
            try
            {
                var entity = _manager.Book.GetOneBookById(id, true);
                if (entity is null)
                {
                    return NotFound(new {statusCode = 404, message=$"Book with id:{id} could not found."});
                }
                _manager.Book.DeleteOneBook(entity);
                _manager.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
<<<<<<< HEAD

        [HttpPatch("{id:int}")]
        public IActionResult PartialUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPath)
        {
            var entity = _manager.Book.GetOneBookById(id, true);
=======
        [HttpPatch("{id:int}")]
        public IActionResult PartialUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPath)
        {
            var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
>>>>>>> 2dde400210ae6ccef9419ace948392c737ce9787
            if (entity is null)
            {
                return NotFound();
            }
            bookPath.ApplyTo(entity);
<<<<<<< HEAD
            _manager.Book.UpdateOneBook(entity);
            _manager.Save();
=======
            _context.SaveChanges();
>>>>>>> 2dde400210ae6ccef9419ace948392c737ce9787
            return NoContent();
        }
    }
}
