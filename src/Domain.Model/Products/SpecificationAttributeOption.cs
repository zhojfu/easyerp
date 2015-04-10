namespace Domain.Model.Products
{
    using global::Infrastructure.Domain.Model;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a specification attribute option
    /// </summary>
    public partial class SpecificationAttributeOption : BaseEntity
    {
        private ICollection<ProductSpecificationAttribute> _productSpecificationAttributes;

        /// <summary>
        /// Gets or sets the specification attribute identifier
        /// </summary>
        public int SpecificationAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the specification attribute
        /// </summary>
        public virtual SpecificationAttribute SpecificationAttribute { get; set; }

        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>
        public virtual ICollection<ProductSpecificationAttribute> ProductSpecificationAttributes
        {
            get { return this._productSpecificationAttributes ?? (this._productSpecificationAttributes = new List<ProductSpecificationAttribute>()); }
            protected set { this._productSpecificationAttributes = value; }
        }
    }
}