namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;

    public class ProductStatistic: BaseEntity, IAggregateRoot
    {
        public double QualifyQuaitity { get; set; }
        public double UnQualifyQuatity { get; set; }
        #region foreign Key
        public string Upc { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
