using System.Xml.Linq;
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
        var books = await _bookRepository.ReadBooks();

        if (books.All(x => x.Id != book.Id))
        {
            throw new Exception("Book not found");
        }
        
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

    public async Task<Book> EditBook(Guid bookId)
    {
        var books = await ReadBooks();
        
        var book = books.FirstOrDefault(x => x.Id == bookId);
        
        try
        {
            var xml = XDocument.Parse(book!.ContentXml);
            return new Book(
                book.Id, 
                book.Title,
                book.Author,
                book.Published,
                book.Genre,
                book.Description,
                xml.ToString());
        }
        catch
        {
            throw new Exception("invalid xml");
        }
    }

    public async Task<List<Book>> GetBooksByTitle(string title)
    {
        var bookEntities = await _bookRepository.GetBooksByTitle(title);
        
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
}