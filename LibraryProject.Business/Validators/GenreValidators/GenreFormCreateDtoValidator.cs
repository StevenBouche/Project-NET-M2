using FluentValidation;
using LibraryProject.Business.Dto.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Validators.GenreValidators
{
    public class GenreFormCreateDtoValidator : AbstractValidator<GenreFormCreateDto>
    {
        public GenreFormCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("{PropertyName} should be not empty")
                .Must(IsValidName).WithMessage("{PropertyName} should be only letters.");
        }

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
