namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;

    public class Product : BaseEntity, IAggregateRoot
    {
        public string Upc { get; set; } //条形码 KEY

        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public ICollection<RepositoryStock> RepositoryStocks { get; set; }

        public ICollection<Price> Prices { get; set; }
    }
}