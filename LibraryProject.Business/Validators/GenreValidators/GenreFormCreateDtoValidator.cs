using FluentValidation;
using LibraryProject.Business.Dto.Genres;

namespace LibraryProject.Business.Validators.GenreValidators
{
    public class GenreFormCreateDtoValidator : AbstractValidator<GenreFormCreateDto>
    {
        public GenreFormCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("{PropertyName} should be not empty")
                .Must(name => name.All(char.IsLetter))
                .WithMessage("{PropertyName} should be only letters.");
        }
    }
}
