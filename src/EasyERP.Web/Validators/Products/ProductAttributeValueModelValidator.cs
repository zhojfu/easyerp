namespace EasyERP.Web.Validators.Products
{
    using Domain.Model.Products;
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Products;
    using FluentValidation;

    public class ProductAttributeValueModelValidator : BaseValidator<ProductModel.ProductAttributeValueModel>
    {
        public ProductAttributeValueModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name.Required");
        }
    }
}