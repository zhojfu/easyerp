namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;

    public class ProductAttributeValue : BaseEntity
    {
        public int ProductAttributeMappingId { get; set; }

        public int AssociatedProductId { get; set; }

        public string Name { get; set; }

        public string ColorSquaresRgb { get; set; }

        public decimal PriceAdjustment { get; set; }

        public decimal WeightAdjustment { get; set; }

        public decimal Cost { get; set; }

        public int Quantity { get; set; }

        public bool IsPreSelected { get; set; }

        public int DisplayOrder { get; set; }

        public int PictureId { get; set; }

        public virtual ProductAttributeMapping ProductAttributeMapping { get; set; }
    }
}