using App.Web.Areas.Admin.Models.Account;
using FluentValidation;

namespace App.Web.Areas.Admin.Validators.Account
{
    public partial class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
