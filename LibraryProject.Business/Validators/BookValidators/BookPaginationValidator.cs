using FluentValidation;
using LibraryProject.Business.Dto.Books;

namespace LibraryProject.Business.Validators.BookValidators
{
    public class BookPaginationValidator : AbstractValidator<PaginationDto>
    {
        public BookPaginationValidator()
        {
            RuleFor(p => p.Page).NotEmpty().WithMessage("{PropertyName} should be not empty")
                .Must(page => page >= 1).WithMessage("{PropertyName} should be a positive number and not equal to 0")
                .Must(page => page < int.MaxValue).WithMessage("{PropertyName} should be a decent number");
            RuleFor(p => p.PageSize).NotEmpty().WithMessage("{PropertyName} should be not empty")
                .Must(size => size >= 1).WithMessage("{PropertyName} should be a positive number and not equal to 0")
                .Must(size => size < int.MaxValue).WithMessage("{PropertyName} should be a decent number");
        }
    }
}
