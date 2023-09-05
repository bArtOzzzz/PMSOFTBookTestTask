using FluentValidation;
using PMSOFTBookTestTask.Models.Request;

namespace PMSOFTBookTestTask.Validation
{
    public class BookValidator : AbstractValidator<BookModel>
    {
        public BookValidator()
        {
            RuleFor(f => f.Name).NotEmpty()
                                .NotNull()
                                .WithMessage("Name: cannot be null or empty");

            RuleFor(f => f.Year).NotEmpty()
                                .NotNull()
                                .WithMessage("Year: cannot be null or empty");

            RuleFor(f => f.AuthorId).NotEmpty()
                                    .NotNull()
                                    .WithMessage("AuthorId: cannot be null or empty");

            RuleFor(f => f.GenreId).NotEmpty()
                                   .NotNull()
                                   .WithMessage("GenreId: cannot be null or empty");
        }
    }
}
