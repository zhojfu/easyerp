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
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name.Required");

            this.RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Quantity.GreaterThanOrEqualTo1")
                .When(x => x.AttributeValueTypeId == (int)AttributeValueType.AssociatedToProduct);
        }
    }
}