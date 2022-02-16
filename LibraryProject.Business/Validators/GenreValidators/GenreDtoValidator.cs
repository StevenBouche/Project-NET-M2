using FluentValidation;
using LibraryProject.Business.Dto.Genres;


namespace LibraryProject.Business.Validators.GenreValidators
{
    public class GenreDtoValidator : AbstractValidator<GenreDto>
    {

        public GenreDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
