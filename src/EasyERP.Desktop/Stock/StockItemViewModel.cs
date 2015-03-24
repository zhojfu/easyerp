namespace EasyERP.Desktop.Stock
{
    using NullGuard;
    using PropertyChanged;
    using System;

    [ImplementPropertyChanged]
    public class StockItemViewModel
    {
        [AllowNull]
        public string ProductName { get; set; } //mapto Product.Name

        [AllowNull]
        public string ProductDescription { get; set; }

        [AllowNull]
        public string Upc { get; set; }

        [AllowNull]
        public DateTime ProductionTime { get; set; }

        [AllowNull]
        public DateTime StockTime { get; set; }

        public double Quantity { get; set; }

        public double Cost { get; set; }

        [AllowNull]
        public string Origin { get; set; }
    }
}