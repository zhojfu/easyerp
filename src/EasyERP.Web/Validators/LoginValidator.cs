using EasyERP.Web.Models.Users;

namespace EasyERP.Web.Validators
{
    using EasyERP.Web.Framework.Validators;
    using FluentValidation;

    public class LoginValidator : BaseValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(x => x.Password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}