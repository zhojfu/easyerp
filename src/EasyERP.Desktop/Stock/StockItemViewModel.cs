namespace EasyERP.Desktop.Stock
{
    using NullGuard;
    using PropertyChanged;
    using System;

    [ImplementPropertyChanged]
    public class StockItemViewModel
    {
        [AllowNull]
        public DateTime ProductionTime { get; set; }

        [AllowNull]
        public DateTime StockTime { get; set; }

        public double Quantity { get; set; }

        public double Cost { get; set; }

        [AllowNull]
        public string Origin { get; set; }

        [AllowNull]
        public string ProductId { get; set; }
    }
}