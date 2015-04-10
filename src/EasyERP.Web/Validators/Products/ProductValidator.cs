namespace EasyERP.Web.Validators.Products
{
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Products;
    using FluentValidation;

    public class ProductValidator : BaseValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name.Required");
        }
    }
}