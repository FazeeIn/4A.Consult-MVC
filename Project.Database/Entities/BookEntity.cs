namespace Project.Database.Entities;

public class BookEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime Published { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string ContentXml { get; set; }
}