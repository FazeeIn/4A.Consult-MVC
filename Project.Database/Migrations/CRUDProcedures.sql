CREATE PROCEDURE CreateBook
    @Id UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Author NVARCHAR(255),
    @Published DATETIME,
    @Genre NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @ContentXml XML
AS
BEGIN
    INSERT INTO Books (Id, Title, Author, Published, Genre, Description, ContentXml)
    VALUES (@Id, @Title, @Author, @Published, @Genre, @Description, @ContentXml);
END;
GO

-- GetBooks: получить все книги
CREATE PROCEDURE GetBooks
AS
BEGIN
    SELECT * FROM Books;
END;
GO

-- GetBookById: получить одну книгу по Id
CREATE PROCEDURE GetBookById
@Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM Books WHERE Id = @Id;
END;
GO

-- UpdateBook: обновить книгу
CREATE PROCEDURE UpdateBook
    @Id UNIQUEIDENTIFIER,
    @Title NVARCHAR(255),
    @Author NVARCHAR(255),
    @Published DATETIME,
    @Genre NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @ContentXml XML
AS
BEGIN
    UPDATE Books
    SET
        Title = @Title,
        Author = @Author,
        Published = @Published,
        Genre = @Genre,
        Description = @Description,
        ContentXml = @ContentXml
    WHERE Id = @Id;
END;
GO

-- DeleteBook: удалить книгу
CREATE PROCEDURE DeleteBook
@Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Books WHERE Id = @Id;
END;
GO