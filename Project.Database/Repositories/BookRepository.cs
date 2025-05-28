using System.Data;
using Microsoft.Data.SqlClient;
using Project.Database.Abstractions;
using Project.Database.Entities;

namespace Project.Database.Repositories;

public class BookRepository: IBookRepository
{
    private readonly string _connectionString;

    public BookRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task CreateBook(BookEntity book)
    {
        await using var connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();

        await using var command = new SqlCommand("CreateBook", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", book.Id);
        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@Author", book.Author);
        command.Parameters.AddWithValue("@Published", book.Published);
        command.Parameters.AddWithValue("@Genre", book.Genre);
        command.Parameters.AddWithValue("@Description", book.Description);
        command.Parameters.AddWithValue("@ContentXml",book.ContentXml);

        await command.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }


    public async Task<IReadOnlyList<BookEntity>> ReadBooks()
    {
        var books = new List<BookEntity>();

        await using var connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();

        await using var command = new SqlCommand("GetBooks", connection);
        
        command.CommandType = CommandType.StoredProcedure;

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            books.Add(new BookEntity
            {
                Id = (Guid)reader["Id"],
                Title = (string)reader["Title"],
                Author = (string)reader["Author"],
                Published = (DateTime)reader["Published"],
                Genre = (string)reader["Genre"],
                Description = (string)reader["Description"],
                ContentXml = (string)reader["ContentXml"]
            });
        }
        
        return books;
    }


    public async Task UpdateBook(BookEntity book)
    {
        await using var connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();

        await using var command = new SqlCommand("UpdateBook", connection);
        
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", book.Id);
        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@Author", book.Author);
        command.Parameters.AddWithValue("@Published", book.Published);
        command.Parameters.AddWithValue("@Genre", book.Genre);
        command.Parameters.AddWithValue("@Description", book.Description);
        command.Parameters.AddWithValue("@ContentXml", book.ContentXml);

        await command.ExecuteNonQueryAsync();

        await connection.CloseAsync();
    }


    public async Task DeleteBook(Guid bookId)
    {
        await using var connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();

        await using var command = new SqlCommand("DeleteBook", connection);
        
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", bookId);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<BookEntity>> GetBooksByTitle(string title)
    {
        var bookEntities = new List<BookEntity>();

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(@"SELECT TOP 30 * 
            FROM Books 
            WHERE ContentXml.exist('/contents/chapter[contains(@title, sql:variable(""@data""))]') = 1 
            ORDER BY Id", connection);
        
        command.Parameters.Add(new SqlParameter("@data", SqlDbType.NVarChar) { Value = title });

        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();
         
        while (await reader.ReadAsync())
        {
            bookEntities.Add(new BookEntity
            {
                Id = (Guid)reader["Id"],
                Title = (string)reader["Title"],
                Author = (string)reader["Author"],
                Published = (DateTime)reader["Published"],
                Genre = (string)reader["Genre"],
                Description = (string)reader["Description"],
                ContentXml = (string)reader["ContentXml"]
            });
        }

        await connection.CloseAsync();

        return bookEntities;
    }
}