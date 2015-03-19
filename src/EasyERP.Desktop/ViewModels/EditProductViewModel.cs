namespace EasyERP.Desktop.ViewModels
{
    using Caliburn.Micro;
    using Doamin.Service;
    using Domain.Model;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public class EditProductViewModel : PropertyChangedBase
    {
        private readonly ProductService productService;

        public EditProductViewModel(ProductService productService)
        {
            this.productService = productService;
        }

        public Product Product { get; set; }

        public bool Result { get; set; }

        public void Ok()
        {
            this.Result = true;
            this.productService.AddNewProduct(this.Product);
        }

        public void Cancel()
        {
            this.Result = false;
        }
    }
}