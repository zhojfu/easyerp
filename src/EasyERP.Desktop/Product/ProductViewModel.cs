namespace EasyERP.Desktop.Product
{
    using NullGuard;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class ProductViewModel
    {
        [AllowNull]
        public string Upc { get; set; } //条形码 KEY

        [AllowNull]
        public string Name { get; set; }

        [AllowNull]
        public string Description { get; set; }

        [AllowNull]
        public string Unit { get; set; }

        public double? Price { get; set; }

        //public double Price
        //{
        //    get
        //    {
        //        if (!this.Prices.Any())
        //        {
        //            return 0;
        //        }
        //        return this.Prices.Aggregate(
        //            (latest, price) =>
        //            (latest == null || latest.UpdataTime > price.UpdataTime ? latest : price))
        //                   .IfNotNull(p => p.SalePrice);
        //    }
        //    set { }
        //}

        public double? StockQuantity { get; set; }

        [AllowNull]
        public string Origin { get; set; }
    }
}