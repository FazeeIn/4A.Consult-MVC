using System.Xml.Linq;
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
            .Must(BeValidXml).WithMessage("ContentXml is not valid XML.");
    }

    private bool BeValidXml(string xml)
    {
        try
        {
            _ = XDocument.Parse(xml);
                return true;
        }
        catch
        {
            return false;
        }
        
    }
}