using Microsoft.AspNetCore.Mvc;
using Project.API.Contracts.Requests;
using Project.Core.Models;
using Project.Core.Validators;
using Project.Services.Abstractions;

namespace Project.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController: Controller
{
    private readonly IBookService _bookService;
    private readonly BookValidator _validator;

    public BookController(IBookService bookService, BookValidator validator)
    {
        _bookService = bookService;
        _validator = validator;
    }
    
    [HttpGet("/Book")]
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.ReadBooks();
        
        return View(books);
    } 
    
    [HttpGet]
    [Route("/Book/[action]")]
    public IActionResult CreateBook()
    {
        return View();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateBook([FromForm] BookRequest request)
    {
        var book = new Book(
            Guid.NewGuid(), 
            request.Title,
            request.Author,
            request.Published,
            request.Genre,
            request.Description,
            request.ContentsXml);

        var validationResult = await _validator.ValidateAsync(book);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return BadRequest();
        }
        
        await _bookService.CreateBook(book);

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> EditBook(Guid bookId)
    {
        var books = await _bookService.ReadBooks();
        
        var book = books.FirstOrDefault(x => x.Id == bookId);
        
        if (book == null)
        {
            return NotFound();
        }
        
        return View(book);
    }
    
    [HttpGet("/[action]")]
    public async Task<IActionResult> ReadBook()
    {
        var books = await _bookService.ReadBooks();

        return Ok(books);
    }
    [HttpPut("/[action]")]
    public async Task<IActionResult> UpdateBook(UpdateBookRequest request)
    {
        var book = new Book(
            request.Id, 
            request.Title,
            request.Author,
            request.Published,
            request.Genre,
            request.Description,
            request.ContentsXml);

        var validationResult = await _validator.ValidateAsync(book);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        await _bookService.UpdateBook(book);
        
        return Ok();
    }
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        await _bookService.DeleteBook(bookId);
        
        return Ok();
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> ViewBook(Guid bookId)
    {
        var books = await _bookService.ReadBooks();
        var book = books.FirstOrDefault(x => x.Id == bookId);
        
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
}