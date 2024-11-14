using BookLending.Interfaces;
using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace BookLending.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        // Constructor to inject the BookService dependency
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Gets all books currently listed
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBooksListed()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while getting books listing");
                return BadRequest();
            }
        }

        /// <summary>
        /// Get details of a book by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();  // Return 404 if the book doesn't exist
            }
            return Ok(book);  // Return the book details if found
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Validate input data
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre,
                Condition = bookDto.Condition,
                IsAvailable = bookDto.IsAvailable,
                UserId = bookDto.UserId,
                ListedAt = DateTime.UtcNow
            };

            await _bookService.AddBookAsync(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);  // Return 201 Created with the new book's details
        }

        /// <summary>
        /// Update an existing book
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Validate input data
            }

            // Fetch the existing book from the database
            var existingBook = await _bookService.GetBookAsync(id);

            if (existingBook == null)
            {
                return NotFound();  // Return 404 if the book doesn't exist
            }

            // Update the properties of the existing book
            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.Genre = bookDto.Genre;
            existingBook.Condition = bookDto.Condition;
            existingBook.IsAvailable = bookDto.IsAvailable;

            // Call the service to update the book in the database
            var isUpdated = await _bookService.UpdateBookAsync(existingBook);

            if (!isUpdated)
            {
                return BadRequest("Update failed");
            }

            // Return 204 No Content after a successful update
            return NoContent();
        }

        /// <summary>
        /// Delete a book by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // Attempt to delete the book
            var result = await _bookService.DeleteBookAsync(id);

            if (!result)
            {
                return NotFound();  // Return 404 if the book doesn't exist
            }

            // Return 200 OK if the book was successfully deleted
            return Ok(new { Message = "Book deleted successfully" });
        }
    }

    public class BookCreateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Condition { get; set; }
        public bool IsAvailable { get; set; }
        public int UserId { get; set; }
    }

    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Condition { get; set; }
        public bool IsAvailable { get; set; }
    }

}
