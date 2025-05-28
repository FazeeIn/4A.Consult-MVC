using Project.Database.Entities;

namespace Project.Database.Abstractions;

public interface IBookRepository
{
    public Task CreateBook(BookEntity book);
    public Task<IReadOnlyList<BookEntity>> ReadBooks();
    public Task UpdateBook(BookEntity book);
    public Task DeleteBook(Guid bookId);
    public Task<List<BookEntity>> GetBooksByTitle(string title);
}