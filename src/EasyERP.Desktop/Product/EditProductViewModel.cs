namespace EasyERP.Desktop.Product
{
    using Caliburn.Micro;
    using NullGuard;
    using PropertyChanged;
    using System.Collections.Generic;

    [ImplementPropertyChanged]
    public class EditProductViewModel : Screen
    {
        [AllowNull]
        public ProductModel Product { get; set; }

        public List<string> Categories { get; set; }

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