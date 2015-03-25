namespace EasyERP.Desktop.Stock
{
    using Caliburn.Micro;
    using NullGuard;
    using PropertyChanged;
    using System.Collections.Generic;

    [ImplementPropertyChanged]
    public class EditStockItemViewModel : Screen
    {
        [AllowNull]
        public StockItemViewModel StockItem { get; set; }

        [AllowNull]
        public IList<string> Products { get; set; }

        public void Ok()
        {
            this.TryClose(true);
        }

        public void Cancel()
        {
            this.TryClose();
        }
    }
}