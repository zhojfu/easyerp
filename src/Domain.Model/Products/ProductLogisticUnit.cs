namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;

    public class ProductLogisticUnit : BaseEntity
    {
        public virtual LogisticUnitType Type { get; set; }

        public virtual float Height { get; set; }

        public virtual float Width { get; set; }

        public virtual float Length { get; set; }

        public virtual float Weight { get; set; }
    }

    public enum LogisticUnitType
    {
        Unit,

        Pack,

        Box,

        Pallet
    }
}