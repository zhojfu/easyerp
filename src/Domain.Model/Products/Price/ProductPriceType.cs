namespace Domain.Model.Products.Price
{
    using Infrastructure.Domain.Model;
    using System.ComponentModel;

    public class ProductPriceType : BaseEntity
    {
        [DefaultValue(true)]
        public string Active { get; set; }
    }
}