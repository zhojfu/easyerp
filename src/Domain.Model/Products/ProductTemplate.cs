namespace Domain.Model.Products
{
    using Domain.Model.Company;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ProductTemplate : BaseEntity
    {
        /// <summary>
        /// Gets or sets the template name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the view path
        /// </summary>
        public string ViewPath { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}