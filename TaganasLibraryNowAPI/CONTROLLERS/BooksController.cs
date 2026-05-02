using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using TaganasLibraryNowAPI.MODELS;

namespace TaganasLibraryNowAPI.CONTROLERS
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                ID = 1,
                Title = "Spider-Man: Kraven's Last Hunt ",
                Author = "J.M. DeMatteis",
                Genre = "Superhero",
                Available = true,
                PublishedYear = 1987
            },
            new Book
            {
                ID = 2,
                Title = "The Walking Dead: Deluxe",
                Author = "Robert Kirkman",
                Genre = "Post-Apocalyptic",
                Available = true,
                PublishedYear = 2020
            },
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success ",
                data = books,
                message = "Books Retrieved."
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.ID == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found"
                });
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Book Retrieved"
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.ID = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.ID },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book Created."
                });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.ID == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not Found."
                });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book updated."
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
            
        {
            var book = books.FirstOrDefault(x => x.ID == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not Found."
                });

            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book deleted."
            });
        }
    }
}