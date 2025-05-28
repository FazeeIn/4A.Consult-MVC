using Project.Core.Models;
using Project.Database.Abstractions;
using Project.Database.Entities;
using Project.Services.Abstractions;

namespace Project.Services.Services;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task CreateBook(Book book)
    {
        var bookEntity = new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Published = book.Published,
            Genre = book.Genre,
            Description = book.Description,
            ContentXml = book.ContentXml
        };
            

        await _bookRepository.CreateBook(bookEntity);
    }

    public async Task<IReadOnlyList<Book>> ReadBooks()
    {
        var bookEntities = await _bookRepository.ReadBooks();
        
        return bookEntities.Select(x => new Book(
            x.Id, 
            x.Title,
            x.Author,
            x.Published,
            x.Genre,
            x.Description,
            x.ContentXml))
            .ToList();
    }

    public async Task UpdateBook(Book book)
    {
        var bookEntity = new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Published = book.Published,
            Genre = book.Genre,
            Description = book.Description,
            ContentXml = book.ContentXml
        };
        
        await _bookRepository.UpdateBook(bookEntity);
    }

    public async Task DeleteBook(Guid bookId)
    {
        await _bookRepository.DeleteBook(bookId);
    }
}