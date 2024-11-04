using BookLending.Interfaces;
using DTO.Models;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book> GetBookAsync(int id)
    {
        return await _bookRepository.GetBookByIdAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllBooksAsync();
    }

    public async Task AddBookAsync(Book book)
    {
        await _bookRepository.AddBookAsync(book);
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        var existingBook = await _bookRepository.GetBookByIdAsync(book.BookId);
        if (existingBook == null) return false;

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Genre = book.Genre;
        existingBook.Condition = book.Condition;
        existingBook.IsAvailable = book.IsAvailable;

        await _bookRepository.UpdateBookAsync(existingBook);
        return true;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null) return false;

        await _bookRepository.DeleteBookAsync(id);
        return true;
    }
}
