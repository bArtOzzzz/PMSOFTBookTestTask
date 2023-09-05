using FluentValidation;
using PMSOFTBookTestTask.Models.Request;

namespace PMSOFTBookTestTask.Validation
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Email).EmailAddress()
                                 .WithMessage("Invalid email");

            RuleFor(u => u.Password).Length(8, 26)
                                    .WithMessage("Password should be 8 to 26 characters");
        }
    }
}
