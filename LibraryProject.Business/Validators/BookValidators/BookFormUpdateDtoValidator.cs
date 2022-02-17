using FluentValidation;
using LibraryProject.Business.Dto.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Validators.BookValidators
{
    public class BookFormUpdateDtoValidator : AbstractValidator<BookFormUpdateDto>
    {
        public BookFormUpdateDtoValidator()
        {
            RuleFor(book => book.IdBook).NotEmpty().WithMessage("{PropertyName} should be not empty");
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
