namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;
    using System;

    public class ProductStatistic : BaseEntity, IAggregateRoot
    {
        public double QualifyQuaitity { get; set; }

        public double UnQualifyQuatity { get; set; }

        #region foreign Key

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        #endregion foreign Key
    }
}