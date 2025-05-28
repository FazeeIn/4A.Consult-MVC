CREATE TABLE Books (
                       Id UNIQUEIDENTIFIER PRIMARY KEY,
                       Title NVARCHAR(255) NOT NULL,
                       Author NVARCHAR(255) NOT NULL,
                       Published DATETIME NOT NULL,
                       Genre NVARCHAR(100),
                       Description NVARCHAR(MAX),
                       ContentXml XML
);
