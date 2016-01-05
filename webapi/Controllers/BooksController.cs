using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using webapi.Models;

namespace webapi.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private BookContext db = new BookContext();

        private static readonly Expression<Func<Book, BookDto>> AsBookDto = m => new BookDto { Author = m.Author.Name, Genre = m.Genre, Title = m.Title };

        // GET: api/Books
        [Route("")]
        public IQueryable<BookDto> GetBooks()
        {
            return db.Books.Include(b => b.Author).Select(AsBookDto);
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        [Route("{id:int}")]
        public IHttpActionResult GetBook(int id)
        {
            BookDto book = db.Books.Include(b => b.Author).Where(b => b.BookId == id).Select(AsBookDto).FirstOrDefault();
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }

        [Route("{id:int}/details")]
        [ResponseType(typeof(BookDetailDto))]
        public async Task<IHttpActionResult> GetBookDetail(int id)
        {
            var bookdetails = await (from b in db.Books.Include(b => b.Author)
                                     where b.BookId == id
                                     select new BookDetailDto
                                     {
                                         Title = b.Title,
                                         Genre = b.Genre,
                                         PublishDate = b.PublishDate,
                                         Price = b.Price,
                                         Description = b.Description,
                                         Author = b.Author.Name
                                     }
            ).FirstOrDefaultAsync();
            if (bookdetails == null)
            {
                return NotFound();
            }
            return Ok(bookdetails);
        }

        [Route("{genre}")]
        public IQueryable<BookDto> GetBooksByGenre(string genre)
        {
            int age = 22;
            return db.Books.Include(b => b.Author).Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).Select(AsBookDto);
        }

        [Route("~api/authors/{authorId}/books")]
        public IQueryable<BookDto> GetBooksByAuthor(int authorId)
        {
            return db.Books.Include(b => b.Author).Where(b => b.AuthorId.Equals(authorId)).Select(AsBookDto);
        }
        [Route("date/{pubdate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public IQueryable<BookDto> GetBooks(DateTime pubdate)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => DbFunctions.TruncateTime(b.PublishDate)
                    == DbFunctions.TruncateTime(pubdate))
                .Select(AsBookDto);
        }
    }
}