using System.Xml;
using FluentValidation;
using Project.Core.Models;

namespace Project.Core.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title must not be empty.")
            .MaximumLength(200).WithMessage("Title is too long.")
            .Matches(@"^[\p{L}0-9\s\p{P}]+$").WithMessage("Title contains invalid characters.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author must not be empty.")
            .MaximumLength(100).WithMessage("Author is too long.")
            .Matches(@"^[\p{L}\s\.\-']+$").WithMessage("Author contains invalid characters.");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre must not be empty.")
            .MaximumLength(100).WithMessage("Genre is too long.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description must not be empty.")
            .MaximumLength(1000).WithMessage("Description is too long.");

        RuleFor(x => x.ContentXml)
            .NotEmpty().WithMessage("ContentXml must not be empty.")
            .Must(IsValidXml).WithMessage("ContentXml is not valid XML.");
    }

    private bool IsValidXml(string xml)
    {
        var allowedTags = new HashSet<string> { "chapter" };
        var allowedAttributes = new Dictionary<string, HashSet<string>>
        {
            { "chapter", new HashSet<string> { "number", "title" } }
        };

        var xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.LoadXml(xml);
        }
        catch (XmlException ex)
        {
            throw new InvalidOperationException("Неверный XML: " + ex.Message);
        }

        var root = xmlDoc.DocumentElement;
        
        if (root == null || root.Name != "contents")
            throw new InvalidOperationException("XML должен начинаться с корневого тега <contents>");

        foreach (XmlNode node in root.ChildNodes)
        {
            if (node.NodeType != XmlNodeType.Element)
                continue;

            if (!allowedTags.Contains(node.Name))
                throw new InvalidOperationException($"Недопустимый тег: <{node.Name}>");

            foreach (XmlAttribute attr in node.Attributes)
            {
                if (!allowedAttributes[node.Name].Contains(attr.Name))
                    throw new InvalidOperationException($"Недопустимый атрибут '{attr.Name}' в теге <{node.Name}>");
            }
        }
        
        return true;
    }
    
    
}