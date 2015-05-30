using System;
using Domain.Model.Products;
using Infrastructure.Domain.Model;

namespace Domain.Model.Stores
{
    public class PostRetail : BaseEntity, IAggregateRoot
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public double Quantity { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
