namespace EasyERP.Desktop.Product
{
    using Caliburn.Micro;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class EditProductViewModel : Screen
    {
        public ProductViewModel Product { get; set; }

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