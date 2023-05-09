using FluentValidation;
using MusicMarket.Api.DTO;

namespace MusicMarket.Api.Validators
{
    public class SaveMusicResourceValidator:AbstractValidator<SaveMusicDto>
    {
        public SaveMusicResourceValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MaximumLength(50);
            RuleFor(m => m.ArtistId).NotEmpty().WithMessage("Artist Id' must be 0.");
        }
    }
}
