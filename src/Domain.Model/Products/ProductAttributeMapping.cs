namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;

    public class ProductAttributeMapping : BaseEntity
    {
        private ICollection<ProductAttributeValue> _productAttributeValues;

        public int ProductId { get; set; }

        public int ProductAttributeId { get; set; }

        public string TextPrompt { get; set; }

        public bool IsRequired { get; set; }

        public int AttributeControlTypeId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ProductAttribute ProductAttribute { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues
        {
            get { return _productAttributeValues ?? (_productAttributeValues = new List<ProductAttributeValue>()); }
            protected set { _productAttributeValues = value; }
        }
    }
}