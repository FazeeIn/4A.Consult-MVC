namespace Project.API.Contracts.Requests;

public record UpdateBookRequest(
    Guid Id,
    string Title,
    string Author,
    DateTime Published,
    string Genre,
    string Description,
    string ContentsXml);