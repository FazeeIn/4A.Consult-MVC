namespace Project.Services.Services;

public interface IBookService
{
    Task CreateBook();
    Task ReadBook();
    Task UpdateBook();
    Task DeleteBook();
    
}