using Project.Core.Models;

namespace Project.Services.Abstractions;

public interface IBookService
{
    public Task CreateBook(Book book);
    public Task<IReadOnlyList<Book>> ReadBooks();
    public Task UpdateBook(Book book);
    public Task DeleteBook(Guid bookId);
    
}