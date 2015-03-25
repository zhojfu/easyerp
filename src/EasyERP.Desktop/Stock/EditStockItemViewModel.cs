namespace EasyERP.Desktop.Stock
{
    using Caliburn.Micro;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class EditStockItemViewModel : Screen
    {
        public StockItemViewModel StockItem { get; set; }

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