using DTO.Models;

namespace BookLending.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookAsync(int id);          // Retrieve a book by its ID
        Task<IEnumerable<Book>> GetAllBooksAsync();  // Retrieve all books
        Task AddBookAsync(Book book);             // Add a new book
        Task<bool> UpdateBookAsync(Book book);    // Update an existing book
        Task<bool> DeleteBookAsync(int id);       // Delete a book by its ID
    }

}
