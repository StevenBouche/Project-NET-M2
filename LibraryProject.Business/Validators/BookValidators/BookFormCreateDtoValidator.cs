using FluentValidation;
using LibraryProject.Business.Dto.Books;

namespace LibraryProject.Business.Validators.BookValidators
{
    public class BookFormCreateDtoValidator : AbstractValidator<BookFormCreateDto>
    {
        public BookFormCreateDtoValidator()
        {
            RuleFor(book => book.Name).NotEmpty().WithMessage("{PropertyName} should be not empty");
            RuleFor(book => book.Author).NotEmpty().WithMessage("{PropertyName} should be not empty");
            RuleFor(book => book.Content).NotEmpty().WithMessage("{PropertyName} should be not empty");
            RuleFor(book => book.Price)
                .NotEmpty().WithMessage("{PropertyName} should be not empty")
                .Must(price => price < double.MaxValue).WithMessage("{PropertyName} should be a decent number")
                .Must(price => price >= 0).WithMessage("{PropertyName} should be a positive number");


        }
    }
}
