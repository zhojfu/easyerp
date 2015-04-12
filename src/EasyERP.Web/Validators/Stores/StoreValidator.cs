namespace EasyERP.Web.Validators.Stores
{
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Stores;
    using FluentValidation;

    public class StoreValidator : BaseValidator<StoreModel>
    {
        public StoreValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name.Required");
        }
    }
}