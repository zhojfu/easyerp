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
            RuleFor(x => x.Width).GreaterThanOrEqualTo(0).WithMessage("高度不能小于0");
            RuleFor(x => x.Height).GreaterThanOrEqualTo(0).WithMessage("高度不能小于0");
            RuleFor(x => x.Length).GreaterThanOrEqualTo(0).WithMessage("高度不能小于0");
            RuleFor(x => x.Weight).GreaterThanOrEqualTo(0).WithMessage("高度不能小于0");
        }
    }
}