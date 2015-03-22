namespace EasyERP.Desktop.ViewModels
{
    using Caliburn.Micro;
    using EasyERP.Desktop.Contacts;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class ListOrdersViewModel : Screen, IViewModel
    {
        public override string DisplayName
        {
            get { return "Order Management"; }
        }

        public string Tag
        {
            get { return "OrderManagement"; }
        }
    }
}