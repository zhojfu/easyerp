namespace EasyERP.Web.Validators.Products
{
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Products;
    using FluentValidation;

    public class ProductListModelValidator : BaseValidator<ProductListModel>
    {
        public ProductListModelValidator()
        {
            RuleFor(x => x.SearchStoreId).GreaterThanOrEqualTo(0).WithMessage("Id不能小于0");
            RuleFor(x => x.SearchCategoryId).GreaterThanOrEqualTo(0).WithMessage("Id不能小于0");
        }
    }
}