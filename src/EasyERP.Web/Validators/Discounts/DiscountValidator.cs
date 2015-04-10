namespace EasyERP.Web.Validators.Discounts
{
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Discounts;
    using FluentValidation;

    public class DiscountValidator : BaseValidator<DiscountModel>
    {
        public DiscountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Required");
        }
    }
}