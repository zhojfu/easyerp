namespace Domain.Model
{
    using Domain.Model.Factory;
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;

    public class Product : BaseEntity, IAggregateRoot
    {
        public string Upc { get; set; } //条形码 KEY

        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public ICollection<RepositoryStock> RepositoryStocks { get; set; }
        public ICollection<ProductStatistic> ProduceRecord { get; set; } 
        public ICollection<MaterialStatisitc> MaterialComsumptions { get; set; } 
        public ICollection<Price> Prices { get; set; }
    }
}