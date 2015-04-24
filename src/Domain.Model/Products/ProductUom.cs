namespace Domain.Model.Products
{
    using System.ComponentModel;
    using Infrastructure.Domain.Model;

    public class ProductUom : BaseEntity
    {
        [DefaultValue(1)]
        public float Factor { get; set; }

        public float Rounding { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        [DefaultValue(1)]
        public int Type { get; set; }

        public long UomCategoryId { get; set; }
        public virtual ProductUomCategory Category { get; set; }
    }

    public enum UomType
    {
        Bigger,

        Reference,

        Smaller
    }
}