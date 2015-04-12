namespace Domain.Model.Products
{
    using global::Infrastructure.Domain.Model;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a specification attribute
    /// </summary>
    public partial class SpecificationAttribute : BaseEntity
    {
        private ICollection<SpecificationAttributeOption> _specificationAttributeOptions;

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the specification attribute options
        /// </summary>
        public virtual ICollection<SpecificationAttributeOption> SpecificationAttributeOptions
        {
            get { return this._specificationAttributeOptions ?? (this._specificationAttributeOptions = new List<SpecificationAttributeOption>()); }
            protected set { this._specificationAttributeOptions = value; }
        }
    }
}