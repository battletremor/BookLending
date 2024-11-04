using DTO.Models;

namespace BookLending.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int id);         // Retrieve a book by its ID
        Task<IEnumerable<Book>> GetAllBooksAsync();  // Retrieve all books
        Task AddBookAsync(Book book);                // Add a new book
        Task UpdateBookAsync(Book book);             // Update an existing book
        Task DeleteBookAsync(int id);                // Delete a book by its ID
    }

}
