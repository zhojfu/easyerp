namespace Domain.Model.Stores
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Company;
    using Domain.Model.Orders;
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;

    public class Store : BaseEntity, IAggregateRoot
    {
        public Store()
        {
            ProductPrices = new List<ProductPrice>();
            Products = new List<Product>();
        }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int DisplayOrder { get; set; }

        public string StoreName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}