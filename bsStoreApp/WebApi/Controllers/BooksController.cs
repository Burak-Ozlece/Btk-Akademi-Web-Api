﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetlAllBooks()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")]int id) 
        {
            try
            {
                var book = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
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
                _context.Books.Add(book);
                _context.SaveChanges();
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
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
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
                _context.SaveChanges();
                return Ok(entity);
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
                var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
                if (entity is null)
                {
                    return NotFound(new {statusCode = 404, message=$"Book with id:{id} could not found."});
                }
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
            var entity = _context.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();
            if (entity is null)
            {
                return NotFound();
            }
            bookPath.ApplyTo(entity);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
