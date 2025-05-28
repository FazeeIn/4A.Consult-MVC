namespace Project.API.Contracts.Requests;

public record BookRequest(
    string Title,
    string Author,
    DateTime Published,
    string Genre,
    string Description,
    string ContentsXml);