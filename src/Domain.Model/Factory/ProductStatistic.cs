namespace Domain.Model.Factory
{
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;

    public class ProductStatistic : Statistic, IAggregateRoot
    {
        public double QualifyQuaitity { get; set; }

        public double UnQualifyQuatity { get; set; }

        #region foreign Key

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        #endregion foreign Key
    }
}