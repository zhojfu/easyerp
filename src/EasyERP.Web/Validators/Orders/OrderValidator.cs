namespace EasyERP.Web.Validators.Orders
{
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Orders;
    using FluentValidation;

    public class OrderValidator : BaseValidator<OrderModel>
    {
        public OrderValidator()
        {
        }
    }
}