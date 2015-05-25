namespace EasyERP.Web.Validators.Products
{
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Products;
    using FluentValidation;

    public class ProductValidator : BaseValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("产品名不能为空");
            RuleFor(x => x.ItemNo).Length(4, 4).WithMessage("产品编号长度为4");
            RuleFor(x => x.Gtin).NotEmpty().WithMessage("条码不能为空").Matches(@"^\d{13}$").WithMessage("条码不符合规范");
        }
    }
}