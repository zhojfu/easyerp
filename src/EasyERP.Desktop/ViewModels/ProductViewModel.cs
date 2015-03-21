namespace EasyERP.Desktop.ViewModels
{
    using NullGuard;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class ProductViewModel
    {
        [AllowNull]
        public string Upc { get; set; } //条形码

        [AllowNull]
        public string Name { get; set; }

        [AllowNull]
        public string Description { get; set; }

        [AllowNull]
        public double Price { get; set; }

        [AllowNull]
        public double Cost { get; set; }

        [AllowNull]
        public double Volume { get; set; }

        [AllowNull]
        public string Origin { get; set; }
    }
}