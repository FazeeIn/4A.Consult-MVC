namespace Project.Core.Models;

public class Book
{
    public Guid Id { get; }
    public string Title { get; }
    public string Author { get; }
    public DateTime Published { get; }
    public string Genre { get; }
    public string Description { get; }
    public string ContentXml { get; }

    public Book(Guid id, string title, string author, 
        DateTime published, string genre, string description, string contentXml)
    {
        Id = id;
        Title = title;
        Author = author;
        Published = published;
        Genre = genre;
        Description = description;
        ContentXml = contentXml;
    }
}