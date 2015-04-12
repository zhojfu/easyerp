namespace Domain.Model.Products
{
    using Domain.Model.Factory;
    using Domain.Model.Security;
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product : BaseEntity, IAggregateRoot
    {
        private ICollection<ProductSpecificationAttribute> productSpecificationAttributes;

        private ICollection<ProductAttributeMapping> productAttributeMappings;

        private ICollection<ProductAttributeCombination> productAttributeCombinations;

        public ICollection<ProductStatistic> ProduceRecord { get; set; }

        public ICollection<MaterialStatisitc> MaterialComsumptions { get; set; }

        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short description
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the full description
        /// </summary>
        public string FullDescription { get; set; }

        public int StockQuantity { get; set; }

        /// <summary>
        /// Gets or sets the SKU
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets the Global Trade Item Number (GTIN). These identifiers include UPC (in North America), EAN (in Europe), JAN (in Japan), and ISBN (for books).
        /// </summary>
        public string Gtin { get; set; }

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product cost
        /// </summary>
        public decimal ProductCost { get; set; }

        /// <summary>
        /// Gets or sets the weight
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the length
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        /// Gets or sets the width
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// Gets or sets the height
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of product creation
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of product update
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedOnUtc { get; set; }

        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>
        public virtual ICollection<ProductSpecificationAttribute> ProductSpecificationAttributes
        {
            get { return this.productSpecificationAttributes ?? (this.productSpecificationAttributes = new List<ProductSpecificationAttribute>()); }
            protected set { this.productSpecificationAttributes = value; }
        }

        /// <summary>
        /// Gets or sets the product attribute mappings
        /// </summary>
        public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings
        {
            get { return this.productAttributeMappings ?? (this.productAttributeMappings = new List<ProductAttributeMapping>()); }
            protected set { this.productAttributeMappings = value; }
        }

        /// <summary>
        /// Gets or sets the product attribute combinations
        /// </summary>
        public virtual ICollection<ProductAttributeCombination> ProductAttributeCombinations
        {
            get { return this.productAttributeCombinations ?? (this.productAttributeCombinations = new List<ProductAttributeCombination>()); }
            protected set { this.productAttributeCombinations = value; }
        }
    }
}