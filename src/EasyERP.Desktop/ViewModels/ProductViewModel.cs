namespace EasyERP.Desktop.ViewModels
{
    using Caliburn.Micro;
    using Doamin.Service;
    using Domain.Model;
    using EasyERP.Desktop.Contacts;
    using PropertyChanged;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    [ImplementPropertyChanged]
    public class ProductViewModel : Screen, IViewModel
    {
        private readonly ProductService productService;

        public ProductViewModel(ProductService productService)
        {
            this.productService = productService;
        }

        public override string DisplayName
        {
            get { return "Product Management"; }
            set { }
        }

        public string SearchProductName { get; set; }

        public int SearchCategoryId { get; set; }

        public IList<string> CategoryList { get; set; }

        public bool SearchIncludeSubCategories { get; set; }

        public IList<string> ManufactureList { get; set; }

        public int SearchManufacturerId { get; set; }

        public IList<string> StoreList { get; set; }

        public int SearchStoreId { get; set; }

        public IList<string> VendorList { get; set; }

        public int SearchVendorId { get; set; }

        public IList<string> WarehouseList { get; set; }

        public int SearchWarehouseId { get; set; }

        public IList<string> ProductTypeList { get; set; }

        public int SearchProductTypeId { get; set; }

        public int SearchPublishedId { get; set; }

        public string GoDirectlyToSku { get; set; }

        public ObservableCollection<Product> Products
        {
            get
            {
                var ids = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                return new ObservableCollection<Product>(this.productService.GetProductsByIds(ids));
            }
            set { }
        }

        public void AddProduct()
        {
            var edit = new EditProductViewModel(this.productService)
            {
                Product = new Product()
            };
            IoC.Get<IWindowManager>().ShowWindow(edit);
        }

        public void Delete()
        {
        }

        public void Import()
        {
        }

        public void Export()
        {
        }

        public void GoToSku()
        {
            //var product = this.productService.GetProductBySku(this.GoDirectlyToSku);
        }
    }
}