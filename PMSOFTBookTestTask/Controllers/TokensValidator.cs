using PMSOFTBookTestTask.Models.Request;
using FluentValidation;

namespace PMSOFTBookTestTask.Controllers
{
    public class TokensValidator : AbstractValidator<TokensModel>
    {
        public TokensValidator()
        {
            RuleFor(u => u.AccessToken).NotNull()
                                       .NotEmpty()
                                       .WithMessage("Access token can't be null or empty");

            RuleFor(u => u.RefreshToken).NotNull()
                                        .NotEmpty()
                                        .WithMessage("Refresh token can't be null or empty");
        }
    }
}
